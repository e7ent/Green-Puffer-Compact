using UnityEngine;
using System.Collections;

public class GameTest : MonoBehaviour {

	string guid = "";

	public void OnGUI()
	{
		guid = GUILayout.TextField(guid);


		if (GUILayout.Button("Create Player"))
		{
			GameManager.Instance.CreatePlayer(guid);
		}
	}
}
