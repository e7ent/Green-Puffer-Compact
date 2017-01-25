using System;
using System.ComponentModel;

namespace GreenPuffer.Misc
{
    class ScoreKeeper : INotifyPropertyChanged
    {
        private float _current;
        public float Current { get { return _current; } private set { _current = value; InvokePropertyChanged("Current"); } }

        public event PropertyChangedEventHandler PropertyChanged;

        private void InvokePropertyChanged(string name)
        {
            if (PropertyChanged!=null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public void Add(float value)
        {
            if (value < 0)
            {
                throw new InvalidOperationException();
            }
            Current += value;
        }
    }
}
