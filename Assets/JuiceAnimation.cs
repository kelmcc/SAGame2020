using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JuiceAnimation : MonoBehaviour
{
    public bool IsWobbling { get; private set; }
    public bool IsScaling { get; private set; }

    private bool _stopScale;
    private bool _stopWobble;
    private bool _stopLerpScale;

    public void Stop()
    {
        _stopScale = true;
        _stopWobble = true;
        _stopLerpScale = true;
    }

    public void PopScale(float startScale, float endScale, float time, Action then = null)
    {
        _stopScale = false;
        CoroutineHelper.Instance.StartCoroutine(CoroutinePopScale(startScale, endScale, time, then));
    }

    public void LerpScale(float startScale, float endScale, float time, Action then = null)
    {
        _stopLerpScale = false;
        CoroutineHelper.Instance.StartCoroutine(CoroutineLerpScale(startScale, endScale, time, then));
    }


    public void Wobble(float magnitude, float speed, float time, Action then = null)
    {
        _stopWobble = false;
        CoroutineHelper.Instance.StartCoroutine(CoroutineWobble(magnitude, speed, time, then));
    }

    private IEnumerator CoroutinePopScale(float startScale, float endScale, float time, Action then)
    {
        IsScaling = true;
        transform.localScale = new Vector3(startScale,startScale,startScale);
        float t = 0;
        while (t < 1)
        {
            if (_stopScale)
            {
                break;
            }
            t = Mathf.Clamp01(t + Time.deltaTime/time);
            float ot = Overshoot(t);

            float extraOvershoot = 0;
            if (startScale == endScale)
            {
                extraOvershoot = ot * 0.015f;
            }
            float newScale = Mathf.Lerp(startScale, endScale, ot) + extraOvershoot;
            transform.localScale = new Vector3(newScale,newScale,newScale);
            yield return null;
        }

        transform.localScale = new Vector3(endScale,endScale,endScale);

        IsScaling = false;
        then?.Invoke();
    }

    private IEnumerator CoroutineLerpScale(float startScale, float endScale, float time, Action then)
    {
        IsScaling = true;
        transform.localScale = new Vector3(startScale,startScale,startScale);
        float t = 0;
        while (t < 1)
        {
            if (_stopLerpScale)
            {
                break;
            }
            t = Mathf.Clamp01(t + Time.deltaTime/time);
            float newScale = Mathf.Lerp(startScale, endScale, t);
            transform.localScale = new Vector3(newScale,newScale,newScale);
            yield return null;
        }

        transform.localScale = new Vector3(endScale,endScale,endScale);

        IsScaling = false;
        then?.Invoke();
    }


    private IEnumerator CoroutineWobble(float magnitude, float speed, float time, Action then)
    {
        IsWobbling = true;
        Vector3 startPosition = transform.localPosition;
        float t = 0;
        while (t < 1)
        {
            if (_stopWobble)
            {
                break;
            }
            t = Mathf.Clamp01(t + Time.deltaTime/time);
            transform.localPosition = startPosition + new Vector3(Mathf.Sin(t*speed),Mathf.Cos(t*speed), Mathf.Sin(t*speed))* magnitude * t;
            yield return null;
        }
        transform.localPosition = startPosition;
        IsWobbling = false;
        then?.Invoke();
    }

    public static float Overshoot(float t)
    {
        float amp = 80;
        float freq = 1;
        float decay = 1;
        return amp*Mathf.Sin(t*freq*Mathf.PI*2)/Mathf.Exp(t*decay);
    }
}
