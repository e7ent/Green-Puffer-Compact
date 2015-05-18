using UnityEngine;
using System.Collections;

public class GameManager : MonoSingleton<GameManager>
{
	private int coin;
	private PlayerCharacter player;


	public void Update()
	{
		if (player == null)
			player = FindObjectOfType<PlayerCharacter>();
	}


	public void GiveCoin(int coin)
	{
		if (coin <= 0)
			return;

		this.coin += coin;
	}



}
