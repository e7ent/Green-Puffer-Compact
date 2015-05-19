using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

namespace E7Assets.CrossPlatformInput
{
	public class OneHandControl : MonoBehaviour
	{
		public ElasticJoystick joystick;


		void Awake()
		{
			if (joystick == null)
				joystick = transform.GetChild(0).GetComponent<ElasticJoystick>();
		}


		void Update()
		{
			if (joystick.gameObject.activeSelf)
				return;


			if (Input.GetMouseButtonDown(0))
			{
				if (EventSystem.current.IsPointerOverGameObject())
					return;
				joystick.Show(Input.mousePosition);
				return;
			}

			for (int i = 0; i < Input.touchCount; i++)
			{
				if (EventSystem.current.IsPointerOverGameObject(i))
					continue;

				joystick.Show(i);
				return;
			}
		}
	}
}