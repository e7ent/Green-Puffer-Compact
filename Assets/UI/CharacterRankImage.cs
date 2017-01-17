using GreenPuffer.Characters;
using UnityEngine;
using UnityEngine.UI;

namespace GreenPuffer.UI
{
    public class CharacterRankImage : MonoBehaviour
    {
        [SerializeField]
        private Image image;
        [SerializeField]
        private Sprite[] sprites;
        private CharacterRank rank;

        public CharacterRank Rank
        {
            get
            {
                return rank;
            }
            set
            {
                rank = value;
                image.sprite = sprites[(int)rank];
            }
        }

        protected void Awake()
        {
            Rank = rank;
        }
    }
}
