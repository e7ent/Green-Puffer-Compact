using GreenPuffer.Accounts;
using System;
using System.Collections;
using UnityEngine;

namespace GreenPuffer.Misc
{
    class GameInitializer : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] backgrounds;

        // 만약 게임이 개복치 처럼 재시작 뒤에도 데이터가 유지 되려면?
        // private static prevScoreKeeper;
        //private static int PrevScore;
        public static event EventHandler Initialized;
        public static ScoreKeeper _prevScoreKeeper;

        private void Awake()
        {
            foreach (var item in backgrounds)
            {
                item.SetActive(item.name == "Background " +
                    Users.LocalUser.SelectedCharacter.Abilities.Rank.ToString());
            }

            var playerCharacter = Instantiate(Users.LocalUser.SelectedCharacter, Vector3.zero, Quaternion.identity);
            if (Initialized != null)
            {
                Initialized(this, EventArgs.Empty);
            }
            if (_prevScoreKeeper == null)
            {
                _prevScoreKeeper = new ScoreKeeper();
            }
            GameManager.Instance.Setup(playerCharacter, _prevScoreKeeper);
            _prevScoreKeeper = null;
        }

        private IEnumerator Start()
        {
            yield return null;
            Initialize();
        }

        private void Initialize()
        {
        }

        //public static void ReInit();
    }
}
