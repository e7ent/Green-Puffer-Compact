using System.Linq;
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
		StartGame();
	}


	private void Update()
	{
		if (isGameOver)
			return;

		if (player == null)
			player = FindObjectOfType<PlayerCharacter>();
		if (player == null)
			return;

		endTime = Time.time;

		UpdateFsm();
	}


	private void UpdateFsm()
	{
		hpPercentageFsm.Value = player.HP / player.MaxHP;
		coinFsm.Value = Coin;
		scoreFsm.Value = Score;
	}


	public void StartGame()
	{
		startTime = Time.time;
		CreatePlayer(GameDataManager.instance.SelectedCharacter);
	}


	public void CreatePlayer(string guid)
	{
		var characters = GameSettings.Instance.characters;
		var prefab = from character in characters where character.ID.guid == guid select character;
		CreatePlayer(prefab.First());
	}


	public void CreatePlayer(PlayerCharacter prefab)
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
