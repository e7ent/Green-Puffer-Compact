using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
	public GameObject[] prefabs;
	public float createTime;
	public float height;


	private IEnumerator Start()
	{
		while (true)
		{
			var obj = Instantiate(prefabs[Random.Range(0, prefabs.Length)]) as GameObject;
			var pos = transform.position;
			pos.y = Random.Range(0, height) - (height / 2);
			obj.transform.position = pos;
			obj.GetComponent<AICreatureControl>().SetMoveDirection(Mathf.Sign(transform.position.x) * -1);
			yield return new WaitForSeconds(createTime);
		}
	}

}
