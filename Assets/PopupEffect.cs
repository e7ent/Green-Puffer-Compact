using UnityEngine;
using DG.Tweening;

public class PopupEffect : MonoBehaviour
{

	public void OnEnable()
	{
		transform.localScale = Vector3.zero;
		transform.DOScale(1, 0.3f).SetEase(Ease.OutExpo).SetUpdate(true);
	}
}
