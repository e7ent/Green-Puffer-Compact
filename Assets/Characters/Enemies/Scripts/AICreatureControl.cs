using UnityEngine;
using System.Collections;

public class AICreatureControl : MonoBehaviour
{
	private enum StateType
	{
		Rest,
		Active,
		Escape,
		Follow,
	}


	[Header("Movement")]
	[SerializeField] private AnimationCurve movementX;
	[SerializeField] private AnimationCurve movementY;
	[SerializeField] private float activeTimeMin;
	[SerializeField] private float activeTimeMax;
	[SerializeField] private float restTimeMin;
	[SerializeField] private float restTimeMax;
	[SerializeField] private ForceMode2D forceMode = ForceMode2D.Force;


	private CreatureCharacter character;
	private Rigidbody2D rigidbody;
	private StateType state;
	private Transform target;
	private Vector2 direction;
	private float moveElapsed;
	private float activeTime;
	private float activeElapsed;
	private float restTime;
	private float restElapsed;


	private void Awake()
	{
		// get components
		character = GetComponent<CreatureCharacter>();
		rigidbody = GetComponent<Rigidbody2D>();

		// set state
		SetState(StateType.Active);
	}


	private void Start()
	{
		// set direction
		var viewportPoint = Camera.main.WorldToViewportPoint(transform.position);
		direction.x = Mathf.Sign(viewportPoint.x) * -1;
		direction.y = 1;
	}


	private void SetState(StateType type)
	{
		state = type;
		switch (state)
		{
			case StateType.Rest:
				restTime = Random.Range(restTimeMin, restTimeMax);
				restElapsed = 0;
				break;
			case StateType.Active:
				activeTime = Random.Range(activeTimeMin, activeTimeMax);
				activeElapsed = 0;
				break;
			case StateType.Escape:
				break;
			case StateType.Follow:
				break;
			default:
				break;
		}
	}


	private void FixedUpdate()
	{
		switch (state)
		{
			case StateType.Rest:
				UpdateRest();
				break;
			case StateType.Active:
				UpdateMove();
				break;
			case StateType.Escape:
			case StateType.Follow:
				UpdateSpecialMove();
				break;
		}
	}


	private void UpdateRest()
	{
		if (restElapsed > restTime)
		{
			restElapsed = 0;
			SetState(StateType.Active);
			return;
		}

		restElapsed += Time.deltaTime;
	}


	private void UpdateMove()
	{
		if (activeElapsed > activeTime)
		{
			activeElapsed = 0;
			SetState(StateType.Rest);
			return;
		}

		activeElapsed += Time.deltaTime;
		moveElapsed += Time.deltaTime;

		var movement = new Vector2(movementX.Evaluate(moveElapsed), movementY.Evaluate(moveElapsed));
		movement.Scale(direction);

		character.Move(movement, forceMode);
	}


	private void UpdateSpecialMove()
	{
		if (target == null && activeElapsed > activeTime)
		{
			activeElapsed = 0;
			SetState(StateType.Rest);
			return;
		}

		activeElapsed += Time.deltaTime;
		moveElapsed += Time.deltaTime;

		var movement = new Vector2(movementX.Evaluate(moveElapsed), movementY.Evaluate(moveElapsed));

		if (state == StateType.Escape)
		{
			movement.Scale((transform.position - target.position).normalized);
		}
		else
		{
			movement.Scale((target.position - transform.position).normalized);
		}

		character.Move(movement, forceMode);
	}


	private void OnTargetFind(Transform target)
	{
		this.target = target;

		var player = target.GetComponent<PlayerCharacter>();

		if (character.Size - player.Size > 0)
			SetState(StateType.Follow);
		else
			SetState(StateType.Escape);

		direction.x = (transform.position - target.position).normalized.x;
	}


	private void OnTargetLost(Transform target)
	{
		if (this.target != target)
			return;

		if (state != StateType.Escape && state != StateType.Follow)
			return;

		target = null;
		SetState(StateType.Active);
	}


	public void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.CompareTag("Player") == false)
			return;

		var target = coll.gameObject.GetComponent<PlayerCharacter>();
		if (character.Size - target.Size > 0)
			target.Hurt(character.Damage);
		else
			character.Hurt(target.Damage);
	}


	IEnumerator OnHurt(float hp)
	{
		yield return null;
		if (hp <= 0)
			character.Kill();
	}
}
