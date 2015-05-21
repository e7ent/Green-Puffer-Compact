using UnityEngine;
using System.Collections;

public class CreatureCharacter : MonoBehaviour
{
	[Header("Stat")]
	[SerializeField] protected float hp = 10;
	[SerializeField] protected float maxHP = 10;
	[SerializeField] protected float damage = 1;
	[SerializeField] protected float defense = 0;
	[SerializeField] protected float luck = 0.1f;
	[SerializeField] protected float force = 10;
	[SerializeField] protected float size = 1;


	[Header("Sync Directioin")]
	[SerializeField] protected float moveThreshold = 0.01f;
	[SerializeField] protected bool facingReverse = false;


	protected bool isAlive;
	protected new Rigidbody2D rigidbody;
	protected Animator animator;
	protected Bounds bounds;


	public bool IsAlive { get { return isAlive; } }
	public float HP
	{
		get { return hp; }
		protected set
		{
			hp = Mathf.Clamp(value, 0, MaxHP);

			if (hp <= 0)
				Kill();
		}
	}
	public float MaxHP { get { return maxHP; } }
	public float Damage { get { return damage; } }
	public float Defense { get { return defense; } }
	public float Luck { get { return luck; } }
	public float Size { get { return size; } }


	protected virtual void Awake()
	{
		isAlive = true;
		rigidbody = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();

		// get bounds
		var renderers = GetComponentsInChildren<Renderer>();
		if (renderers.Length > 0)
		{
			bounds = renderers[0].bounds;
			for (int i = 1; i < renderers.Length; i++)
				bounds.Encapsulate(renderers[i].bounds);
		}
	}


	protected virtual void Update()
	{
		if (animator != null)
			animator.SetFloat("Speed", rigidbody.velocity.sqrMagnitude);

		//var position = transform.position;
		//// draw gizmos 그려보자
		////position.x -= Mathf.Sign(position.x) * bounds.size.x * 2;
		////position.y -= Mathf.Sign(position.y) * bounds.size.y * 2;
		//position = Camera.main.WorldToViewportPoint(position);
		//if (position.x <= 0 || position.x >= 1 || position.y <= 0 || position.y >= 1)
		//	Destroy(gameObject);
	}


	public virtual void Move(Vector3 move, ForceMode2D forceMode = ForceMode2D.Force)
	{
		if (isAlive == false)
			return;

		if (move.magnitude > 1)
			move.Normalize();

		rigidbody.AddForce(move * force, forceMode);

		// sync face
		if (Mathf.Abs(rigidbody.velocity.x) >= moveThreshold)
		{
			Vector3 theScale = transform.localScale;
			theScale.x = Mathf.Abs(theScale.x) * Mathf.Sign(rigidbody.velocity.x) * (facingReverse ? -1 : 1);
			transform.localScale = theScale;
		}
	}


	public virtual void Attack(PlayerCharacter target)
	{
		if (isAlive == false)
			return;

		if (animator != null)
			animator.SetTrigger("Attack");

		target.Hurt(damage);

		SendMessage("OnAttack", SendMessageOptions.DontRequireReceiver);
	}


	public virtual void Hurt(float damage)
	{
		if (isAlive == false)
			return;

		if (damage <= 0)
			return;

		if (animator != null)
			animator.SetTrigger("Hurt");

		HP -= damage;

		SendMessage("OnHurt", hp, SendMessageOptions.DontRequireReceiver);
	}


	public virtual void Kill()
	{
		if (isAlive == false)
			return;

		// play animation...
		if (animator != null)
			animator.SetBool("Dead", true);
		
		// disable colliders...
		var colliders = GetComponentsInChildren<Collider2D>();
		for (int i = 0; i < colliders.Length; i++)
			colliders[i].enabled = false;

		// set alive...
		isAlive = false;

		SendMessage("OnKill", SendMessageOptions.DontRequireReceiver);
		Destroy(gameObject, 0.3f);
	}


	public virtual void OnTriggerEnter2D(Collider2D other)
	{
		if (isAlive == false)
			return;
		if (other.CompareTag("Player") == false)
			return;

		SendMessage("OnTargetFind", other.transform, SendMessageOptions.DontRequireReceiver);
	}


	public virtual void OnTriggerExit2D(Collider2D other)
	{
		if (isAlive == false)
			return;
		if (other.CompareTag("Player") == false)
			return;

		SendMessage("OnTargetLost", other.transform, SendMessageOptions.DontRequireReceiver);
	}
}
