using GreenPuffer.Accounts;
using GreenPuffer.Synthesis;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

namespace GreenPuffer.UI
{
    class CharacterSynthesisViewer : MonoBehaviour
    {
        [SerializeField]
        private Table table;
        [SerializeField]
        private CharacterInfomationViewer infomationViewer;
        [SerializeField]
        private Alert alert;
        [SerializeField]
        private Slider slider;
        private Synthesizer synthesizer;

        private void Awake()
        {
            Users.LocalUser.PropertyChanged += OnPropertyChanged;
        }

        private void OnEnable()
        {
            table.Reload(Users.LocalUser.Characters);
            synthesizer = Users.LocalUser.CreateSynthesizer();
            UpdateSlider();
        }

        private void OnDestroy()
        {
            Users.LocalUser.PropertyChanged -= OnPropertyChanged;
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Characters")
            {
                OnEnable();
            }
        }

        private void UpdateSlider()
        {
            slider.value = synthesizer.TotalWeight;
        }

        public void OnCellClicked(CharacterSelectionTableCell cell)
        {
            var c = cell.Character;
            if (cell.IsSelected)
            {
                synthesizer.Characters.Add(c);
            }
            else
            {
                synthesizer.Characters.Remove(c);
            }
            UpdateSlider();
        }

        public void OnClicked()
        {
            if (Users.LocalUser.Coin < 100)
            {
                alert.Show("100코인이 필요합니다.");
                return;
            }

            if (synthesizer.Characters.Count <= 1)
            {
                alert.Show("2마리 이상 선택해주세요.");
                return;
            }

            var result = synthesizer.Synthesize();
            if (result == null)
                return;
            Users.LocalUser.Coin -= 100;
            infomationViewer.Apply(result);
        }
    }
}
