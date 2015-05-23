using UnityEngine;
using System.Collections;
using E7Assets;

public class HintEffect : MonoBehaviour
{
	private float lastCreateTime;

	void OnFollow()
	{
		if (Time.time - lastCreateTime < 1)
			return;

		lastCreateTime = Time.time;


		var bounds = gameObject.CalculateBounds();
		var position = transform.position;
		position.y += bounds.size.y;
		Instantiate<GameObject>(GameSettings.Instance.warningPrefab).transform.position = position;
	}
}
