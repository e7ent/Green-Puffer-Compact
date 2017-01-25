using System;
using GreenPuffer.Accounts;
using GreenPuffer.Characters;
using UnityEngine;

namespace GreenPuffer.Misc
{
    class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [SerializeField]
        private Canvas _result;
        [SerializeField]
        private float _scorePerSeconds;

        public ScoreKeeper ScoreKeeper { get; private set; }

        private void Awake()
        {
            Instance = this;
        }


        private void OnKilled(object sender, System.EventArgs e)
        {
            _result.enabled = true;
            if (ScoreKeeper.Current > Users.LocalUser.BestScore)
            {
                Users.LocalUser.BestScore = (int)ScoreKeeper.Current;
            }
        }

        private void Update()
        {
            if (!_result.enabled)
            {
                if (ScoreKeeper != null)
                {
                    ScoreKeeper.Add(Time.deltaTime * _scorePerSeconds);
                }
            }
        }

        public void Setup(PlayerCharacter playerCharacter, ScoreKeeper _scoreKeeper)
        {
            playerCharacter.Killed += OnKilled;
            ScoreKeeper = _scoreKeeper;
        }
    }
}