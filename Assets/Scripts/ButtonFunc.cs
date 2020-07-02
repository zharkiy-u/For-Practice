using System.Collections;
using UnityEngine;

public class ButtonFunc : MonoBehaviour
{
    public Joint2D joint;
	public float motor_speed;

	private enum Joints
	{
		hinge, slider
	};

	public enum Buttons
	{
		wall, push
	};

	private Joints joint_type;
	public Buttons button;
	private Vector2 startPos;
	private bool isActive = false;
	public bool isGate;

	public bool isPlayer = false;
	private bool ready = true;

	private void Start()
	{
		if (joint is HingeJoint2D) joint_type = Joints.hinge;
		if (joint is SliderJoint2D) joint_type = Joints.slider;
		startPos = transform.position;
	}

	private void Update()
	{
		if ((Input.GetKeyDown(KeyCode.F) && button is Buttons.wall && isPlayer) || (CheckPush() && button is Buttons.push))
		{
			Play_Switch_Sound();
			SwitchJoint();
		}
		switch (joint_type)
		{
			case Joints.hinge:
				break;
			case Joints.slider:
				if(!isGate) ChangeSliderDirection();
				break;
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			isPlayer = true;
			GameObject.Find("GUI").GetComponent<GUI_Functions>().isButton = true;
		}
	}
	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			isPlayer = false;
			GameObject.Find("GUI").GetComponent<GUI_Functions>().isButton = false;
		}
	}

	private void ChangeSliderDirection()
	{
		SliderJoint2D slider = (SliderJoint2D)joint;
		JointMotor2D motor = new JointMotor2D();
		motor.maxMotorTorque = 10000f;
		if (slider.motor.motorSpeed != 0 && ready && CheckLimits(slider))
		{
			motor_speed *= -1f;
			motor.motorSpeed = motor_speed;
			slider.motor = motor;
			StartCoroutine(Wait(0.1f));
		}
	}

	private bool CheckLimits(SliderJoint2D slider)
	{
		if (slider.limitState == JointLimitState2D.LowerLimit || slider.limitState == JointLimitState2D.UpperLimit) return true;
		else return false;
	}

	private bool CheckPush()
	{
		if ((isActive && Vector2.Distance(transform.position, startPos) <= 0.1f) || (!isActive && Vector2.Distance(transform.position, startPos) > 0.1f))
		{
			isActive = !isActive;
			return true;
		}
		else return false;
	}

	private void SwitchJoint()
	{
		JointMotor2D motor = new JointMotor2D();
		motor.maxMotorTorque = 10000f;
		switch (joint_type)
		{
			case Joints.hinge:
				motor.motorSpeed = (motor_speed *= -1f);
				((HingeJoint2D)joint).motor = motor;
				break;
			case Joints.slider:
				if (isGate) motor.motorSpeed = (motor_speed *= -1f);
				else
				{
					if (((SliderJoint2D)joint).motor.motorSpeed != 0) motor.motorSpeed = 0;
					else motor.motorSpeed = motor_speed;
				}
				((SliderJoint2D)joint).motor = motor;
				break;
		}
	}

	IEnumerator Wait(float second)
	{
		ready = false;
		yield return new WaitForSeconds(second);
		ready = true;
	}

	private void Play_Switch_Sound()
	{
		GameObject.Find("Soundtrack").GetComponent<Soundtrack>().event_number = 2;
	}
}
