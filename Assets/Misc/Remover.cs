using System.Collections.Generic;
using UnityEngine;

namespace GreenPuffer.Misc
{
    public class Remover : MonoBehaviour
    {
        [SerializeField]
        private Camera mainCamera;
        [SerializeField]
        private List<string> detectingTags;
        private BoxCollider2D dectectingCollider;

        private int width, height;

        private void Update()
        {
            if (dectectingCollider == null)
            {
                dectectingCollider = gameObject.AddComponent<BoxCollider2D>();
                dectectingCollider.isTrigger = true;
            }

            if (width == Screen.width && height == Screen.height)
                return;

            width = Screen.width;
            height = Screen.height;
            dectectingCollider.size = mainCamera.ViewportToWorldPoint(Vector3.one) - mainCamera.ViewportToWorldPoint(Vector3.zero);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (!detectingTags.Contains(other.tag))
                return;

            Destroy(other.gameObject);
        }
    }
}