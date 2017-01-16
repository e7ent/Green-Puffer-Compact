using GreenPuffer.Characters;
using UnityEngine;
using UnityEngine.UI;

namespace GreenPuffer.UI
{
    public class CharacterRankImage : Image
    {
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
                sprite = sprites[(int)rank];
            }
        }

        protected override void Awake()
        {
            base.Awake();
            Rank = rank;
        }
    }
}
