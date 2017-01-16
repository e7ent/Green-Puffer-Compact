using UnityEngine;
using GreenPuffer.Characters;

namespace GreenPuffer.Misc
{
    class AbilityEffectorBase : MonoBehaviour, IAbilityEffector
    {
        [SerializeField]
        private string triggingTag;
        [SerializeField]
        private CharacterAbilities abilities;
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

            character.TakeEffect(this);
        }

        public void Affect(IAbilitiesModifier modifier)
        {
            Instantiate(effectPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}