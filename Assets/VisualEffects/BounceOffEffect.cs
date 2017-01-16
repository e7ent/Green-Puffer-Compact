using UnityEngine;

namespace GreenPuffer.VisualEffects
{
    class BounceOffEffect : MonoBehaviour
    {
        [SerializeField]
        private string targetTag = "Creature";
        [SerializeField]
        private float force = 10;

        private new Rigidbody2D rigidbody;

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody2D>();
        }

        private void OnCollisionEnter2D(Collision2D coll)
        {
            if (!coll.gameObject.CompareTag(targetTag))
                return;

            Vector2 dir = transform.position - coll.transform.position;
            rigidbody.AddForce(dir.normalized * force, ForceMode2D.Impulse);
        }
    }
}