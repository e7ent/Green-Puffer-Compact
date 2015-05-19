﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class HurtEffect : MonoBehaviour
{
	public Color color = Color.red;


	private Renderer[] renderers;
	private List<Tween> tweens = new List<Tween>();


	void Awake()
	{
		renderers = GetComponentsInChildren<Renderer>();
	}


	void OnHurt()
	{
		for (int i = 0; i < tweens.Count; i++)
			tweens[i].Kill(true);

		tweens.Clear();
		for (int i = 0; i < renderers.Length; i++)
		{
			var mat = renderers[i].material;
			if (mat.HasProperty("_Color") == false) continue;

			var seq = DOTween.Sequence();
			for (int j = 0; j < 3; j++)
			{
				seq.Append(renderers[i].material.DOColor(color, 0.1f));
				seq.Append(renderers[i].material.DOColor(Color.white, 0.1f));
			}
			tweens.Add(seq);
		}

		transform.DOKill(true);
		transform.DOPunchScale(Vector3.one * 0.2f, 0.4f);
		transform.DOShakeRotation(0.2f, 20);
	}
}
