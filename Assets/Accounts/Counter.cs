using System;
using UnityEngine;

namespace GreenPuffer.Accounts
{
    class Counter
    {
        private User user;

        public event EventHandler<CounterEventArgs> Counted;

        public int this[string index]
        {
            get { return PlayerPrefs.GetInt(user.Id + index, 0); }
            set { PlayerPrefs.SetInt(user.Id + index, value); InvokeCounted(index, value); }
        }

        public Counter(User user)
        {
            this.user = user;
        }

        private void InvokeCounted(string key, int value)
        {
            if (Counted != null)
            {
                Counted(this, new CounterEventArgs(key: key, value: value));
            }
        }
    }
}
