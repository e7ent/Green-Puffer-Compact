using UnityEngine;
using System.Collections;


namespace E7Assets.Achievement
{
	public abstract class RewardProviderBase : ScriptableObject
	{
		private string description;


		public string Description
		{
			get { return description; }
		}


		public abstract bool CanProvide
		{
			get;
		}


		public abstract void Provide();
	}
}