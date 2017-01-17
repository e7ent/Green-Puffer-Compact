using DG.Tweening;
using GreenPuffer.Characters;
using System.ComponentModel;
using UnityEngine;

namespace GreenPuffer.Effectors
{
    class AffectedEffect : MonoBehaviour
    {
        [SerializeField]
        private CharacterBase character;

        private void Awake()
        {
            character.Abilities.PropertyChanged += OnAbilitiesPropertyChanged;
        }

        private void OnAbilitiesPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Exp")
            {
                var t = character.transform;
                t.DOComplete(true);
                transform.DOPunchScale(Vector2.one * 0.2f, 0.4f);
            }
        }
    }
}