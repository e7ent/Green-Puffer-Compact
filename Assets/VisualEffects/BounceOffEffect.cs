using UnityEngine;

namespace GreenPuffer.VisualEffects
{
    class BounceOffEffect : MonoBehaviour
    {
        [SerializeField]
        private string targetTag = "Creature";

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
            rigidbody.AddForce(dir.normalized * coll.relativeVelocity.magnitude * .5f, ForceMode2D.Impulse);
        }
    }
}