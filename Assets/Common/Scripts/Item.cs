using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour
{
	public float addHP;
	public float addSize;

	public void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player") == false)
			return;

		other.GetComponent<PlayerCharacter>().Feed(addHP, addSize);
		Destroy(gameObject);
	}

}
