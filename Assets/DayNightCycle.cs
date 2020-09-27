using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
	public Light Light;
	public Material SkyBox;

	[Header("Day colors")]
	public Color DayFogColor;
	public Color DaySkyBoxColorTop;
	public Color DaySkyBoxColorMid;
	public Color DaySkyBoxColorBottom;
	public float DayFogValue = 0.02f;
	public float DayLightIntensity = 1;

	[Header("Night colors")]
	public Color NightFogColor;
	public Color NightSkyBoxColorTop;
	public Color NightSkyBoxColorMid;
	public Color NightSkyBoxColorBottom;
	public float NightFogValue = 0.07f;
	public float NightLightIntensity = 0;

	public float DayDuration = 60;
	public float DayTransDuration = 30;
	public float NightDuration = 30;
	public float NightTransDuration = 30;

	public float timeMultiplier = 1;
	public float CurrentTime;//{ get; private set; }
	public int DayCount;//{ get; private set; }

	private void Awake()
	{
		RenderSettings.fogColor = DayFogColor;
		RenderSettings.fogDensity = DayFogValue;
		SkyBox.SetColor("_Color1", DaySkyBoxColorTop);
		SkyBox.SetColor("_Color2", DaySkyBoxColorMid);
		SkyBox.SetColor("_Color3", DaySkyBoxColorBottom);
		Light.intensity = DayLightIntensity;

		CurrentTime = 0;
		DayCount = 0;
	}

	private void Update()
	{
		if (CurrentTime <= DayDuration)
		{
			RenderSettings.fogColor = DayFogColor;
			RenderSettings.fogDensity = DayFogValue;
			SkyBox.SetColor("_Color1", DaySkyBoxColorTop);
			SkyBox.SetColor("_Color2", DaySkyBoxColorMid);
			SkyBox.SetColor("_Color3", DaySkyBoxColorBottom);
			Light.intensity = DayLightIntensity;
			CurrentTime += Time.deltaTime * timeMultiplier;

		}
		else if (CurrentTime <= (DayTransDuration + DayDuration))
		{
			float localTime = (CurrentTime - DayDuration);
			RenderSettings.fogColor = Color.Lerp(DayFogColor, NightFogColor, localTime / DayTransDuration);
			RenderSettings.fogDensity = Mathf.Lerp(DayFogValue, NightFogValue, localTime / DayTransDuration);
			SkyBox.SetColor("_Color1", Color.Lerp(DaySkyBoxColorTop, NightSkyBoxColorTop, localTime / DayTransDuration));
			SkyBox.SetColor("_Color2", Color.Lerp(DaySkyBoxColorMid, NightSkyBoxColorMid, localTime / DayTransDuration));
			SkyBox.SetColor("_Color3", Color.Lerp(DaySkyBoxColorBottom, NightSkyBoxColorBottom, localTime / DayTransDuration));
			Light.intensity = Mathf.Lerp(DayLightIntensity, NightLightIntensity, localTime / DayTransDuration);
			CurrentTime += Time.deltaTime * timeMultiplier;
		}
		else if (CurrentTime <= (DayTransDuration + DayDuration + NightDuration))
		{
			RenderSettings.fogColor = NightFogColor;
			RenderSettings.fogDensity = NightFogValue;
			SkyBox.SetColor("_Color1", NightSkyBoxColorTop);
			SkyBox.SetColor("_Color2", NightSkyBoxColorMid);
			SkyBox.SetColor("_Color3", NightSkyBoxColorBottom);
			Light.intensity = NightLightIntensity;
			CurrentTime += Time.deltaTime * timeMultiplier;
		}
		else if (CurrentTime <= (DayTransDuration + DayDuration + NightDuration + NightTransDuration))
		{
			float localTime = (CurrentTime - DayTransDuration - DayDuration - NightDuration);
			RenderSettings.fogColor = Color.Lerp(NightFogColor, DayFogColor, localTime / NightTransDuration);
			RenderSettings.fogDensity = Mathf.Lerp(NightFogValue, DayFogValue, localTime / NightTransDuration);
			SkyBox.SetColor("_Color1", Color.Lerp(NightSkyBoxColorTop, DaySkyBoxColorTop, localTime / NightTransDuration));
			SkyBox.SetColor("_Color2", Color.Lerp(NightSkyBoxColorMid, DaySkyBoxColorMid, localTime / NightTransDuration));
			SkyBox.SetColor("_Color3", Color.Lerp(NightSkyBoxColorBottom, DaySkyBoxColorBottom, localTime / NightTransDuration));
			Light.intensity = Mathf.Lerp(NightLightIntensity, DayLightIntensity, localTime / NightTransDuration);
			CurrentTime += Time.deltaTime * timeMultiplier;
		}
		else
		{
			DayCount++;
			CurrentTime = 0;
		}
	}
}