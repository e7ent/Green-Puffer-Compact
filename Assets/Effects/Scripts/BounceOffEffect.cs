using UnityEngine;
using System.Collections;

public class BounceOffEffect : MonoBehaviour
{
	[SerializeField] private string tag = "Creature";
	[SerializeField] private float force = 10;


	private Rigidbody2D rigidbody;


	private void Awake()
	{
		rigidbody = GetComponent<Rigidbody2D>();
	}


	public void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.CompareTag(tag) == false)
			return;

		Vector2 dir = transform.position - coll.transform.position;
		rigidbody.AddForce(dir.normalized * force, ForceMode2D.Impulse);
	}
}
