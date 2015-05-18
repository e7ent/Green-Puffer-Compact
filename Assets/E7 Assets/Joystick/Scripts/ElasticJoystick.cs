using UnityEngine;
using DG.Tweening;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace E7Assets.CrossPlatformInput
{
	[RequireComponent(typeof(RectTransform))]
	public class ElasticJoystick : MonoBehaviour
	{

		public enum AxisOption
		{
			Both,
			OnlyHorizontal,
			OnlyVertical
		}


		[Header("Settings")]
		public int movementRange = 100;
		public AxisOption axesToUse = AxisOption.Both;
		public string horizontalAxisName = "Horizontal";
		public string verticalAxisName = "Vertical";


		[Header("Sprites")]
		public Image targetImage;
		public Sprite dragedSprite;


		public Vector3 StartPosition
		{
			get { return startPosition; }
			set { transform.position = startPosition = value; }
		}


		private int touchId;
		private bool useX;
		private bool useY;
		private Vector3 imageOrinalSize;
		private Vector2 imageOrinalPivot;
		private Vector3 startPosition;
		private CrossPlatformInputManager.VirtualAxis horizontalVirtualAxis;
		private CrossPlatformInputManager.VirtualAxis verticalVirtualAxis;


		void Awake()
		{
			touchId = -1;
			CreateVirtualAxes();

			if (targetImage == null)
				targetImage = GetComponent<Image>();

			if (targetImage != null)
			{
				imageOrinalSize = targetImage.rectTransform.sizeDelta;
				imageOrinalPivot = targetImage.rectTransform.pivot;
			}
		}


		void CreateVirtualAxes()
		{
			useX = (axesToUse == AxisOption.Both || axesToUse == AxisOption.OnlyHorizontal);
			useY = (axesToUse == AxisOption.Both || axesToUse == AxisOption.OnlyVertical);

			if (useX)
			{
				horizontalVirtualAxis = new CrossPlatformInputManager.VirtualAxis(horizontalAxisName);
				CrossPlatformInputManager.RegisterVirtualAxis(horizontalVirtualAxis);
			}
			if (useY)
			{
				verticalVirtualAxis = new CrossPlatformInputManager.VirtualAxis(verticalAxisName);
				CrossPlatformInputManager.RegisterVirtualAxis(verticalVirtualAxis);
			}
		}


		void UpdateVirtualAxes(Vector3 value)
		{
			var delta = startPosition - value;
			delta.y = -delta.y;
			delta /= movementRange;
			if (useX)
			{
				horizontalVirtualAxis.Update(-delta.x);
			}

			if (useY)
			{
				verticalVirtualAxis.Update(delta.y);
			}
		}


		void Update()
		{
			bool isHide = false;
			// if game paused.
			isHide |= Time.timeScale <= float.Epsilon;
			// if touch release.
			isHide |= (Input.touchCount <= touchId) && (touchId >= 0);
			// if mouse up.
			isHide |= !Input.GetMouseButton(0) && (touchId == -1);

			if (isHide)
			{
				Hide();
				return;
			}

			// get an input position
			var inputPosition = Vector3.zero;

			if (touchId >= 0)
				inputPosition = Input.touches[touchId].position;
			else
				inputPosition = Input.mousePosition;

			// CanvasScaler issus
			// 해결안함

			var delta = inputPosition - StartPosition;
			var deltaMag = delta.magnitude;
			
			var rectTransform = targetImage.rectTransform;
			rectTransform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(delta.y, delta.x) * Mathf.Rad2Deg);

			// clamp to movementRange
			if (deltaMag > movementRange)
			{
				delta = delta.normalized * movementRange;
				deltaMag = movementRange;
			}

			// elastic
			do
			{
				if (targetImage == null)
					break;
				if (dragedSprite == null)
					break;

				if (deltaMag < imageOrinalSize.magnitude)
				{
					if (targetImage.overrideSprite == dragedSprite)
					{
						SendMessage("OnControlSpriteChanged", SendMessageOptions.DontRequireReceiver);
						targetImage.overrideSprite = null;
						rectTransform.sizeDelta = imageOrinalSize;
						rectTransform.pivot = imageOrinalPivot;
					}
					break;
				}
				else
				{
					if (targetImage.overrideSprite != dragedSprite)
					{
						SendMessage("OnControlSpriteChanged", SendMessageOptions.DontRequireReceiver);
						targetImage.overrideSprite = dragedSprite;
					}
				}

				var imageSize = rectTransform.sizeDelta;
				imageSize.x = deltaMag + (imageOrinalSize.x * 0.5f);
				rectTransform.sizeDelta = imageSize;

				var pivot = rectTransform.pivot;
				pivot.x = (imageOrinalSize.x * 0.5f) / imageSize.x;
				rectTransform.pivot = pivot;

			} while (false);

			UpdateVirtualAxes(StartPosition + delta);
		}

		
		public void Show(Vector2 position)
		{
			transform.position = StartPosition = position;
			gameObject.SetActive(true);
			SendMessage("OnControlShow", SendMessageOptions.DontRequireReceiver);
		}


		public void Show(int touchId)
		{
			this.touchId = touchId;
			Show(Input.touches[touchId].position);
		}


		public void Hide()
		{
			UpdateVirtualAxes(StartPosition);
			SendMessage("OnControlHide", SendMessageOptions.DontRequireReceiver);
			gameObject.SetActive(false);
			if (targetImage != null)
				targetImage.overrideSprite = null;
			var rectTransform = targetImage.rectTransform;
			rectTransform.sizeDelta = imageOrinalSize;
			rectTransform.sizeDelta = imageOrinalSize;
			rectTransform.pivot = imageOrinalPivot;
			targetImage.overrideSprite = null;
		}


		void OnDestroy()
		{
			// remove the joysticks from the cross platform input
			if (useX)
			{
				horizontalVirtualAxis.Remove();
			}
			if (useY)
			{
				verticalVirtualAxis.Remove();
			}
		}
	}
}