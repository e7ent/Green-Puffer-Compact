using UnityEngine;
using System.Collections;

public class HUDEffectTrigger : MonoBehaviour
{
	void OnHurt()
	{
		Instantiate(GameSettings.Instance.hitHUD, transform.position, Quaternion.identity);
	}
}
