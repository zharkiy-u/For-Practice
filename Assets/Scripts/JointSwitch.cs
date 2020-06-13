using System.Collections;
using UnityEngine;

public class JointSwitch : MonoBehaviour
{
    public Joint2D joint;
	public float motor_speed;

	public enum Joints
	{
		hinge, slider, spring, target, wheel
	};

	public Joints type;

	private bool isPlayer = false;
	private bool ready = true;

	private void Start()
	{
		//joint = gameObject.GetComponentInParent<Joint2D>();
		//joint = gameObject.GetComponentInChildren<Joint2D>();
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Q) && isPlayer)
		{
			switch (type)
			{
				case Joints.hinge:
					SwitchHinge();
					break;
				case Joints.slider:
					SwitchSlider();
					break;
			}
		}

		switch (type)
		{
			case Joints.hinge:
				break;
			case Joints.slider:
				ChangeSliderDirection();
				break;
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player")) isPlayer = true;
	}
	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.CompareTag("Player")) isPlayer = false;
	}

	private void ChangeSliderDirection()
	{
		SliderJoint2D slider = (SliderJoint2D)joint;
		JointMotor2D motor = new JointMotor2D();
		motor.maxMotorTorque = 10000f;
		if (slider.motor.motorSpeed != 0 && ready && 
			(slider.limitState == JointLimitState2D.LowerLimit || slider.limitState == JointLimitState2D.UpperLimit))
		{
			motor_speed *= -1f;
			motor.motorSpeed = motor_speed;
			slider.motor = motor;
			StartCoroutine(Wait(0.1f));
		}
		
	}

	private void SwitchHinge()
	{
		HingeJoint2D hinge = (HingeJoint2D)joint;
		JointMotor2D motor = new JointMotor2D();
		motor_speed *= -1f;
		motor.motorSpeed = motor_speed;
		motor.maxMotorTorque = 10000f;
		hinge.motor = motor;
	}

	private void SwitchSlider()
	{
		SliderJoint2D slider = (SliderJoint2D)joint;
		JointMotor2D motor = new JointMotor2D();
		if (slider.motor.motorSpeed != 0) motor.motorSpeed = 0;
		else motor.motorSpeed = motor_speed;
		motor.maxMotorTorque = 10000f;
		slider.motor = motor;
	}

	IEnumerator Wait(float second)
	{
		ready = false;
		yield return new WaitForSeconds(second);
		ready = true;
	}
}
