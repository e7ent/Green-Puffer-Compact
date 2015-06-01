using UnityEngine;
using System.Collections;
using System.Threading.Tasks;
using E7Assets.Achievement;

public class GameInitializer : MonoBehaviour
{
	public IEnumerator Start()
	{
		var task = DataManager.Instance.Init();
		while (task.IsCompleted == false)
			yield return null;


		task = AchievementManager.Instance.Init();
		while (task.IsCompleted == false)
			yield return null;

		Application.LoadLevel("Main");
	}
}
