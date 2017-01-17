using GreenPuffer.Accounts;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

namespace GreenPuffer.UI
{
    public class CoinIndicator : MonoBehaviour
    {
        [SerializeField]
        private Text coinText;

        private void Awake()
        {
            User.LocalUser.PropertyChanged += OnUserPropertyChanged;
            UpdateUI();
        }

        private void OnDestroy()
        {
            User.LocalUser.PropertyChanged -= OnUserPropertyChanged;
        }

        private void OnUserPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Coin")
            {
                UpdateUI();
            }
        }

        private void UpdateUI()
        {
            coinText.text = User.LocalUser.Coin.ToString("#,##0");
        }
    }
}