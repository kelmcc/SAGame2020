using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBrain : Brain
{
	public override float GetMovement()
	{
		return Input.GetAxis("Vertical");
	}

	public override bool GetAction()
	{
		return Input.GetButton("Action");
	}
}
