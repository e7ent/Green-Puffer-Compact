using System;
using UnityEngine;
using UnityEngine.UI;

#pragma warning disable 0649
namespace GreenPuffer.UI
{
    class Alert : MonoBehaviour
    {
        [SerializeField]
        private Canvas canvas;
        [SerializeField]
        private Text messageText;
        [SerializeField]
        private Button yes, no;

        private Action yesCallback, noCallback;

        private void Awake()
        {
            yes.onClick.AddListener(OnYesClicked);
            no.onClick.AddListener(OnNoClicked);
        }

        public void Show(string message, Action yesCallback = null, Action noCallback = null)
        {
            messageText.text = message;

            no.gameObject.SetActive(noCallback != null);
            this.yesCallback = yesCallback;
            this.noCallback = noCallback;
            canvas.enabled = true;
        }

        private void Invoke(Action callback)
        {
            if (callback != null)
            {
                callback();
            }
            canvas.enabled = false;
        }


        private void OnYesClicked()
        {
            Invoke(yesCallback);
        }

        private void OnNoClicked()
        {
            Invoke(noCallback);
        }
    }
}