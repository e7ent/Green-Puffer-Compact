using UnityEngine;
using GreenPuffer.Characters;
using System;

namespace GreenPuffer.VisualEffects
{
    using URandom = UnityEngine.Random;
    class DamagedEffect : MonoBehaviour
    {
        [SerializeField]
        private CharacterBase character;
        [SerializeField]
        private GameObject[] effectPrefabs;

        private void Awake()
        {
            character.Damaged += OnDamaged;
        }

        private void OnDamaged(object sender, EventArgs e)
        {
            Instantiate(GetRandomPrefab(), transform.position, Quaternion.identity);
        }

        private GameObject GetRandomPrefab()
        {
            return effectPrefabs[URandom.Range(0, effectPrefabs.Length)];
        }
    }
}