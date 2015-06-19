using UnityEngine;
using System.Collections;
using E7Assets.Achievement;

public class GameInitializer : MonoBehaviour
{
    public IEnumerator Start()
    {
        var task = DataManager.Instance.Init();
        while (task.IsCompleted == false)
            yield return null;
        Debug.Log("Data Manager 초기화 완료");

        task = AchievementManager.Instance.Init();
        while (task.IsCompleted == false)
            yield return null;
        Debug.Log("Achievement Manager 초기화 완료");

        Application.LoadLevel("Main");
    }
}
