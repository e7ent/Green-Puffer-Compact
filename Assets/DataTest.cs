using UnityEngine;
using System.Collections;
using Parse;
using System.Threading.Tasks;

public class DataTest : MonoBehaviour {

	Task task;

	public void OnGUI()
	{
		if (GUILayout.Button("Try Login", GUILayout.Width(161), GUILayout.Height(100)))
		{
			GameDataManager.Instance.LoginAsync();
		}

		if (GameDataManager.Instance.User != null)
			GUILayout.Label(GameDataManager.Instance.User.Username);
	}
}
