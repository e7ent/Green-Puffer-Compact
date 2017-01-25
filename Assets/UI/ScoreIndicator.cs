using GreenPuffer.Misc;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

namespace GreenPuffer.UI
{
    class ScoreIndicator : MonoBehaviour
    {
        [SerializeField]
        private Text scoreText;

        private void Awake()
        {
            GameManager.Instance.ScoreKeeper.PropertyChanged += OnPropertyChanged;
            UpdateUI();
        }

        private void OnDestroy()
        {
            GameManager.Instance.ScoreKeeper.PropertyChanged -= OnPropertyChanged;
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Current")
            {
                UpdateUI();
            }
        }

        private void UpdateUI()
        {
            scoreText.text = GameManager.Instance.ScoreKeeper.Current.ToString("#,##0");
        }
    }
}