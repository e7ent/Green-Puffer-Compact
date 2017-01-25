using System.Collections.Generic;
using GreenPuffer.Accounts;
using GreenPuffer.Characters;
using System.Linq;
using UnityEngine;

namespace GreenPuffer.Synthesis
{
    class Synthesizer
    {
        public float TotalWeight
        {
            get
            {
                return Characters.Select(x => x.Abilities.Rank.GetWeight()).Sum();
            }
        }
        public CharacterRank ResultRank
        {
            get
            {
                CharacterRank rank = CharacterRank.Unknown;
                float unit = 1.0f / 4;
                if (TotalWeight >= 1)
                {
                    rank = CharacterRank.S;
                }
                else if (TotalWeight >= unit * 3)
                {
                    rank = CharacterRank.A;
                }
                else if (TotalWeight >= unit * 2)
                {
                    rank = CharacterRank.B;
                }
                else if (TotalWeight >= unit * 1)
                {
                    rank = CharacterRank.C;
                }
                return rank;
            }
        }
        public List<PlayerCharacter> Characters { get; private set; }
        private User _user;

        public Synthesizer(User user)
        {
            _user = user;
            Characters = new List<PlayerCharacter>();
        }

        public PlayerCharacter Synthesize()
        {
            if (ResultRank == CharacterRank.Unknown)
                return null;

            var betters = CharacterDatabase.GetAllPlayerCharacters()
                .Where(x => x.Abilities.Rank == ResultRank).ToArray();
            var selected = betters[Random.Range(0, betters.Length)];

            foreach (var character in Characters)
            {
                _user.RemoveCharacter(character);
            }
            _user.CreateCharacter(selected);

            return selected;
        }
    }
}
