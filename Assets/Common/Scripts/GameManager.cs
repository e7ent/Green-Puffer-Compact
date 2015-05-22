using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;

public class GameManager : MonoSingleton<GameManager>
{
	public int Score
	{
		get { return score + (int)(endTime - startTime); }
		set { score = value; }
	}

	public int Coin
	{
		get { return coin; }
		set { coin = value; }
	}


	private bool isGameOver = false;
	private int coin;
	private int score;
	private float startTime, endTime;
	private PlayerCharacter player;
	private FsmInt coinFsm;
	private FsmInt scoreFsm;
	private FsmInt bestScoreFsm;
	private FsmFloat hpPercentageFsm;


	protected override void Awake()
	{
		base.Awake();
		coinFsm = FsmVariables.GlobalVariables.FindFsmInt("Coin");
		scoreFsm = FsmVariables.GlobalVariables.FindFsmInt("Score");
		bestScoreFsm = FsmVariables.GlobalVariables.FindFsmInt("Best Score");
		hpPercentageFsm = FsmVariables.GlobalVariables.FindFsmFloat("HP Percentage");
	}


	private void Start()
	{
		FadeManager.FadeIn();
		startTime = Time.time;
	}


	private void Update()
	{
		if (isGameOver)
			return;

		if (player == null)
			player = FindObjectOfType<PlayerCharacter>();

		endTime = Time.time;

		UpdateFsm();
	}


	private void UpdateFsm()
	{
		hpPercentageFsm.Value = player.HP / player.MaxHP;
		coinFsm.Value = Coin;
		scoreFsm.Value = Score;
	}


	public void Flush()
	{
		print("GameManager Data Flush");
		GameDataManager.Instance.RecordPlayLog(startTime, endTime, Coin);
		GameDataManager.Instance.BestScore = Score;
		GameDataManager.Instance.Coin += Coin;
		bestScoreFsm.Value = GameDataManager.Instance.BestScore;
	}
}
