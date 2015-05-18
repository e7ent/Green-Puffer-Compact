// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Math)]
	public class SelectRandomFloat : FsmStateAction
	{
		[CompoundArray("Values", "Value", "Weight")]
		public FsmFloat[] values;
		[HasFloatSlider(0, 1)]
		public FsmFloat[] weights;
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmFloat storeValue;
		
		public override void Reset()
		{
			values = new FsmFloat[2];
			weights = new FsmFloat[] {1, 1};
			storeValue = null;
		}

		public override void OnEnter()
		{
			DoSelectRandomValue();
			Finish();
		}
		
		void DoSelectRandomValue()
		{
			if (values == null) return;
			if (values.Length == 0) return;
			if (storeValue == null) return;

			int randomIndex = ActionHelpers.GetRandomWeightedIndex(weights);
			
			if (randomIndex != -1)
			{
				storeValue.Value = values[randomIndex].Value;
			}
		}
	}
}