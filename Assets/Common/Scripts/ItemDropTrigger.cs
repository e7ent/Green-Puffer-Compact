using UnityEngine;
using System.Collections;

public class ItemDropTrigger : MonoBehaviour
{
	public Item[] itemPrefabs;
	

	IEnumerator OnKill()
	{
		yield return new WaitForSeconds(1);
		var prefabs = GameSettings.Instance.explosion;
		Instantiate(prefabs[Random.Range(0, prefabs.Length)], transform.position, Quaternion.identity);

		for (int i = 0; i < itemPrefabs.Length; i++)
		{
			var item = Instantiate(itemPrefabs[i], transform.position, Quaternion.identity) as Item;
		}
	}


}
