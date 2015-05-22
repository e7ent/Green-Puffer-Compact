using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityStandardAssets.CrossPlatformInput;
using HutongGames.PlayMaker;

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


	void OnFeed()
	{
		transform.DOKill(true);
		transform.DOPunchScale(Vector3.one * 0.2f, 0.4f);
	}

	IEnumerator OnKill()
	{
		yield return null;
		Time.timeScale = 0;
		GameManager.Instance.Flush();
		PlayMakerFSM.BroadcastEvent("OnGameFinish");
	}
}
