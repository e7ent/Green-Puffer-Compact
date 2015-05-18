// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Math)]
	public class FloatSign : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
        [Tooltip("The Float variable.")]
		public FsmFloat floatVariable;

        [Tooltip("Repeat every frame. Useful if the Float variable is changing.")]
		public bool everyFrame;

		public override void Reset()
		{
			floatVariable = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoFloatAbs();
			
			if (!everyFrame)
			{
			    Finish();
			}
		}

		public override void OnUpdate()
		{
			DoFloatAbs();
		}
		
		void DoFloatAbs()
		{
			floatVariable.Value = Mathf.Sign(floatVariable.Value);
		}
	}
}