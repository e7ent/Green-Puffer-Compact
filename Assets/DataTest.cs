using UnityEngine;
using System.Collections;
using Parse;
using System.Threading.Tasks;

public class DataTest : MonoBehaviour {

	Task task;

	public void OnGUI()
	{
		if (GUILayout.Button("Test PlayLog"))
		{
			Test_RecordPlayLog();
		}

		if (GUILayout.Button("Test CoinLog"))
		{
			Test_RecordCoinLog();
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
