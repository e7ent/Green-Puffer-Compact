using System;
using Astro.Features.Effects;
using UnityEngine;

namespace GreenPuffer.Characters
{
    abstract class CharacterBase : MonoBehaviour, ICharacter
    {
        public event EventHandler Attacked;
        public event EventHandler Damaged;
        public event EventHandler Killed;
        public event EventHandler Grown;
        public event EventHandler<AffectedEventArgs<IAbilitiesModifier>> Affected;

        [SerializeField]
        protected CharacterAbilities abilities = null;
        [Header("Calibration")]
        [SerializeField]
        protected float moveThreshold = 0.01f;
        [Header("Visual")]
        [SerializeField]
        protected bool facingReverse = false;
        [SerializeField]
        protected bool faceToDirection = true;
        protected bool alive;
        protected Animator animator;
        protected new Rigidbody2D rigidbody;

        public bool IsAlive { get { return alive; } }
        public IAbilities Abilities { get { return abilities; } }
        public float Mass { get { return rigidbody.mass; } }

        protected virtual void Awake()
        {
            alive = true;
            rigidbody = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            abilities.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == "Exp")
                {
                    if (Abilities.Exp >= Abilities.MaxExp)
                    {
                        if (Grown != null)
                        {
                            Grown(this, EventArgs.Empty);
                        }
                    }
                }
            };
        }

        protected virtual void Update()
        {
            if (animator != null)
                animator.SetFloat("Speed", rigidbody.velocity.sqrMagnitude);
        }

        public virtual void Move(Vector3 movement)
        {
            if (!IsAlive)
                return;

            if (movement.magnitude > 1)
                movement.Normalize();

            rigidbody.AddForce(movement * Abilities.Speed);

            var speedSq = rigidbody.velocity.sqrMagnitude;
            if (speedSq >= Abilities.Speed * Abilities.Speed)
            {
                rigidbody.velocity = rigidbody.velocity.normalized * Abilities.Speed;
            }

            // face to direction
            if (faceToDirection)
            {
                //var directionSource = rigidbody.velocity.x;
                var directionSource = movement.x;
                if (Mathf.Abs(directionSource) >= moveThreshold)
                {
                    Vector3 theScale = transform.localScale;
                    theScale.x = Mathf.Abs(theScale.x) * Mathf.Sign(directionSource) * (facingReverse ? -1 : 1);
                    transform.localScale = theScale;
                }
            }
        }

        public virtual void Attack(ICharacter target)
        {
            if (!IsAlive)
                return;

            if (animator != null)
                animator.SetTrigger("Attack");

            target.TakeDamage(Abilities.Strength);

            if (Attacked!=null)
            {
                Attacked(this, EventArgs.Empty);
            }
        }

        public virtual void TakeDamage(float damage)
        {
            if (!IsAlive)
                return;

            float realDamage = Mathf.Clamp(damage - Abilities.Armor, 0, float.MaxValue);

            abilities.Hp -= realDamage;

            if (Damaged != null)
            {
                Damaged(this, EventArgs.Empty);
            }

            if (Abilities.Hp <= 0)
            {
                Kill();
            }
        }

        public virtual void Kill()
        {
            if (!IsAlive)
                return;

            if (animator != null)
                animator.SetBool("Dead", true);

            var colliders = GetComponentsInChildren<Collider2D>();
            for (int i = 0; i < colliders.Length; i++)
                colliders[i].enabled = false;

            alive = false;

            if (Killed != null)
            {
                Killed(this, EventArgs.Empty);
            }
        }

        public void TakeEffect(IEffector<IAbilitiesModifier> effector)
        {
            effector.Affect(abilities);
            if (Affected != null)
            {
                Affected(this, new AffectedEventArgs<IAbilitiesModifier>(effector));
            }
        }
    }
}