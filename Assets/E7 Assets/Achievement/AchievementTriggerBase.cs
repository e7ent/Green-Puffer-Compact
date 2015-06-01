using UnityEngine;
using System.Collections;


namespace E7Assets.Achievement
{
	public abstract class AchievementTriggerBase : MonoBehaviour
	{
		[SerializeField] private string[] achievementIds;
		private IAchievement[] cachedAchievements;


		private void Awake()
		{
			if (AchievementManager.Instance.IsInit == false) return;

			cachedAchievements = new IAchievement[achievementIds.Length];

			for (int i = 0; i < achievementIds.Length; i++)
			{
				cachedAchievements[i] = AchievementManager.Instance.GetAchievement(achievementIds[i]);
			}
		}


		protected void Increment()
		{
			for (int i = 0; i < cachedAchievements.Length; i++)
				cachedAchievements[i].Increment();
		}
	}
}