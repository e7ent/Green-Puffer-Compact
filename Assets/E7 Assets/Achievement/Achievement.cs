using UnityEngine;
using System.Collections;
using System.Threading.Tasks;
using Parse;


namespace E7Assets.Achievement
{
	public class Achievement : ScriptableObject
	{
		[SerializeField] private string name;
		[SerializeField] private string description;
		[SerializeField] private string rewardDescription;
		[SerializeField] private int reward;
		[SerializeField] private Sprite icon;
		[SerializeField] private int totalStep;
		[SerializeField] private string id;


		private ParseObject dataSource;
		private ParseObject ownershipData;
		private int cachedCurrentStep;
		private bool cachedIsFinish;


		public string Name { get { return name; } }
		public string Description { get { return description; } }
		public string RewardDescription { get { return rewardDescription; } }
		public int Reward { get { return reward; } }
		public Sprite Icon { get { return icon; } }
		public int TotalStep { get { return totalStep; } }
		public int CurrentStep { get { return cachedCurrentStep; } }
		public bool IsFinish { get { return cachedIsFinish; } }


		public void TryProvideReward()
		{

		}


		public void Increment()
		{
			cachedCurrentStep++;
			if (cachedCurrentStep >= totalStep)
		}


		public Task SaveAsync()
		{
			var query = ParseObject.GetQuery("AchievementOwnershipData");
			query.WhereEqualTo("createBy", DataManager.Instance.User);
			query.WhereEqualTo("achievement", )
		}
	}
}