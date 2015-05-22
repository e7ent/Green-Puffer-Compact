using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Math)]
	public class Fade : FsmStateAction
	{
		public FsmColor from, to;
		public FsmFloat duration;
		public FsmEvent finishEvent;


		public override void OnEnter()
		{
			Color fromColor = FadeManager.Instance.Color;
			float duration = 1;

			if (from.IsNone == false)
				fromColor = from.Value;

			if (this.duration.IsNone == false)
				duration = this.duration.Value;

			FadeManager.Instance.Fade(fromColor, to.Value, duration, () =>
			{
				Fsm.Event(finishEvent);
				Finish();
			});
		}
	}
}