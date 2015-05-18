using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerUserControl : MonoBehaviour
{
	private PlayerCharacter character;


	private void Start()
	{
		character = GetComponent<PlayerCharacter>();
	}


	private void FixedUpdate()
	{
		float h = CrossPlatformInputManager.GetAxis("Horizontal");
		float v = CrossPlatformInputManager.GetAxis("Vertical");

		character.Move(new Vector2(h, v));
	}
}
