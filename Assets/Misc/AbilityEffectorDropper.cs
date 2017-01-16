using UnityEngine;
using GreenPuffer.Characters;
using System;

namespace GreenPuffer.Misc
{
    using URandom = UnityEngine.Random;
    class AbilityEffectorDropper : MonoBehaviour
    {
        [SerializeField]
        private CharacterBase character;
        [SerializeField]
        private AbilityEffectorBase[] prefabs;
        [SerializeField]
        private GameObject[] effectPrefabs;

        private void Awake()
        {
            character.Killed += OnKilled;
        }

        private void OnKilled(object sender, EventArgs args)
        {
            //yield return new WaitForSeconds(0.1f);

            Instantiate(
                effectPrefabs[URandom.Range(0, effectPrefabs.Length)],
                transform.position, Quaternion.identity);

            for (int i = 0; i < prefabs.Length; i++)
            {
                var effector = Instantiate(prefabs[i], transform.position, Quaternion.identity);
                var rigid = effector.GetComponent<Rigidbody2D>();
                if (rigid != null)
                {
                    rigid.AddForce(URandom.insideUnitCircle * effector.transform.localScale.magnitude, ForceMode2D.Impulse);
                    rigid.AddTorque(URandom.Range(-1, 1) * 5);
                }
            }
        }
    }
}