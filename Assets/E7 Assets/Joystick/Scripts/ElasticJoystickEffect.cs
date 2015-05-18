using UnityEngine;
using DG.Tweening;

namespace E7Assets.CrossPlatformInput
{
	public class ElasticJoystickEffect : MonoBehaviour
	{
		private new Transform transform;
		private Vector3 originScale;

		public void Awake()
		{
			transform = GetComponent<Transform>();
			originScale = transform.localScale;
		}

		
		public void OnControlShow()
		{
			transform.DOPunchScale(Vector3.one * .3f, 0.1f);
		}


		public void OnControlHide()
		{
			transform.DOKill();
			transform.localScale = originScale;
		}


		public void OnControlSpriteChanged()
		{
			transform.DOPunchScale(Vector3.one * .3f, 0.1f);
		}
	}
}