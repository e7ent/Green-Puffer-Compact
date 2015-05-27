using UnityEngine;
using System.Collections;


namespace E7Assets.Achievement
{
	public abstract class AbstractAchievementTrigger : MonoBehaviour
	{
		public enum Mode
		{
			Parallel,
			Serial,
		}


		[SerializeField]
		private Mode mode;
		private Achievement[] achievements;
	}
}