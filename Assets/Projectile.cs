using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
	public float MetersPerSecond = 10;

	public void Shoot(Vector3 startPosition, Vector3 endPosition)
	{
		gameObject.SetActive(true);
		transform.position = startPosition;
		float dist = Vector3.Distance(endPosition, startPosition);
		float totalTime = dist / MetersPerSecond;

		CoroutineHelper.Instance.StartCoroutine(Shoot(startPosition, endPosition, totalTime));
	}

	IEnumerator Shoot(Vector3 startPosition, Vector3 endPosition, float totalTime)
	{
		float time = 0;

		while(time < totalTime)
		{
			yield return null;
			float normalizedTime = time / totalTime;
			Vector3 newLinearPosition = Vector3.Lerp(startPosition, endPosition, normalizedTime);
			float verticalComponent = Mathf.Sin(normalizedTime * Mathf.PI);
			transform.position = newLinearPosition + Vector3.up * verticalComponent;
			time += Time.deltaTime;
		}
		transform.position = endPosition;

		yield return new WaitForSeconds(1);
		ProjectilePool.Instance.ReturnProjectile(this);
	}
}
