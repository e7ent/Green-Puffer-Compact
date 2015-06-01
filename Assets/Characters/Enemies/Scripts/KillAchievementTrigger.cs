using E7Assets.Achievement;


public class KillAchievementTrigger : AchievementTriggerBase
{
	private void OnKill()
	{
		Increment();
	}
}