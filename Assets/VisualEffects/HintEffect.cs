using UnityEngine;
using E7Assets;

namespace GreenPuffer.VisualEffects
{
    class HintEffect : MonoBehaviour
    {
        [SerializeField]
        private GameObject effectPrefab;
        private float lastCreateTime;

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