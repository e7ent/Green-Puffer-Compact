using UnityEngine;
using System.Collections;
using Parse;


namespace E7Assets.Achievement
{
	public class Achievement : ScriptableObject
	{
		[SerializeField] private string name;
		[SerializeField] private string description;
		[SerializeField] private Sprite icon;
		[SerializeField] private int totalStep;
		[SerializeField] private string id;
		[SerializeField] private RewardProviderBase rewardProvider;


		private bool isInit = false;
		private int cachedStep;
		private bool cachedIsFinished;


		public string Name { get { return name; } }
		public string Description { get { return description; } }
		public Sprite Icon { get { return icon; } }
		public int TotalStep { get { return totalStep; } }
		public int Step
		{
			get { return cachedStep; }
			set
			{
			}
		}
		public bool IsFinished { get { return cachedIsFinished; } }
		public string RewardDescription { get { return rewardProvider.Description; } }


		public void TryProvideReward()
		{

		}


		private void Init()
		{

		}
	}
}