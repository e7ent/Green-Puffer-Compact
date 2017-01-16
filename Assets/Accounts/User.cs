using Astro.Features.Effects;
using Astro.Features.Quests;
using GreenPuffer.Quests;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GreenPuffer.Accounts
{
    class User : INotifyPropertyChanged, IAffectable<CoinBankAccount>
    {
        private static User localUser;
        public static User LocalUser
        {
            get
            {
                if (localUser == null)
                {
                    localUser = new User();
                }
                return localUser;
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler<AffectedEventArgs<CoinBankAccount>> Affected;

        public string Id { get { return "LocalUser"; } }
        public Counter Counter { get; private set; }
        public decimal Coin { get { return coinBankAccount.Balance; } }
        public IEnumerable<Quest> InProgress { get { return questCollection; } }
        public IEnumerable<Quest> Complated { get { return questCollection; } }
        private QuestCollection questCollection;
        private CoinBankAccount coinBankAccount;

        public User()
        {
            Counter = new Counter(this);
            questCollection = new QuestCollection(this);
            coinBankAccount = new CoinBankAccount(this);
            coinBankAccount.PropertyChanged += (sender, args) =>
            {
                InvokePropertyChanged("Coin");
            };
        }

        private void InvokePropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public void TakeEffect(IEffector<CoinBankAccount> effector)
        {
            effector.Affect(coinBankAccount);
            if (Affected != null)
            {
                Affected(this, new AffectedEventArgs<CoinBankAccount>(effector));
            }
        }
    }
}
