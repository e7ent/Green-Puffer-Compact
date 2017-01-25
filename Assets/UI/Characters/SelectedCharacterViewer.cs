using GreenPuffer.Accounts;
using System.Linq;
using UnityEngine;

namespace GreenPuffer.UI
{
    class SelectedCharacterViewer : MonoBehaviour
    {
        private void Awake()
        {
            Users.LocalUser.PropertyChanged += OnSelectionChanged;
            UpdateViewer();
        }

        private void OnSelectionChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            UpdateViewer();
        }

        private void OnDestroy()
        {
            Users.LocalUser.PropertyChanged -= OnSelectionChanged;
        }

        private void UpdateViewer()
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }

            var character = Instantiate(Users.LocalUser.SelectedCharacter, transform);

            var components = from component in character.GetComponents<Component>()
                             where !(component is Transform)
                             select component;
            foreach (var item in components)
                Destroy(item);

            character.transform.position = Vector3.zero;
            character.transform.localScale = Vector3.one * 2;
            character.transform.rotation = Quaternion.identity;
        }
    }
}