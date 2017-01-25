using UnityEngine;

namespace GreenPuffer.Misc
{
    class ScoreAdder : MonoBehaviour
    {
        [SerializeField]
        private bool triggerMode;
        [SerializeField]
        private string triggingTag;
        [SerializeField]
        private int score;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (triggerMode)
            {
                return;
            }
            else
            {
                Use(collision.collider);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (triggerMode)
            {
                Use(other);
            }
            else
            {
                return;
            }
        }

        private void Use(Collider2D target)
        {
            if (target == null)
                return;
            if (!target.CompareTag(triggingTag))
                return;

            GameManager.Instance.ScoreKeeper.Add(score);
        }
    }
}
