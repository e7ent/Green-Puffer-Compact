using Astro.Features.Quests;
using System;
using Astro.Features.Effects;
using GreenPuffer.Accounts;
using UnityEngine;

namespace GreenPuffer.Quests
{
    [Serializable]
    class Quest : IQuest<QuestDescriptor>, IEffector<CoinBankAccount>
    {
        public QuestDescriptor Descriptor { get { return descriptor; } }
        public bool CanProvide
        {
            get
            {
                return owner.Counter[descriptor.CounterKey] >= descriptor.Goal && !alreadyProvide;
            }
        }

        private bool alreadyProvide;
        private User owner;
        private QuestDescriptor descriptor;

        public Quest(User owner, QuestDescriptor descriptor)
        {
            this.owner = owner;
            alreadyProvide = PlayerPrefs.GetInt(this.owner.Id + descriptor.Key, 0) == 1;
            this.descriptor = descriptor;
        }

        public bool TryProvideReward()
        {
            if (!CanProvide)
            {
                return false;
            }
            owner.TakeEffect(this);
            PlayerPrefs.SetInt(owner.Id + descriptor.Key, 1);
            return true;
        }

        public void Affect(CoinBankAccount modifier)
        {
            modifier.Deposit(descriptor.RewardCoin);
        }
    }
}
