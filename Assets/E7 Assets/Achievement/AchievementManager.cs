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
		private List<IAchievement> achievements = null;

		public class TEST:ScriptableObject
		{
			public string asdf;
		}

		public bool IsInit { get { return achievements != null; } }


		protected override void Awake()
		{
			base.Awake();
			DontDestroyOnLoad(this);
		}


		public Task Init()
		{
			var assets = Resources.LoadAll<Achievement>("Achievements");

			var query = ParseObject.GetQuery("Achievement");
			query.WhereEqualTo("createdBy", DataManager.instance.User);

			achievements = new List<IAchievement>(assets.Cast<IAchievement>());

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

						assets[i].IsFinish = item.Get<bool>("finish");
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


		public IEnumerable<IAchievement> GetAllAchievement()
		{
			return achievements;
		}
	}
}