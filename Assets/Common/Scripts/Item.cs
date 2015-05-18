using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour
{
	public float giveHP;
	public float giveSize;
	public int giveCoin;

	public void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player") == false)
			return;

		other.GetComponent<PlayerCharacter>().Feed(giveHP, giveSize);
		GameManager.instance.GiveCoin(giveCoin);
		Destroy(gameObject);
	}

}
