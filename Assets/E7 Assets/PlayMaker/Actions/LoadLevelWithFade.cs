// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

using System.Collections;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Level)]
	public class LoadLevelWithFade : FsmStateAction
	{
		[RequiredField]
		public FsmString levelName;
		public bool additive;
		public bool async;
		public FsmEvent loadedEvent;
		public FsmBool dontDestroyOnLoad;

		private bool isFading = false;
		private AsyncOperation asyncOperation;

		public override void Reset()
		{
			levelName = "";
			additive = false;
			async = false;
			loadedEvent = null;
			dontDestroyOnLoad = false;
		}

		public override void OnEnter()
		{
			isFading = true;
			FadeManager.Instance.SortingOrder = short.MaxValue;
			FadeManager.Instance.FadeTo(Color.black, 1, () =>
			{
				if (dontDestroyOnLoad.Value)
				{
					// Have to get the root, since this FSM will be destroyed if a parent is destroyed.

					var root = Owner.transform.root;

					Object.DontDestroyOnLoad(root.gameObject);
				}

				if (additive)
				{
					if (async)
					{
						asyncOperation = Application.LoadLevelAdditiveAsync(levelName.Value);

						Debug.Log("LoadLevelAdditiveAsyc: " + levelName.Value);

						return; // Don't Finish()
					}

					Application.LoadLevelAdditive(levelName.Value);

					Debug.Log("LoadLevelAdditive: " + levelName.Value);
				}
				else
					if (async)
					{
						asyncOperation = Application.LoadLevelAsync(levelName.Value);

						Debug.Log("LoadLevelAsync: " + levelName.Value);

						return; // Don't Finish()
					}
					else
					{
						Application.LoadLevel(levelName.Value);

						Debug.Log("LoadLevel: " + levelName.Value);
					}


				// LogAction: FsmExecutionStack.ExecutingAction == null 오류남
				// 나중에 위 내용 전부 고치자
				//Log("LOAD COMPLETE");

				Fsm.Event(loadedEvent);
				Finish();
				isFading = false;
			});
		}

		public override void OnUpdate()
		{
			if (isFading) return;
			if (asyncOperation.isDone)
			{
				Fsm.Event(loadedEvent);
				Finish();
			}
		}
	}
}