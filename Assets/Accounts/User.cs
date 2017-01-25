using GreenPuffer.Characters;
using GreenPuffer.Misc;
using GreenPuffer.Synthesis;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System;

namespace GreenPuffer.Accounts
{
    class User : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string Id { get { return "LocalUser"; } }
        // IAffectable or Transaction을 쓰지 않는 이유
        // 1. setter는 Server측에서만 제공하면 된다.
        // 2. 오프라인게임에서도 복잡도만 증가시킨다.
        public int Coin { get { return GetProperty<int>("Coin"); } set { SetProperty("Coin", value); } }
        public int BestScore { get { return GetProperty<int>("BestScore"); } set { SetProperty("BestScore", value); } }
        public Counter Counter { get; private set; }
        public QuestManager QuestManager { get; private set; }
        public IEnumerable<PlayerCharacter> Characters { get { return _characters; } }
        public PlayerCharacter SelectedCharacter
        {
            get
            {
                var id = GetProperty("SelectedCharacter", "Baby");
                return CharacterDatabase.GetPlayerCharactersById(id);
            }
            set { SetProperty("SelectedCharacter", value.name); }
        }

        private List<PlayerCharacter> _characters;

        public User()
        {
            Counter = new Counter(this);
            QuestManager = new QuestManager(this);

            var ids = GetProperty("Characters", new string[] { "Baby" });
            _characters = new List<PlayerCharacter>(ids.Select(x => CharacterDatabase.GetPlayerCharactersById(x)));
        }

        private void InvokePropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public void CreateCharacter(PlayerCharacter prefab)
        {
            _characters.Add(prefab);
            SetProperty("Characters", _characters.Select(x => x.name).ToArray());
            InvokePropertyChanged("Characters");
        }

        public void RemoveCharacter(PlayerCharacter prefab)
        {
            _characters.Remove(prefab);
            SetProperty("Characters", _characters.Select(x => x.name).ToArray());
            InvokePropertyChanged("Characters");
        }

        public Synthesizer CreateSynthesizer()
        {
            return new Synthesizer(this);
        }

        private T GetProperty<T>(string name, T defaultValue = default(T))
        {
            return Storage.Load(Id + name, defaultValue);
        }

        private void SetProperty(string name, object value)
        {
            Storage.Save(Id + name, value);
            InvokePropertyChanged(name);
        }
    }
}
