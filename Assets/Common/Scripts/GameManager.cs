using System.Linq;
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;

public class GameManager : MonoSingleton<GameManager>
{
	private bool isFinish;
	private int coin;
	private int score;
	private float startTime;

	// current player
	private PlayerCharacter player;

	// fsm vars
	private FsmInt coinFsm;
	private FsmInt scoreFsm;
	private FsmFloat hpPercentageFsm;


	#region Properties

	public int Score
	{
		get { return score + (int)(Time.time - startTime); }
		set { score = value; }
	}

	public int Coin
	{
		get { return coin; }
		set { coin = value; }
	}

	public bool IsFinish { get { return isFinish; } }

	public bool IsPause { get { return Time.timeScale <= float.Epsilon; } }

	#endregion


	protected override void Awake()
	{
		base.Awake();
		coinFsm = FsmVariables.GlobalVariables.FindFsmInt("Coin");
		scoreFsm = FsmVariables.GlobalVariables.FindFsmInt("Score");
		hpPercentageFsm = FsmVariables.GlobalVariables.FindFsmFloat("HP Percentage");
	}


	private void Start()
	{
		isFinish = false;
		startTime = Time.time;
		InstantiatePlayerCharacter(DataManager.instance.SelectedCharacter);
	}


	private void Update()
	{
		if (isFinish)
			return;

		if (player == null)
			player = FindObjectOfType<PlayerCharacter>();
		if (player == null)
			return;

		UpdateFsm();
	}


	private void UpdateFsm()
	{
		hpPercentageFsm.Value = player.HP / player.MaxHP;
		coinFsm.Value = Coin;
		scoreFsm.Value = Score;
	}


	public PlayerCharacter InstantiatePlayerCharacter(PlayerCharacter prefab)
	{
		var position = Vector3.zero;
		var rotation = Quaternion.identity;

		if (player != null)
		{
			position = player.transform.position;
			rotation = player.transform.rotation;
			Destroy(player.gameObject);
		}

		Instantiate(GameSettings.Instance.createEffect, position, Quaternion.identity);
		player = Instantiate(prefab, position, rotation) as PlayerCharacter;
		return player;
	}


	public void Finish()
	{
		if (isFinish) return;

		Pause();

		DataManager.Instance.RecordPlayLog(startTime, Time.time, Coin);
		DataManager.Instance.BestScore = Score;
		DataManager.Instance.Coin += Coin;
		PlayMakerFSM.BroadcastEvent("OnGameFinish");

		isFinish = true;
	}


	public void Pause()
	{
		Time.timeScale = 0;
	}


	public void Resume()
	{
		Time.timeScale = 1;
	}
}
