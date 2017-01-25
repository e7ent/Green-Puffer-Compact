using Astro.Features.Effects;
using GreenPuffer.Accounts;
using GreenPuffer.Characters;
using UnityEngine;

namespace GreenPuffer.Effectors
{
    class CoinEffector : MonoBehaviour
    {
        [SerializeField]
        private string triggingTag;
        [SerializeField]
        private int coin;
        [SerializeField]
        private GameObject effectPrefab;

        private void OnTriggerEnter2D(Collider2D other)
        {
            Use(other);
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            Use(other);
        }

        private void Use(Collider2D target)
        {
            if (target == null)
                return;
            if (!target.CompareTag(triggingTag))
                return;

            var character = target.GetComponent<PlayerCharacter>();
            if (character == null)
                return;

            Users.LocalUser.Coin += coin;

            Instantiate(effectPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}