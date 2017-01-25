using System.Collections.Generic;
using UnityEngine;

namespace GreenPuffer.Characters
{
    static class CharacterDatabase
    {
        public static IEnumerable<PlayerCharacter> GetAllPlayerCharacters()
        {
            return Resources.LoadAll<PlayerCharacter>("PlayerCharacters");
        }

        public static PlayerCharacter GetPlayerCharactersById(string id)
        {
            return Resources.Load<PlayerCharacter>("PlayerCharacters/" + id);
        }
    }
}
