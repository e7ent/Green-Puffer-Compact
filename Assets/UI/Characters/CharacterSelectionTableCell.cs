using System;
using GreenPuffer.Characters;
using UnityEngine;
using UnityEngine.UI;

namespace GreenPuffer.UI
{
#pragma warning disable 0649
    class CharacterSelectionTableCell : TableCell
    {
        [SerializeField]
        private Text nameText;
        [SerializeField]
        private CharacterRankImage rankImage;
        [SerializeField]
        private Image thumbnail;
        [SerializeField]
        private Image selection;

        public PlayerCharacter Character { get; private set; }
        public bool IsSelected { get { return selection.enabled; } }

        public void OnClicked()
        {
            selection.enabled = !selection.enabled;
        }

        public override void Load(object data)
        {
            Character = data as PlayerCharacter;
            nameText.text = Character.NickName;
            rankImage.Rank = Character.Abilities.Rank;
            thumbnail.sprite = Character.Thumbnail;
            selection.enabled = false;
        }
    }
}