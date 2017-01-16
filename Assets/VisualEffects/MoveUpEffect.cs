using UnityEngine;

namespace GreenPuffer.VisualEffects
{
    class MoveUpEffect : MonoBehaviour
    {
        [SerializeField]
        private float duration = 0.5f;

        private void Start()
        {
            Destroy(gameObject, duration);
        }

        private void Update()
        {
            transform.Translate(0, Time.deltaTime * 0.5f, 0);
        }
    }
}