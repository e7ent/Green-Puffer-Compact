using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerUserControl : MonoBehaviour
{
	private PlayerCharacter character;
	private Rigidbody2D rigidbody;
	private List<Tween> tweens = new List<Tween>();


	private void Awake()
	{
		character = GetComponent<PlayerCharacter>();
		rigidbody = GetComponent<Rigidbody2D>();
	}


	private void FixedUpdate()
	{
		float h = CrossPlatformInputManager.GetAxis("Horizontal");
		float v = CrossPlatformInputManager.GetAxis("Vertical");

		character.Move(new Vector2(h, v));
	}


	public void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.CompareTag("Creature") == false)
			return;

		var force = transform.position - coll.transform.position;
		force.Normalize();
		force *= rigidbody.mass;
		rigidbody.AddForce(force, ForceMode2D.Impulse);
	}


	void OnFeed()
	{
		for (int i = 0; i < tweens.Count; i++)
			tweens[i].Kill(true);

		tweens.Add(transform.DOPunchScale(Vector3.one * 0.2f, 0.4f));
	}
}
