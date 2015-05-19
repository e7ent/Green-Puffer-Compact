using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class HurtEffect : MonoBehaviour
{
	public Color color = Color.red;


	private List<Material> materials = new List<Material>();
	private List<Tween> tweens = new List<Tween>();


	void Awake()
	{
		foreach (var renderer in GetComponentsInChildren<Renderer>())
		{
			var mat = renderer.material;
			if (mat.HasProperty("_Color") == false) continue;
			materials.Add(mat);
		}
	}


	void OnHurt()
	{
		for (int i = 0; i < tweens.Count; i++)
			tweens[i].Kill(true);

		tweens.Clear();
		for (int i = 0; i < materials.Count; i++)
		{
			var seq = DOTween.Sequence();
			for (int j = 0; j < 3; j++)
			{
				seq.Append(materials[i].DOColor(color, 0.1f));
				seq.Append(materials[i].DOColor(Color.white, 0.1f));
			}
			tweens.Add(seq);
		}

		transform.DOKill(true);
		transform.DOPunchScale(Vector3.one * 0.2f, 0.4f);
		transform.DOShakeRotation(0.2f, 20);
	}
}
