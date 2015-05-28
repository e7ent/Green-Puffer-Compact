using UnityEngine;
using System.Collections;


namespace E7Assets.Achievement
{
	public abstract class AchievementTriggerBase : MonoBehaviour
	{
		public enum Mode
		{
			Parallel,
			Serial,
		}


		[SerializeField] private Mode mode;
		[SerializeField] private Achievement[] achievements;
	}
}