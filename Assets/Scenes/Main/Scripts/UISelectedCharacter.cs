using UnityEngine;
using System.Collections;
using Parse;

public class UISelectedCharacter : MonoBehaviour
{


	public IEnumerator Start()
	{
		var task = GameDataManager.Instance.LoginAsync();
		while (task.IsCompleted == false) yield return null;

		var character = Instantiate<PlayerCharacter>(GameDataManager.Instance.SelectedCharacter);
		Destroy(character.GetComponent<PlayerUserControl>());

		character.transform.position = Vector3.zero;
		character.transform.rotation = Quaternion.identity;
		character.GetComponent<Rigidbody2D>().gravityScale = 0;
	}

}
