using UnityEngine;
using UnityEngine.SceneManagement;

namespace GreenPuffer.Misc
{
    public class UnityMethods : MonoBehaviour
    {
        public void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

        public void ResetPlayerPrefs()
        {
            PlayerPrefs.DeleteAll();
        }
    }
}