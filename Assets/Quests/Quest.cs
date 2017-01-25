using Astro.Features.Quests;
using System;
using GreenPuffer.Accounts;
using UnityEngine;

namespace GreenPuffer.Quests
{
    [Serializable]
    class Quest : IQuest<QuestDescriptor>
    {
        public QuestDescriptor Descriptor { get { return descriptor; } }
        public bool CanProvide
        {
            get
            {
                return Complate && !AlreadyProvide;
            }
        }
        public bool Complate
        {
            get
            {
                return owner.Counter[descriptor.CounterKey] >= descriptor.Goal;
            }
        }
        public int CurrentValue
        {
            get
            {
                return owner.Counter[descriptor.CounterKey];
            }
        }

        public int GoalValue
        {
            get
            {
                return descriptor.Goal;
            }
        }

        public bool AlreadyProvide
        {
            get
            {
                return PlayerPrefs.GetInt(this.owner.Id + descriptor.Key, 0) == 1; ;
            }
        }

        private User owner;
        private QuestDescriptor descriptor;

        public Quest(User owner, QuestDescriptor descriptor)
        {
            this.owner = owner;
            this.descriptor = descriptor;
        }

        public bool TryProvideReward()
        {
            if (!CanProvide)
            {
                return false;
            }
            owner.Coin += descriptor.RewardCoin;
            PlayerPrefs.SetInt(owner.Id + descriptor.Key, 1);
            PlayerPrefs.Save();
            return true;
        }
    }
}
