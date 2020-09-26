using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [Header("Day colors")]
    public Color DayFogColor;
    public Color DaySkyBoxColorTop;
    public Color DaySkyBoxColorMid;
    public Color DaySkyBoxColorBottom;

    [Header("Night colors")]
    public Color NightFogColor;
    public Color NightSkyBoxColorTop;
    public Color NightSkyBoxColorMid;
    public Color NightSkyBoxColorBottom;

    public float CycleDuration = 120;
    public int DayCount { get; private set; }

    private void Update()
    {
        //RenderSettings.fogColor = lerp    
    }
}
