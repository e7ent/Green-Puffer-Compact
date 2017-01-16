using System;

namespace GreenPuffer.Accounts
{
    class CounterEventArgs : EventArgs
    {
        public string Key { get; private set; }
        public int Value { get; private set; }

        public CounterEventArgs(string key, int value)
        {
            this.Key = key;
            this.Value = value;
        }
    }
}
