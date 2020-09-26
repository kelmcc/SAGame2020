using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelPopulatorSettings")]
public class LevelPopulatorSettings : ScriptableObject
{
	public List<EnvironmentObjectData> Objects;

	public float NegativeStartingPoint;
	public float Width;
	public float Distance;
	public float Step = 0.5f;

	[Serializable]
	public class EnvironmentObjectData
	{
		public EnvironmentObject Prefab;
		public AnimationCurve VisibilityProbability;
		public float MinDistance = 0;
		public float MaxDistance = 0;
		public float CheckSurroundingsRadius = 1;
	}
}
