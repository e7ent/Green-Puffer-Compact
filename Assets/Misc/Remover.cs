using UnityEngine;

namespace GreenPuffer.Misc
{
    public class Remover : MonoBehaviour
    {
        [SerializeField]
        private string ignoreTag;
        [SerializeField]
        private Camera mainCamera;
        [SerializeField]
        private BoxCollider2D dectectingCollider;

        private int width, height;

        private void Update()
        {
            if (width == Screen.width && height == Screen.height)
                return;

            width = Screen.width;
            height = Screen.height;
            dectectingCollider.size = mainCamera.ViewportToWorldPoint(Vector3.one) - mainCamera.ViewportToWorldPoint(Vector3.zero);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag(ignoreTag))
                return;

            Destroy(other.gameObject);
        }
    }
}