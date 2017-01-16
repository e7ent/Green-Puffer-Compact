using System.Linq;
using UnityEngine;
using GreenPuffer.Characters;

namespace GreenPuffer.UI
{
    class SelectedCharacterViewer : MonoBehaviour
    {
        private void Start()
        {
            var character = Instantiate(PlayerCharacter.Selected);

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