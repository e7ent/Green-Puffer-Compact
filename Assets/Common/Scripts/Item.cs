using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour
{
	public float giveHP;
	public float giveSize;
	public int giveCoin;


	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player") == false)
			return;
		Use(other.GetComponent<PlayerCharacter>());
	}


	private void OnTriggerStay2D(Collider2D other)
	{
		if (other.CompareTag("Player") == false)
			return;
		Use(other.GetComponent<PlayerCharacter>());
	}


	private void Use(PlayerCharacter player)
	{
		if (player == null)
			return;

		// feeding
		player.Feed(giveHP, giveSize);


		// create effect
		if (giveHP > 0 || giveSize > 0)
			Instantiate(GameSettings.Instance.expHUD, transform.position, Quaternion.identity);

		if (giveCoin > 0)
		{
			Instantiate(GameSettings.Instance.coinHUD, transform.position, Quaternion.identity);
			GameManager.instance.GiveCoin(giveCoin);
		}


		// destroy self
		Destroy(gameObject);
	}

}
