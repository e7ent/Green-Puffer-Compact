using GreenPuffer.Characters;
using GreenPuffer.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GreenPuffer.Accounts
{
    class CharacterCollection
    {
        public event EventHandler Unlocked;
        private User user;

        public CharacterCollection(User user)
        {
            this.user = user;
        }

        public IEnumerable<PlayerCharacter> AllCharacters
        {
            get
            {
                return Resources.LoadAll<PlayerCharacter>("PlayerCharacters");
            }
        }

        public IEnumerable<PlayerCharacter> UnlockedCharacters
        {
            get
            {
                string[] ids = Storage.Load<string[]>(user.Id + "UnlockedCharacters");
                if (ids == null || ids.Length <= 0)
                {
                    ids = new string[] { "Baby" };
                }

                foreach (var id in ids)
                {
                    yield return GetPrefabFromId(id);
                }
            }
            private set
            {
                var strs = value.Select(x => x.name).ToArray();
                Storage.Save(user.Id + "UnlockedCharacters", strs);
                if (Unlocked != null)
                {
                    Unlocked(this, EventArgs.Empty);
                }
            }
        }

        public PlayerCharacter GetPrefabFromId(string id)
        {
            return Resources.Load<PlayerCharacter>("PlayerCharacters/" + id);
        }

        public void Unlock(PlayerCharacter character)
        {
            var list = UnlockedCharacters.ToList();
            //if (!list.Contains(character))
            {
                list.Add(character);
            }
            UnlockedCharacters = list;
        }
    }
}
