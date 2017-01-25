using GreenPuffer.Accounts;
using System.ComponentModel;
using UnityEngine;

namespace GreenPuffer.UI
{
    class MyCharactersViewer : MonoBehaviour
    {
        [SerializeField]
        private Table table;

        private void Awake()
        {
            Users.LocalUser.PropertyChanged += OnPropertyChanged;
        }

        private void OnEnable()
        {
            table.Reload(Users.LocalUser.Characters);
        }

        private void OnDestroy()
        {
            Users.LocalUser.PropertyChanged -= OnPropertyChanged;
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Characters")
            {
                OnEnable();
            }
        }
    }
}
