using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

namespace GreenPuffer.Inputs.Joysticks
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

        public Vector3 StartPosition
        {
            get { return startPosition; }
            set { transform.position = startPosition = value; }
        }

        [Header("Settings")]
        [SerializeField]
        private int movementRange = 100;
        [SerializeField]
        private AxisOption axesToUse = AxisOption.Both;
        [SerializeField]
        private string horizontalAxisName = "Horizontal";
        [SerializeField]
        private string verticalAxisName = "Vertical";
		[Header("Sprites")]
        [SerializeField]
        private Image targetImage;
        [SerializeField]
        private Sprite dragedSprite;
        private Vector3 originScale;
		private bool useX;
		private bool useY;
		private Vector3 imageOrinalSize;
		private Vector2 imageOrinalPivot;
		private Vector3 startPosition;
		private CrossPlatformInputManager.VirtualAxis horizontalVirtualAxis;
		private CrossPlatformInputManager.VirtualAxis verticalVirtualAxis;

        private void Awake()
        {
#if !MOBILE_INPUT
            Destroy(gameObject);
#endif
            CreateVirtualAxes();

            Input.simulateMouseWithTouches = true;

            imageOrinalSize = targetImage.rectTransform.sizeDelta;
            imageOrinalPivot = targetImage.rectTransform.pivot;
            originScale = transform.localScale;
        }

        private void Clear()
        {
            ClearVirtualAxes();

            transform.DOKill();
            transform.localScale = originScale;

            if (targetImage != null)
                targetImage.overrideSprite = null;
            var rectTransform = targetImage.rectTransform;
            rectTransform.sizeDelta = imageOrinalSize;
            rectTransform.sizeDelta = imageOrinalSize;
            rectTransform.pivot = imageOrinalPivot;
            targetImage.overrideSprite = null;
        }

        private void CreateVirtualAxes()
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

        private void UpdateVirtualAxes(Vector3 value)
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

        private void ClearVirtualAxes()
        {
            if (useX)
            {
                horizontalVirtualAxis.Update(0);
            }
            if (useY)
            {
                verticalVirtualAxis.Update(0);
            }
        }

        private TouchPhase lastPhase = TouchPhase.Ended;
        private bool TryGetInputPosition(out Vector3 pos)
        {
            if (Input.GetMouseButton(0))
            {
                if ((lastPhase == TouchPhase.Ended && !EventSystem.current.IsPointerOverGameObject()) ||
                    lastPhase != TouchPhase.Ended)
                {
                    if (lastPhase == TouchPhase.Ended)
                        lastPhase = TouchPhase.Began;
                    if (lastPhase == TouchPhase.Moved)
                        lastPhase = TouchPhase.Moved;
                    pos = Input.mousePosition;
                    return true;
                }
            }
            else if (Input.touchCount >= 1)
            {
                if ((lastPhase == TouchPhase.Ended && !EventSystem.current.IsPointerOverGameObject()) ||
                    lastPhase != TouchPhase.Ended)
                {
                    if (lastPhase == TouchPhase.Ended)
                        lastPhase = TouchPhase.Began;
                    if (lastPhase == TouchPhase.Moved)
                        lastPhase = TouchPhase.Moved;
                    pos = Input.GetTouch(0).position;
                    return true;
                }
            }
            lastPhase = TouchPhase.Ended;
            pos = Vector3.zero;
            return false;
        }

        private void Update()
        {
            Vector3 inputPosition;

            if (TryGetInputPosition(out inputPosition))
			{
                if (!targetImage.enabled)
                {
                    targetImage.enabled = true;
                    transform.position = StartPosition = inputPosition;
                    transform.DOComplete();
                    transform.DOPunchScale(Vector3.one * .3f, 0.1f);
                }
            }
            else
            {
                Clear();
                if (targetImage.enabled)
                {
                    targetImage.enabled = false;
                }
                return;
            }

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
            if (deltaMag < imageOrinalSize.magnitude)
            {
                if (targetImage.overrideSprite == dragedSprite)
                {
                    transform.DOComplete();
                    transform.DOPunchScale(Vector3.one * .3f, 0.1f);

                    targetImage.overrideSprite = null;
                    rectTransform.sizeDelta = imageOrinalSize;
                    rectTransform.pivot = imageOrinalPivot;
                }
            }
            else
            {
                if (targetImage.overrideSprite != dragedSprite)
                {
                    transform.DOComplete();
                    transform.DOPunchScale(Vector3.one * .3f, 0.1f);

                    targetImage.overrideSprite = dragedSprite;
                }

                var imageSize = rectTransform.sizeDelta;
                imageSize.x = deltaMag + (imageOrinalSize.x * 0.5f);
                rectTransform.sizeDelta = imageSize;

                var pivot = rectTransform.pivot;
                pivot.x = (imageOrinalSize.x * 0.5f) / imageSize.x;
                rectTransform.pivot = pivot;
            }

            UpdateVirtualAxes(StartPosition + delta);
		}

        private void OnDestroy()
		{
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