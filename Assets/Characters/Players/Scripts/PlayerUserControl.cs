using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerUserControl : MonoBehaviour
{
	private PlayerCharacter character;
	private Rigidbody2D rigidbody;


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
}
