using GreenPuffer.Accounts;
using UnityEngine;

namespace GreenPuffer.UI
{
    class MyCharactersViewer : MonoBehaviour
    {
        [SerializeField]
        private Table table;

        private void Awake()
        {
            User.LocalUser.CharacterCollection.Unlocked += OnUnlocked;
        }

        private void OnUnlocked(object sender, System.EventArgs e)
        {
            OnEnable();
        }

        private void OnDestroy()
        {
            User.LocalUser.CharacterCollection.Unlocked -= OnUnlocked;
        }

        private void OnEnable()
        {
            table.Reload(User.LocalUser.CharacterCollection.UnlockedCharacters);
        }
    }
}
