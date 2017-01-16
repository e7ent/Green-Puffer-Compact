using UnityEngine;
using UnityEngine.UI;
using GreenPuffer.Characters;

#pragma warning disable 0649
namespace GreenPuffer.UI
{
    class CharacterInfomationViewer : MonoBehaviour
    {
        [SerializeField] private CharacterRankImage rankImage;
        [SerializeField] private Text nameText;
        [SerializeField] private Image thumbnailImage;
        [SerializeField] private Text strengthText, armorText, maxHpText, forceText, luckText, specialText;
        [SerializeField] private Text descriptionText;
        [SerializeField] private Button selectButton;

        private PlayerCharacter current;

        public void Apply(PlayerCharacter playerCharacter)
        {
            current = playerCharacter;

            var ability = current.Abilities;

            nameText.text = current.NickName;
            thumbnailImage.sprite = current.Thumbnail;

            rankImage.Rank = ability.Rank;
            strengthText.text = ability.Strength.ToString();
            armorText.text = ability.Armor.ToString();
            maxHpText.text = ability.MaxHp.ToString();
            luckText.text = ability.Luck.ToString();
            //force.text = pc.Force.ToString();

            descriptionText.text = current.Description;

            selectButton.interactable = PlayerCharacter.Selected != current;
        }

        public void Select()
        {
            PlayerCharacter.Selected = current;
            selectButton.interactable = PlayerCharacter.Selected != current;
        }
    }
}