using GreenPuffer.UI;
using UnityEngine;

namespace GreenPuffer.Misc
{
    class AndroidQuit : MonoBehaviour
    {
        [SerializeField]
        private Alert alert;

        private void Update()
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                if (Input.GetKey(KeyCode.Escape))
                {
                    alert.Show("종료하시겠습니까?", () => Application.Quit(), () => { });
                }
            }
        }
    }

}