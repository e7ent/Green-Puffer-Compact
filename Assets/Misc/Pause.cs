using UnityEngine;

namespace GreenPuffer.Misc
{
    class Pause : MonoBehaviour
    {
        private static Pause obj;
        public static bool IsPause { get; private set; }

        [SerializeField]
        private Behaviour behaviour;

        private void Awake()
        {
            if (obj != null)
            {
                DestroyImmediate(gameObject);
            }
            obj = this;
            IsPause = false;
        }

        private void Update()
        {
            IsPause = behaviour.isActiveAndEnabled;
        }
    }
}