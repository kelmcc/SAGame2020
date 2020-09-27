using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
	public static PlayerBehaviour Instance;

	public void Awake()
	{
		Instance = this;
	}


}
