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
            Users.LocalUser.PropertyChanged += OnUserPropertyChanged;
            UpdateUI();
        }

        private void OnDestroy()
        {
            Users.LocalUser.PropertyChanged -= OnUserPropertyChanged;
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
            coinText.text = Users.LocalUser.Coin.ToString("#,##0");
        }
    }
}