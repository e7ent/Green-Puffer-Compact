using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

public class FadeManager : MonoSingleton<FadeManager>
{
	public static new FadeManager Instance
	{
		get
		{
			if (instance == null)
			{
				instance = GameObject.FindObjectOfType<FadeManager>();
				if (instance != null)
					return instance;

				instance = new GameObject("_FadeManager").AddComponent<FadeManager>();
			}

			return instance;
		}
	}


	public int SortingOrder
	{
		get { return canvas.sortingOrder; }
		set { canvas.sortingOrder = value; }
	}

	public Color Color
	{
		get { return color; }
	}


	private Canvas canvas;
	private Image image;
    private Color color;
	private Tween tween;


	protected override void Awake()
	{
        base.Awake();

		DontDestroyOnLoad(gameObject);

		if ((canvas = GetComponent<Canvas>()) == null)
			canvas = gameObject.AddComponent<Canvas>();
		if ((image = GetComponent<Image>()) == null)
			image = gameObject.AddComponent<Image>();

		canvas.renderMode = RenderMode.ScreenSpaceOverlay;
		canvas.enabled = false;
	}


	/// <summary>
	/// 투명으로 페이드인 된다.
	/// </summary>
	public static void FadeIn(float duration = 1, Action onFinish = null)
	{
		Instance.Fade(instance.color, Color.clear, duration, onFinish);
	}


	/// <summary>
	/// 투명에서 검은색으로 페이드아웃 된다.
	/// </summary>
	public static void FadeOut(float duration = 1, Action onFinish = null)
	{
		Instance.Fade(Color.clear, Color.black, duration, onFinish);
	}


	public void FadeTo(Color to, float duration = 1, Action onFinish = null)
	{
		Fade(color, to, duration, onFinish);
	}


	/// <summary>
	/// 페이드 한다.
	/// </summary>
	/// <param name="from">시작 색상</param>
	/// <param name="to">끝 색상</param>
	/// <param name="duration">페이드에 걸리는 시간</param>
	/// <param name="onComplate">페이드 완료 콜백</param>
	public void Fade(Color from, Color to, float duration = 1, Action onFinish = null)
	{
		if (tween != null)
			tween.Kill();
		if (canvas.enabled != true)
			canvas.enabled = true;
		image.color = from;
		tween = image.DOColor(to, duration).OnComplete(() =>
		{
			if (onFinish != null)
				onFinish();

			if (to.a <= 0)
				canvas.enabled = false;
		}).SetUpdate(true);
        color = to;
	}
}
