using System.Collections;
using UnityEngine;
using DG.Tweening;

namespace GreenPuffer.VisualEffects
{
    class DisappearEffect : MonoBehaviour
    {
        [SerializeField]
        private float delay;
        [SerializeField]
        private float duration;

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(delay);

            var renderers = GetComponentsInChildren<Renderer>();
            for (int i = 0; i < renderers.Length; i++)
            {
                var mat = renderers[i].material;
                if (mat.HasProperty("_Color") == false) continue;

                renderers[i].material.DOFade(0, duration);
            }
        }
    }
}