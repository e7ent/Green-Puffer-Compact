using System;
using GreenPuffer.Characters;
using UnityEngine;
using UnityEngine.UI;

namespace GreenPuffer.UI
{

    class MyCharactersTableCell : TableCell
    {
        [SerializeField]
        private Text nameText;
        [SerializeField]
        private CharacterRankImage rankImage;
        [SerializeField]
        private Image thumbnail;
        [SerializeField]
        private CharacterInfomationViewer viewer;

        private PlayerCharacter character;

        public void Setup(PlayerCharacter character)
        {
            this.character = character;
            nameText.text = character.NickName;
            rankImage.Rank = character.Abilities.Rank;
            thumbnail.sprite = character.Thumbnail;
        }

        public void OnClicked()
        {
            viewer.Apply(character);
        }

        public override void Load(object data)
        {
            character = data as PlayerCharacter;
            nameText.text = character.NickName;
            rankImage.Rank = character.Abilities.Rank;
            thumbnail.sprite = character.Thumbnail;
        }
    }
}