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

	private StateType currentState;

	protected override void Awake()
	{
		base.Awake();
		currentState = StateType.Normal;
	}


	protected override void Update()
	{
		fin.speed = Mathf.Clamp(rigidbody.velocity.sqrMagnitude * 5.0f, 0.5f, 2);
	}


	public override void Move(Vector3 move, ForceMode2D forceMode = ForceMode2D.Force)
	{
		base.Move(move, forceMode);

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
		hp += Mathf.Clamp(addHP, 0, maxHP);
		size += Mathf.Clamp(addSize, 0, GameSettings.Instance.maxSize);

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