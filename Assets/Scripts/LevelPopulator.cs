using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPopulator : MonoBehaviour
{
	public LevelPopulatorSettings Settings;
	public Transform Root;

    void Start()
    {
	    for(float z = 0; z < Settings.Distance; z += Settings.Step)
	    {
		    for (float x = -Settings.Width; x < Settings.Width; x+=Settings.Step)
		    {
			    float totalProbability = 0;
			    foreach (LevelPopulatorSettings.EnvironmentObjectData data in Settings.Objects)
			    {
				    totalProbability += data.VisibilityProbability.Evaluate(Mathf.Abs(x) / Settings.Width);
			    }

			    float randomNumber = Random.Range(0f, 1f);
			    float cumulativeNormalizedProbability = 0;
			    foreach (LevelPopulatorSettings.EnvironmentObjectData data in Settings.Objects)
			    {
				    float normalizedProbability = data.VisibilityProbability.Evaluate(Mathf.Abs(x) / Settings.Width) / totalProbability;
				    cumulativeNormalizedProbability += normalizedProbability;

				    if (randomNumber <= cumulativeNormalizedProbability)
				    {
					    if (data.Prefab != null)
					    {
						    Vector2 randomOffset = Random.insideUnitCircle * 0.75f;
						    Create(data, new Vector3(x + Settings.Step/2f + randomOffset.x, 0,  z + Settings.NegativeStartingPoint + randomOffset.y));
					    }
					    break;
				    }
			    }
		    }
	    }
    }

    void Create(LevelPopulatorSettings.EnvironmentObjectData data, Vector3 position)
    {
	    GameObject instance = Instantiate(data.Prefab).gameObject;
	    instance.transform.SetParent(Root);
	    instance.transform.position = position;
    }
}
