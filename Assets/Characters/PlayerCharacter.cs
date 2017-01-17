using GreenPuffer.Accounts;
using System;
using UnityEngine;

namespace GreenPuffer.Characters
{
    using Astro.Features.Effects;
    using DG.Tweening;
    using System.ComponentModel;
    using UnityEngine.SceneManagement;
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
                transform.localScale = scale;
            }
        }

        private void OnGrown(object sender, EventArgs e)
        {
            if (nextCharacters.Length <= 0)
                return;
            var prefab = nextCharacters[URandom.Range(0, nextCharacters.Length)];
            Selected = prefab;
            User.LocalUser.CharacterCollection.Unlock(prefab);
            SceneManager.LoadScene("Game");
        }

        protected override void Update()
        {
            abilities.Hp -= Time.deltaTime;

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

        public static event EventHandler SelectionChanged;
        public static PlayerCharacter Selected
        {
            get
            {
                var stored = PlayerPrefs.GetString("SelectedPlayerCharacter");
                if (string.IsNullOrEmpty(stored))
                {
                    stored = "Baby";
                }
                return Resources.Load<PlayerCharacter>("PlayerCharacters/" + stored);
            }

            set
            {
                PlayerPrefs.SetString("SelectedPlayerCharacter", value.name);
                PlayerPrefs.Save();
                if (SelectionChanged != null)
                {
                    SelectionChanged(null, EventArgs.Empty);
                }
            }
        }

        public static PlayerCharacter Playing { get; private set; }
        #endregion
    }
}