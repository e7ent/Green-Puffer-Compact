using UnityEngine;
using DG.Tweening;

public class PopupEffect : MonoBehaviour
{
	private const float DURATION = 0.2f;


	private Canvas canvas;
	private Tween tween;


	private void Awake()
	{
		canvas = GetComponentInParent<Canvas>();
	}


	public void Show()
	{
		// fade
		FadeManager.Instance.SortingOrder = canvas.sortingOrder - 1;
		FadeManager.Instance.FadeTo(new Color(0, 0, 0, 0.5f), DURATION);


		// tween
		if (tween != null)
			tween.Kill();
		tween = transform.DOScale(1, DURATION).SetEase(Ease.OutBack).SetUpdate(true);
	}


	public void Hide()
	{
		// fade
		FadeManager.FadeIn(DURATION);


		// tween
		if (tween != null)
			tween.Kill();
		tween = transform.DOScale(0, DURATION).SetEase(Ease.InBack).SetUpdate(true);
	}
}
