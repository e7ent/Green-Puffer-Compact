using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Screen)]
	public class SelectRandomInt : FsmStateAction
	{
		[CompoundArray("Values", "Value", "Weight")]
		public FsmInt[] values;
		[HasFloatSlider(0, 1)]
		public FsmFloat[] weights;
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmInt storeValue;
		
		public override void Reset()
		{
			values = new FsmInt[2];
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