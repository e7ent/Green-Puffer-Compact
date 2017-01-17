using Astro.Features.Quests;
using System;
using UnityEngine;

namespace GreenPuffer.Quests
{
    [Serializable]
    class QuestDescriptor : IQuestDescriptor
    {

        [SerializeField]
        [Header("Descriptions")]
        private string key = "";
        [SerializeField]
        private Sprite icon = null;
        [SerializeField]
        private string title = "", description = "";
        [SerializeField]
        private int rewardCoin = 0;
        [Header("Values")]
        [SerializeField]
        private string counterKey = "";
        [SerializeField]
        private int goal = 1;

        public string Key { get { return key; } }
        public Sprite Icon { get { return icon; } }
        public string Title { get { return title; } }
        public string Description { get { return description; } }
        public string CounterKey { get { return counterKey; } }
        public int Goal { get { return goal; } }
        public int RewardCoin { get { return rewardCoin; } }
    }
}
