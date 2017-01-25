using GreenPuffer.Characters;
using GreenPuffer.Misc;
using System;
using UnityEngine;

namespace GreenPuffer.VisualEffects
{
    class HintEffect : MonoBehaviour
    {
        [SerializeField]
        private PlayerCharacter character;
        [SerializeField]
        private GameObject effectPrefab;

        private float lastCreateTime;

        private void Awake()
        {
            character.Damaged += OnEvent;
            character.Damaged += OnEvent;
        }

        private void OnEvent(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OnFollow()
        {
            if (Time.time - lastCreateTime < 1)
                return;

            lastCreateTime = Time.time;

            var bounds = gameObject.CalculateBounds();
            var position = transform.position;
            position.y += bounds.size.y;
            Instantiate(effectPrefab).transform.position = position;
        }
    }
}