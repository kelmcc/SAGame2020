using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelPopulator : MonoBehaviour
{
	public int Seed = -1;
	public LevelPopulatorSettings Settings;
	public Transform Root;

	public LayerMask CollisionLayerMask;

	public static LevelPopulator Instance;

	private void Awake()
	{
		Instance = this;
	}

	void Start()
	{
		if (Seed != -1)
		{
			Random.InitState(Seed);
		}
		for (float z = 0; z < Settings.Distance; z += Settings.Step)
		{
			for (float x = -Settings.Width; x < Settings.Width; x += Settings.Step)
			{
				Vector2 randomOffset = Random.insideUnitCircle * 0.75f;
				Vector3 position = new Vector3(x + Settings.Step / 2f + randomOffset.x, 0, z + Settings.NegativeStartingPoint + randomOffset.y);

				float totalProbability = 0;
				foreach (LevelPopulatorSettings.EnvironmentObjectData data in Settings.Objects)
				{
					if (data.MinDistance <= z && data.MaxDistance >= z)
					{
						totalProbability += data.VisibilityProbability.Evaluate(Mathf.Abs(x) / Settings.Width);
					}
				}

				float randomNumber = Random.Range(0f, 1f);
				float cumulativeNormalizedProbability = 0;
				foreach (LevelPopulatorSettings.EnvironmentObjectData data in Settings.Objects)
				{
					if (data.MinDistance <= z && data.MaxDistance >= z)
					{
						float normalizedProbability = data.VisibilityProbability.Evaluate(Mathf.Abs(x) / Settings.Width) / totalProbability;
						cumulativeNormalizedProbability += normalizedProbability;

						if (randomNumber <= cumulativeNormalizedProbability)
						{
							if (!Physics.CheckSphere(position, data.CheckSurroundingsRadius, CollisionLayerMask.value, QueryTriggerInteraction.Collide))
							{
								if (data.Prefab != null)
								{
									Create(data, position);
								}
							}
							break;
						}
					}
				}
			}
		}
	}

	void Create(LevelPopulatorSettings.EnvironmentObjectData data, Vector3 position)
	{
		int randomFlip = Random.Range(0, 2);

		GameObject instance = Instantiate(data.Prefab).gameObject;
		instance.transform.SetParent(Root);
		instance.transform.position = position;

		if (randomFlip == 0)
		{
			instance.transform.localScale = new Vector3(-1,1,1);
		}
	}
}
