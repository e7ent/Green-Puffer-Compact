using Astro.Features.Economic.Banking;
using System.ComponentModel;
using UnityEngine;

namespace GreenPuffer.Accounts
{
    class CoinBankAccount : IBankAccount
    {
        private User user;

        public decimal Balance { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public CoinBankAccount(User user)
        {
            this.user = user;
            Balance = PlayerPrefs.GetInt(user.Id + "Coin", 100);
        }

        public bool Deposit(decimal amount)
        {
            Balance += amount;
            InvokePropertyChanged();
            return true;
        }

        public bool Withdraw(decimal amount)
        {
            if (Balance < amount) return false;
            Balance -= amount;
            InvokePropertyChanged();
            return true;
        }

        private void InvokePropertyChanged()
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("Balance"));
            }
            PlayerPrefs.SetInt(user.Id + "Coin", (int)Balance);
            PlayerPrefs.Save();
        }
    }
}
