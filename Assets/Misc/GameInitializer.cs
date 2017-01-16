using GreenPuffer.Characters;
using UnityEngine;

namespace GreenPuffer.Misc
{
    class GameInitializer : MonoBehaviour
    {

        private void Awake()
        {
            var character = Instantiate(PlayerCharacter.Selected, Vector3.zero, Quaternion.identity);
        }
    }
}
