using UnityEngine;
using System.Collections;
using DG.Tweening;

public class HUDEffect : MonoBehaviour
{
	public float duration = 0.5f;

	void Start()
	{
		var renderer = GetComponent<SpriteRenderer>();
		if (renderer != null)
			renderer.DOFade(0, duration);

		Destroy(gameObject, duration);
	}

	void Update()
	{
		transform.Translate(0, Time.deltaTime * 0.5f, 0);
	}
}
