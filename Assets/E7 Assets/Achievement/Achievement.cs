using UnityEngine;
using System.Collections;


namespace E7Assets.Achievement
{
	public class Achievement : ScriptableObject
	{
		[SerializeField]
		private string name;
		[SerializeField]
		private string description;
		[SerializeField]
		private Sprite icon;
		[SerializeField]
		private string androidID, iOSID;

		public string Name
		{
			get { return name; }
		}
		public string Description
		{
			get { return description; }
		}
		public Sprite Icon
		{
			get { return icon; }
		}
		public int TotalStep
		{
			get
			{
				return 0;
			}
		}
		public int Step
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}
		public bool IsFinished;
	}
}