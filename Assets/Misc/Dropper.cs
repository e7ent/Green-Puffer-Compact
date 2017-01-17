using UnityEngine;
using GreenPuffer.Characters;
using System;

namespace GreenPuffer.Misc
{
    using System.Collections;
    using URandom = UnityEngine.Random;
    class Dropper : MonoBehaviour
    {
        [SerializeField]
        private CharacterBase character = null;
        [SerializeField]
        private GameObject[] prefabs = null;
        //[SerializeField]
        //private GameObject[] effectPrefabs;

        private void Awake()
        {
            character.Killed += OnKilled;
        }

        private void OnKilled(object sender, EventArgs args)
        {
            StartCoroutine(Creating());
        }

        private IEnumerator Creating()
        {
            yield return new WaitForSeconds(0.2f);

            //Instantiate(
            //    effectPrefabs[URandom.Range(0, effectPrefabs.Length)],
            //    transform.position, Quaternion.identity);

            for (int i = 0; i < prefabs.Length; i++)
            {
                var effector = Instantiate(prefabs[i], transform.position, Quaternion.identity);
                var rigid = effector.GetComponent<Rigidbody2D>();
                if (rigid != null)
                {
                    rigid.AddForce(URandom.insideUnitCircle * rigid.mass, ForceMode2D.Impulse);
                    rigid.AddTorque(URandom.Range(-1, 1) * 5);
                }
            }
        }
    }
}