using UnityEngine;
using System.Collections;

public class ItemDropTrigger : MonoBehaviour
{
	public Item[] itemPrefabs;
	

	IEnumerator OnKill()
	{
		yield return new WaitForSeconds(0.1f);
		var prefabs = GameSettings.Instance.explosion;
		Instantiate(prefabs[Random.Range(0, prefabs.Length)], transform.position, Quaternion.identity);

		for (int i = 0; i < itemPrefabs.Length; i++)
		{
			var item = Instantiate(itemPrefabs[i], transform.position, Quaternion.identity) as Item;
			var rigid = item.GetComponent<Rigidbody2D>();
			if (rigid == null)
			{
				Debug.Log("Not found Rigidbody2D from " + item);
				continue;
			}
			rigid.AddForce(Random.insideUnitCircle, ForceMode2D.Impulse);
			rigid.AddTorque(Random.Range(-1, 1) * 5);
		}
	}


}
