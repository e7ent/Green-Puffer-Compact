using GreenPuffer.Accounts;
using GreenPuffer.Characters;
using System;
using UnityEngine;

namespace GreenPuffer.Misc
{
    class KillingTrigger : MonoBehaviour
    {
        [SerializeField]
        private CharacterBase character;
        [SerializeField]
        private string key;

        private void Awake()
        {
            character.Killed += OnKilled;
        }

        private void OnKilled(object sender, EventArgs e)
        {
            Users.LocalUser.Counter[key]++;
        }
    }
}
