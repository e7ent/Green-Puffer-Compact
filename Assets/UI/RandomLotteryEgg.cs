using UnityEngine;
using System.Collections;
using GreenPuffer.Accounts;
using Astro.Features.Effects;
using System.Linq;
using UnityEngine.UI;
using DG.Tweening;


namespace GreenPuffer.UI
{
    class RandomLotteryEgg : MonoBehaviour, IEffector<CoinBankAccount>
    {
        [SerializeField]
        private Image effectImage;
        [SerializeField]
        private CharacterInfomationViewer infomation;

        public void OnClicked()
        {
            StartCoroutine(Buy());
        }

        public IEnumerator Buy()
        {
            if (User.LocalUser.Coin < 100)
                yield break;

            float fadeDuration = 0.3f;
            yield return effectImage.DOColor(Color.white, fadeDuration).SetEase(ease: Ease.OutExpo).WaitForCompletion();
            yield return effectImage.DOColor(Color.black, fadeDuration).WaitForCompletion();
            yield return effectImage.DOColor(Color.white, fadeDuration).WaitForCompletion();
            yield return effectImage.DOColor(Color.black, fadeDuration).WaitForCompletion();
            yield return effectImage.DOColor(Color.clear, fadeDuration).WaitForCompletion();

            User.LocalUser.TakeEffect(this);
        }

        public void Affect(CoinBankAccount modifier)
        {
            if (modifier.Withdraw(100))
            {
                var characters = User.LocalUser.CharacterCollection.AllCharacters;
                var array = characters.ToArray();
                var prefab = array[UnityEngine.Random.Range(0, array.Length)];
                User.LocalUser.CharacterCollection.Unlock(prefab);
                infomation.Apply(prefab);
            }
        }
    }
}