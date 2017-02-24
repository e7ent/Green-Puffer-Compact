using DG.Tweening;
using GreenPuffer.Accounts;
using System;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GreenPuffer.Characters
{
    using Misc;
    using URandom = UnityEngine.Random;
    sealed class PlayerCharacter : CharacterBase
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

        /// 아래 데이터들은 모두 Model에 해당된다.
        /// PlayerCharacter클래스는 View에 해당한다.
        /// PlayerCharacterController는 Controller에 해당한다.
        /// 하지만 PlayerCharacter는 Model과 의존성이 매우 높다.
        /// 의존성을 낮추게 되면 PlayerCharacter라는 이름이 맞지 않는다.
        /// CharacterView
        /// CharacterModel
        /// CharacterController 
        /// Input은 View이다.
        /// Transform, Rigidbody등의 Unity Component는 View이다. (Server에 존재하지 않는다는게 증거)
        /// Stat, Ability등만으로는 nickname, icon, story...등의 데이터를 감당하지 못한다.
        /// 보통의 프로그램과 달리 게임에 MVC를 적용하기 위해서는
        /// N:1:1의 관계가 아닌 N:N:1의 구조가 되어야 하는것 같다.
        [Header("Infomation")]
        [SerializeField]
        private string nickName;
        [SerializeField]
        private string description;
        [SerializeField]
        private Sprite thumbnail;
        [SerializeField]
        private PlayerCharacter[] nextCharacters;
        [Header("Animator")]
        [SerializeField]
        private Animator eye;
        [SerializeField]
        private Animator nose;
        [SerializeField]
        private Animator mouth;
        [SerializeField]
        private Animator body;
        [SerializeField]
        private Animator fin;

        private Vector2 originScale;

        public string NickName { get { return nickName; } }
        public string Description { get { return description; } }
        public Sprite Thumbnail { get { return thumbnail; } }

        private StateType currentState;

        protected override void Awake()
        {
            base.Awake();
            currentState = StateType.Normal;
            animator = null;
            if (Playing == null)
            {
                Playing = this;
            }
            originScale = transform.localScale;
            Abilities.PropertyChanged += OnAbilitiesPropertyChanged;
            Grown += OnGrown;
        }

        private void OnAbilitiesPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Exp")
            {
                transform.DOComplete(true);
                var scale = originScale;
                scale += Vector2.one * (Abilities.Exp / Abilities.MaxExp);
                scale.x *= Mathf.Sign(transform.localScale.x);
                transform.localScale = scale;
            }
        }

        private void OnGrown(object sender, EventArgs e)
        {
            if (nextCharacters.Length <= 0)
                return;
            var prefab = nextCharacters[URandom.Range(0, nextCharacters.Length)];
            Users.LocalUser.SelectedCharacter = prefab;
            Users.LocalUser.CreateCharacter(prefab);
            GameInitializer._prevScoreKeeper = GameManager.Instance.ScoreKeeper;
            SceneManager.LoadScene("Game");
        }

        protected override void Update()
        {
            TakeDamage(Time.deltaTime + Abilities.Armor);

            fin.speed = Mathf.Clamp(rigidbody.velocity.sqrMagnitude * 5.0f, 0.5f, 2);

            Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
            pos.x = Mathf.Clamp(pos.x, 0, 1);
            pos.y = Mathf.Clamp(pos.y, 0, 1);
            transform.position = Camera.main.ViewportToWorldPoint(pos);
        }


        public override void Move(Vector3 movement)
        {
            base.Move(movement);
            //if (alive == false)
            //    return;

            //if (movement.magnitude > 1)
            //    movement.Normalize();

            //rigidbody.AddForce(movement * Time.deltaTime);

            //// sync face
            //if (Mathf.Abs(movement.x) >= moveThreshold)
            //{
            //    Vector3 theScale = transform.localScale;
            //    theScale.x = Mathf.Abs(theScale.x) * Mathf.Sign(movement.x);
            //    transform.localScale = theScale;
            //}

            // sync rotation
            var direction = Mathf.Sign(transform.localScale.x);
            var rotation = Quaternion.AngleAxis(rigidbody.velocity.y * 10 * direction, transform.forward);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * 5);
        }

        //public void Feed(float addHP, float addSize)
        //{
        //    HP += addHP;
        //    size = Mathf.Clamp(size + addSize, 0, 3);// GameSettings.Instance.maxSize);

        //    var theScale = Vector3.one * addSize;
        //    theScale.x *= Mathf.Sign(transform.localScale.x);
        //    transform.localScale += theScale;

        //    SendMessage("OnFeed", SendMessageOptions.DontRequireReceiver);
        //}

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

        #region Statics
        public static PlayerCharacter Playing { get; private set; }
        #endregion
    }
}