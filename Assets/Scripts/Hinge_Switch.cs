using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Hinge_Switch : MonoBehaviour
{
    public HingeJoint2D hinge;

	public bool isPlayer = false;

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Q) && isPlayer)
		{
			JointMotor2D motor = new JointMotor2D();
			motor.motorSpeed = hinge.motor.motorSpeed * -1f;
			motor.maxMotorTorque = 10000f;
			hinge.motor = motor;
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
}
