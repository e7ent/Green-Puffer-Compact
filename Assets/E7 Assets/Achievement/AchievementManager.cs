using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Parse;


namespace E7Assets.Achievement
{
	public class AchievementManager : MonoSingleton<AchievementManager>
	{
		private List<Achievement> achievements = null;


		public bool IsInit { get { return achievements != null; } }


		public Task Init()
		{
			var assets = Resources.LoadAll<Achievement>("Achievements");
			achievements = new List<Achievement>(assets);

			var query = ParseObject.GetQuery("Achievement");
			query.WhereEqualTo("createdBy", DataManager.instance.User);

			return query.FindAsync().ContinueWith(t =>
			{
				foreach (var item in t.Result)
				{
					var id = item.Get<string>("achievementId");

					for (int i = 0; i < achievements.Count; i++)
					{
						var curr = achievements[i];
						if (curr.ID != id)
							continue;

						achievements[i].IsFinish = item.Get<bool>("finish");
						break;
					}
				}
			});
		}


		public IAchievement GetAchievement(string id)
		{
			if (IsInit == false)
			{
				Debug.LogError("AchievementManager muse be init!");
				return null;
			}

			return (from ac in achievements where ac.ID == id select ac).First();
		}
	}
}