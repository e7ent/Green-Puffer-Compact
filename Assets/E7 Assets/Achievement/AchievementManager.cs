using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Parse;


namespace E7Assets.Achievement
{
	public class AchievementManager : MonoSingleton<AchievementManager>
	{
		private List<Achievement> achievements;
		private List<Achievement> lazyAchievements;


		public Task SaveAllAsync()
		{
			foreach (var achievement in lazyAchievements)
			{
				achievement.incre
			}
		}
	}
}