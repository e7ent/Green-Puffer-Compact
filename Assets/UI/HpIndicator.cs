using GreenPuffer.Characters;
using System;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

namespace GreenPuffer.UI
{
    public class HpIndicator : MonoBehaviour
    {
        [SerializeField]
        private Slider slider;
        private CharacterBase character;

        private void Update()
        {
            if (character == null)
            {
                character = PlayerCharacter.Playing;
                if (character != null)
                {
                    Initialize();
                }
            }
        }

        private void Initialize()
        {
            character.Damaged += OnDamaged;
            character.Abilities.PropertyChanged += OnUserPropertyChanged;
            UpdateUI();
        }

        private void OnDamaged(object sender, EventArgs e)
        {
            UpdateUI();
        }

        private void OnUserPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Hp")
            {
                UpdateUI();
            }
        }

        private void UpdateUI()
        {
            slider.value = character.Abilities.Hp / character.Abilities.MaxHp;
        }
    }
}