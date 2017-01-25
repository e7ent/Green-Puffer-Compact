using System.Collections;
using UnityEngine;

namespace GreenPuffer.Misc
{
    class Pauser : MonoBehaviour
    {
        private void Awake()
        {
            Time.timeScale = 1;
        }

        private void OnDestroy()
        {
            Time.timeScale = 1;
        }

        public void Pause()
        {
            Time.timeScale = 0;
        }

        public void Resume()
        {
            Time.timeScale = 1;
        }
    }
}