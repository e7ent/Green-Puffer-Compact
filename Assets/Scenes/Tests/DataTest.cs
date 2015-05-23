using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Parse;
using UnityEngine;

public class DataTest : MonoBehaviour {

	Task task;

	public void OnGUI()
	{
		if (GUILayout.Button("Login"))
		{
			GameDataManager.Instance.LoginAsync();
		}

		if (GUILayout.Button("Test PlayLog"))
		{
			Test_RecordPlayLog();
		}

		if (GUILayout.Button("Test CoinLog"))
		{
			Test_RecordCoinLog();
		}

		if (GUILayout.Button("Set Best Score"))
		{
			var user = GameDataManager.Instance.User;
			user["bestScore"] = 100;
			user.SaveAsync();
		}

		if (GUILayout.Button("Get User Count"))
		{
			ParseUser.Query.CountAsync().ContinueWith(t =>
			{
				print(t.Result);
			});
		}

		if (GUILayout.Button("Test Array"))
		{
			//var guids = from character in GameSettings.Instance.characters select character.ID.guid;
			//GameDataManager.Instance.User.AddRangeToList<string>("characters", guids);
			//GameDataManager.Instance.User.SaveAsync();

			var list = GameDataManager.Instance.User.Get<IEnumerable<object>>("characters");
			foreach (string item in list)
			{
				print(item);
			}
		}


		if (GUILayout.Button("Create All Owned Characters"))
		{
			foreach (var item in GameDataManager.Instance.OwnedCharacters)
			{
				Instantiate<PlayerCharacter>(item);
			}
		}
	}


	void Test_RecordPlayLog()
	{
		GameDataManager.Instance.RecordPlayLog(Time.realtimeSinceStartup, Time.realtimeSinceStartup + 100, 100);
	}


	void Test_RecordCoinLog()
	{
		GameDataManager.Instance.RecordCoinLog(100, 200);
	}
}
