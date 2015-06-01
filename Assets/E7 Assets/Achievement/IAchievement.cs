using System.Threading.Tasks;
using UnityEngine;

namespace E7Assets.Achievement
{
	public interface IAchievement
	{
		string ID { get; }
		string Title { get; }
		string Description { get; }
		int Reward { get; }
		string RewardDescription { get; }
		Sprite Icon { get; }
		int TotalStep { get; }
		bool IsSatisfy { get; }
		bool IsFinish { get; }
		int CurrentStep { get; }


		void Increment();
		Task ProvideReward();
	}
}
