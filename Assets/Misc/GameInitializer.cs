using GreenPuffer.Characters;
using System;
using System.Collections;
using UnityEngine;

namespace GreenPuffer.Misc
{
    class GameInitializer : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] backgrounds;

        // private static prevScoreKeeper;
        //private static int PrevScore;
        public static event EventHandler Initialized;

        private void Awake()
        {
            foreach (var item in backgrounds)
            {
                item.SetActive(item.name == "Background " + PlayerCharacter.Selected.Abilities.Rank.ToString());
            }
        }

        private IEnumerator Start()
        {
            yield return null;
            Initialize();
        }

        private void Initialize()
        {
            Instantiate(PlayerCharacter.Selected, Vector3.zero, Quaternion.identity);
            if (Initialized != null)
            {
                Initialized(this, EventArgs.Empty);
            }
        }

        //public static void ReInit();
    }
}
