using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBrain : Brain
{
	public enum AIState
	{
		Vagrant,
		Following,
		Positioned,
		Enraged
	}


	public override float GetMovement()
	{
		return 0;
	}

	public override bool GetAction()
	{
		return false;
	}
}
