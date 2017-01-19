using System.Linq;
using UnityEngine;
using GreenPuffer.Characters;

namespace GreenPuffer.UI
{
    class SelectedCharacterViewer : MonoBehaviour
    {
        private void Awake()
        {
            PlayerCharacter.SelectionChanged += OnSelectionChanged;
            UpdateViewer();
        }

        private void OnDestroy()
        {
            PlayerCharacter.SelectionChanged -= OnSelectionChanged;
        }

        private void OnSelectionChanged(object sender, System.EventArgs e)
        {
            UpdateViewer();
        }

        private void UpdateViewer()
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }

            var character = Instantiate(PlayerCharacter.Selected, transform);

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