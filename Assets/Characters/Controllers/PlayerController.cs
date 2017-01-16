using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityStandardAssets.CrossPlatformInput;

namespace GreenPuffer.Characters.Controllers
{
    class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private PlayerCharacter character;

        private void Awake()
        {
        }

        private void FixedUpdate()
        {
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            float v = CrossPlatformInputManager.GetAxis("Vertical");

            character.Move(new Vector2(h, v));
        }

        private void OnFeed()
        {
            transform.DOKill(true);
            transform.DOPunchScale(Vector3.one * 0.2f, 0.4f);
        }

        private IEnumerator OnKill()
        {
            yield return null;
            //GameManager.Instance.Finish();
        }
    }
}