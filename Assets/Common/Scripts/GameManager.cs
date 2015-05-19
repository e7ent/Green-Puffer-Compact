using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;

public class GameManager : MonoSingleton<GameManager>
{
	private int coin;
	private float score;
	private PlayerCharacter player;
	private FsmInt scoreFsm;
	private FsmInt coinFsm;
	private FsmFloat hpPercentageFsm;


	public float Score
	{
		get { return score; }
		set
		{
			score = value;
			scoreFsm.Value = (int)score;
		}
	}


	public int Coin
	{
		get { return coin; }
		set
		{
			coin = value;
			coinFsm.Value = (int)coin;
		}
	}


	protected override void Awake()
	{
		base.Awake();
		scoreFsm = FsmVariables.GlobalVariables.FindFsmInt("Score");
		coinFsm = FsmVariables.GlobalVariables.FindFsmInt("Coin");
		hpPercentageFsm = FsmVariables.GlobalVariables.FindFsmFloat("HP Percentage");
	}


	private void Update()
	{
		if (player == null)
			player = FindObjectOfType<PlayerCharacter>();

		Score += Time.deltaTime;
		hpPercentageFsm.Value = player.HP / player.MaxHP;
	}


	public void GiveCoin(int addCoin)
	{
		if (addCoin <= 0)
			return;

		Coin += addCoin;
	}


	public void AddScore(int addScore)
	{
		if (addScore <= 0)
			return;

		Score += addScore;
	}

}
