using DG.Tweening;
using GreenPuffer.Accounts;
using GreenPuffer.Characters;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


#pragma warning disable 0649
namespace GreenPuffer.UI
{
    class RandomLotteryEgg : MonoBehaviour
    {
        [SerializeField]
        private Image effectImage;
        [SerializeField]
        private CharacterInfomationViewer infomation;
        [SerializeField]
        private Alert alert;

        private Coroutine _coroutine;

        public void OnClicked()
        {
            if (_coroutine == null)
            {
                _coroutine = StartCoroutine(Buy());
            }
        }

        public IEnumerator Buy()
        {
            if (Users.LocalUser.Coin < 100)
            {
                alert.Show("100코인이 필요합니다.");
            }
            else
            {
                float fadeDuration = 0.3f;
                yield return effectImage.DOColor(Color.white, fadeDuration).SetEase(ease: Ease.OutExpo).WaitForCompletion();
                yield return effectImage.DOColor(Color.black, fadeDuration).WaitForCompletion();
                yield return effectImage.DOColor(Color.white, fadeDuration).WaitForCompletion();
                yield return effectImage.DOColor(Color.black, fadeDuration).WaitForCompletion();
                yield return effectImage.DOColor(Color.clear, fadeDuration).WaitForCompletion();

                var characters = CharacterDatabase.GetAllPlayerCharacters();
                var array = characters.ToArray();
                var prefab = array[Random.Range(0, array.Length)];
                Users.LocalUser.CreateCharacter(prefab);
                Users.LocalUser.Coin -= 100;
                infomation.Apply(prefab);
                _coroutine = null;
            }
        }
    }
}