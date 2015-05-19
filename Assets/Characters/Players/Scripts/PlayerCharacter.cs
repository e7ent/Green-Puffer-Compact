using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public sealed class PlayerCharacter : CreatureCharacter
{
	public enum StateType
	{
		None = -1,
		Normal = 0,
		Thin = 1,
		Fat = 2,
		Rage = 3,
		Eat = 4,
		Attack = 5,
		Fear = 6,
		Stress = 7,
		Sweat = 8,
		Laze = 9,
		Pinch = 10,
		Sleep = 11,
		Hungry = 12,
		SpaceOut = 13,
		Full = 14,
		Blow = 15,
		Move = 16,
		Die = 17,
		Happy = 18,
		Fun = 19,
	}

	[Header("Animator")]
	[SerializeField] private Animator eye;
	[SerializeField] private Animator nose;
	[SerializeField] private Animator mouth;
	[SerializeField] private Animator body;
	[SerializeField] private Animator fin;


	[Header("Skill")]
	[SerializeField] private ActiveSkill activeSkill = null;
	[SerializeField] private PassiveSkill passiveSkill = null;

	private float additiveScale = 1;
	private StateType currentState;

	protected override void Awake()
	{
		base.Awake();
		currentState = StateType.Normal;
	}


	protected override void Update()
	{
		hp = Mathf.Clamp(hp - Time.deltaTime, 0, MaxHP);

		fin.speed = Mathf.Clamp(rigidbody.velocity.sqrMagnitude * 5.0f, 0.5f, 2);

		Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
		pos.x = Mathf.Clamp(pos.x, 0, 1);
		pos.y = Mathf.Clamp(pos.y, 0, 1);
		transform.position = Camera.main.ViewportToWorldPoint(pos);
	}


	public override void Move(Vector3 move, ForceMode2D forceMode = ForceMode2D.Force)
	{
		if (isAlive == false)
			return;

		if (move.magnitude > 1)
			move.Normalize();

		rigidbody.AddForce(move * force, forceMode);

		// sync face
		if (Mathf.Abs(move.x) >= moveThreshold)
		{
			Vector3 theScale = transform.localScale;
			theScale.x = Mathf.Abs(theScale.x) * Mathf.Sign(move.x);
			transform.localScale = theScale;
		}

		// sync rotation
		var direction = Mathf.Sign(transform.localScale.x);
		var rotation = Quaternion.AngleAxis(rigidbody.velocity.y * 10 * direction, transform.forward);
		transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * 5);
	}


	public void Hurt(float damage)
	{
		base.Hurt(damage - defense);
	}


	public void Feed(float addHP, float addSize)
	{
		hp = Mathf.Clamp(hp + addHP, 0, MaxHP);
		size = Mathf.Clamp(size + addSize, 0, GameSettings.Instance.maxSize);
		additiveScale += addSize * 0.1f;

		var theScale = Vector3.one * additiveScale;
		theScale.x *= Mathf.Sign(transform.localScale.x);
		transform.localScale = theScale;

		SendMessage("OnFeed", SendMessageOptions.DontRequireReceiver);
	}


	public void ChangeBodyState(StateType type)
	{
		int value = (int)type;
		body.SetInteger("State", value);
	}


	public void ChangeState(StateType type)
	{
		if (currentState == type)
			return;

		currentState = type;

		eye.Play("Init");
		int value = (int)type;

		eye.SetInteger("State", value);
		nose.SetInteger("State", value);
		mouth.SetInteger("State", value);
		body.SetInteger("State", value);
		fin.SetInteger("State", value);
	}
}