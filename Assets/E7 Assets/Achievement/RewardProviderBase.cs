using System;
using System.Collections;
using UnityEngine;


namespace E7Assets.Achievement
{
	[Obsolete("당장 쓰지 않고 나중에 쓸 예정.")]
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