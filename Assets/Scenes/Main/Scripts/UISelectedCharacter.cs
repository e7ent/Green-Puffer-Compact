using System.Linq;
using UnityEngine;
using System.Collections;
using Parse;

public class UISelectedCharacter : MonoBehaviour
{
	private void Start()
	{
		var character = Instantiate<PlayerCharacter>(DataManager.Instance.SelectedCharacter);

		var components = from component in character.GetComponents<Component>()
						 where component.GetType() != typeof(Transform)
						 select component;
		foreach (var item in components)
			Destroy(item);

		character.transform.position = Vector3.zero;
		character.transform.localScale = Vector3.one * 2;
		character.transform.rotation = Quaternion.identity;
	}

}
