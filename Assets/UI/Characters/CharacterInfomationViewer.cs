using UnityEngine;
using UnityEngine.UI;
using GreenPuffer.Characters;
using GreenPuffer.Accounts;

#pragma warning disable 0649
namespace GreenPuffer.UI
{
    class CharacterInfomationViewer : MonoBehaviour
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private CharacterRankImage rankImage;
        [SerializeField] private Text nameText;
        [SerializeField] private Image thumbnailImage;
        [SerializeField] private Text strengthText, armorText, maxHpText, forceText, luckText, specialText;
        [SerializeField] private Text descriptionText;
        [SerializeField] private Button selectButton;

        private PlayerCharacter current;

        public void Apply(PlayerCharacter playerCharacter)
        {
            canvas.enabled = true;
            current = playerCharacter;

            var ability = current.Abilities;

            nameText.text = current.NickName;
            thumbnailImage.sprite = current.Thumbnail;

            rankImage.Rank = ability.Rank;
            strengthText.text = ability.Strength.ToString();
            armorText.text = ability.Armor.ToString();
            maxHpText.text = ability.MaxHp.ToString();
            luckText.text = ability.Luck.ToString();
            forceText.text = ability.Speed.ToString();

            descriptionText.text = current.Description;

            selectButton.interactable = Users.LocalUser.SelectedCharacter != current;
        }

        public void Select()
        {
            Users.LocalUser.SelectedCharacter = current;
            selectButton.interactable = false;
        }
    }
}