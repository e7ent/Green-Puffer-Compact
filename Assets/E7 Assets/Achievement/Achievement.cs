using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Parse;


namespace E7Assets.Achievement
{
	public class Achievement : ScriptableObject, IAchievement
	{

		[SerializeField] private string id;
		[SerializeField] private string title;
		[SerializeField] [Multiline] private string description;
		[SerializeField] private int reward;
		[SerializeField] private string rewardDescription;
		[SerializeField] private Sprite icon;
		[SerializeField] private int totalStep;
		[SerializeField] private string resourceID;


		private int? cachedCurrentStep = null;
		private bool cachedFinish = false;


		public string ID { get { return id; } }
		public string Title { get { return title; } }
		public string Description { get { return description; } }
		public string RewardDescription { get { return rewardDescription; } }
		public int Reward { get { return reward; } }
		public Sprite Icon { get { return icon; } }
		public int TotalStep { get { return totalStep; } }
		public bool IsSatisfy { get { return CurrentStep >= totalStep; } }


		public bool IsFinish
		{
			get { return cachedFinish; }
			set { cachedFinish = value; }
		}


		public int CurrentStep
		{
			get
			{
				if (cachedCurrentStep.HasValue == false)
					cachedCurrentStep = PlayerPrefs.GetInt(id, 0);
				return cachedCurrentStep.Value;
			}


			private set
			{
				cachedCurrentStep = value;
				PlayerPrefs.SetInt(id, value);
			}
		}


		public void Increment()
		{
			if (IsFinish)
				return;

			if (CurrentStep >= TotalStep)
				return;

			CurrentStep++;
		}


		public Task ProvideReward()
		{
			if (DataManager.Instance.User == null)
				return null;

			if (IsFinish)
				return null;

			if (IsSatisfy == false)
				return null;

			DataManager.Instance.Coin += Reward;

			var query = ParseObject.GetQuery("Achievement");
			query.WhereEqualTo("createdBy", DataManager.Instance.User);
			query.WhereEqualTo("achievementId", id);
			
			return query.FirstAsync().ContinueWith(t =>
			{
				if (t.IsFaulted)
				{
					var newObj = new ParseObject("Achievement");
					newObj["createdBy"] = DataManager.Instance.User;
					newObj["achievementId"] = id;
					newObj["finish"] = true;
					return newObj.SaveAsync();
				}

				var obj = t.Result;
				obj["finish"] = true;
				return obj.SaveAsync();
			}).Unwrap();
		}
	}
}