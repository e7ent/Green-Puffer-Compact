using UnityEngine;
using System.Collections;
using Parse;
using System.Threading.Tasks;

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
