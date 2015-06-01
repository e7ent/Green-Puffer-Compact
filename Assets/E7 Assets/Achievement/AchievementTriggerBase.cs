using UnityEngine;
using System.Collections;


namespace E7Assets.Achievement
{
	public abstract class AchievementTriggerBase : MonoBehaviour
	{
		[SerializeField] private Achievement[] achievements;
	}
}