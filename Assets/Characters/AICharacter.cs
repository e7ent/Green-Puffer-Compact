using System;
using UnityEngine;

namespace GreenPuffer.Characters
{
    class AICharacter : CharacterBase
    {
        public event EventHandler<AICharacterEventArgs> TargetEnter;
        public event EventHandler<AICharacterEventArgs> TargetLeave;

        protected override void Awake()
        {
            base.Awake();
            Killed += OnKilled;
        }

        private void OnKilled(object sender, EventArgs e)
        {
            Destroy(gameObject, 0.3f);
        }

        public virtual void OnTriggerEnter2D(Collider2D other)
        {
            if (!alive)
                return;
            if (other.CompareTag("Player") == false)
                return;

            if (TargetEnter != null)
            {
                TargetEnter(this, new AICharacterEventArgs(other.GetComponent<PlayerCharacter>()));
            }
        }


        public virtual void OnTriggerExit2D(Collider2D other)
        {
            if (!alive)
                return;
            if (other.CompareTag("Player") == false)
                return;

            if (TargetLeave != null)
            {
                TargetLeave(this, new AICharacterEventArgs(other.GetComponent<PlayerCharacter>()));
            }
        }
    }
}
