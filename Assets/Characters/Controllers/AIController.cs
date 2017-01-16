using UnityEngine;
using System;

namespace GreenPuffer.Characters.Controllers
{
    using URandom = UnityEngine.Random;
    class AIController : MonoBehaviour
    {
        private enum StateType
        {
            Rest,
            Active,
            Escape,
            Follow,
        }

        [SerializeField]
        private AICharacter character;

        [Header("Movement")]
        [SerializeField]
        private AnimationCurve movementX;
        [SerializeField]
        private AnimationCurve movementY;
        [SerializeField]
        private float activeTimeMin;
        [SerializeField]
        private float activeTimeMax;
        [SerializeField]
        private float restTimeMin;
        [SerializeField]
        private float restTimeMax;

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
            // set state
            SetState(StateType.Active);

            //character.TargetEnter += OnTargetEnter;
            //character.TargetLeave += OnTargetLeave;
            character.Killed += OnKilled;
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
                    restTime = URandom.Range(restTimeMin, restTimeMax);
                    restElapsed = 0;
                    break;
                case StateType.Active:
                    activeTime = URandom.Range(activeTimeMin, activeTimeMax);
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
                    UpdateFollow();
                    break;
            }

            Vector2 viewportPoint = Camera.main.WorldToViewportPoint(transform.position);
            if (Mathf.Abs(viewportPoint.sqrMagnitude) >= 2)
                Destroy(gameObject);
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
            character.Move(movement * Time.fixedDeltaTime);
        }

        private void UpdateFollow()
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

            character.Move(movement);
        }

        private void OnTargetEnter(object sender, AICharacterEventArgs args)
        {
            target = args.Player.transform;

            var player = target.GetComponent<PlayerCharacter>();
            var sizeSubtract = character.Mass - player.Mass;

            if (sizeSubtract > 0)
            {
                SendMessage("OnFollow", target, SendMessageOptions.DontRequireReceiver);
                SetState(StateType.Follow);
            }
            else
            {
                SendMessage("OnEscape", target, SendMessageOptions.DontRequireReceiver);
                SetState(StateType.Escape);
            }

            direction.x = (transform.position - target.position).normalized.x * Mathf.Sign(character.Mass - player.Mass) * -1;
        }

        private void OnTargetLeave(object sender, AICharacterEventArgs args)
        {
            if (target != args.Player.transform)
                return;

            if (state != StateType.Escape && state != StateType.Follow)
                return;

            target = null;
            SetState(StateType.Active);
        }

        private void OnCollisionEnter2D(Collision2D coll)
        {
            if (coll.gameObject.CompareTag("Creature"))
            {
                var other = coll.gameObject.GetComponent<AIController>();
                direction.x = other.direction.x = URandom.Range(0, 2) == 0 ? -1 : 1;
                state = other.state = StateType.Active;
            }

            if (coll.gameObject.CompareTag("Player") == false)
                return;

            var target = coll.gameObject.GetComponent<PlayerCharacter>();
            if (character.Mass - target.Mass > 0)
                target.TakeDamage(character.Abilities.Strength);
            else
                character.TakeDamage(target.Abilities.Strength);
        }

        private void OnKilled(object sender, EventArgs args)
        {
            character.Kill();
        }
    }
}