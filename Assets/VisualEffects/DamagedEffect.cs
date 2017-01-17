using UnityEngine;
using GreenPuffer.Characters;
using System;

namespace GreenPuffer.VisualEffects
{
    using DG.Tweening;
    using System.Collections.Generic;
    using URandom = UnityEngine.Random;
    class DamagedEffect : MonoBehaviour
    {
        [SerializeField]
        private CharacterBase character;
        [SerializeField]
        private Color color = Color.red;
        [SerializeField]
        private GameObject[] effectPrefabs;

        private Renderer[] renderers;
        private List<Tween> tweens = new List<Tween>();

        private void Awake()
        {
            renderers = GetComponentsInChildren<Renderer>();
            character.Killed += OnKilled;
            character.Damaged += OnDamaged;
        }

        private void OnKilled(object sender, EventArgs e)
        {
            Instantiate(GetRandomPrefab(), transform.position, Quaternion.identity);
        }

        private void OnDamaged(object sender, EventArgs e)
        {
            for (int i = 0; i < tweens.Count; i++)
                tweens[i].Kill(true);

            tweens.Clear();

            for (int i = 0; i < renderers.Length; i++)
            {
                var mat = renderers[i].material;
                if (mat.HasProperty("_Color") == false) continue;

                var seq = DOTween.Sequence();
                for (int j = 0; j < 3; j++)
                {
                    seq.Append(renderers[i].material.DOColor(color, 0.1f));
                    seq.Append(renderers[i].material.DOColor(Color.white, 0.1f));
                }
                tweens.Add(seq);
            }

            transform.DOKill(true);
            transform.DOPunchScale(Vector3.one * 0.2f, 0.4f);
            transform.DOShakeRotation(0.2f, 20);
        }

        private GameObject GetRandomPrefab()
        {
            return effectPrefabs[URandom.Range(0, effectPrefabs.Length)];
        }
    }
}