using GreenPuffer.Accounts;
using GreenPuffer.Quests;
using System.Collections.Generic;
using UnityEngine;

namespace GreenPuffer.UI
{
    class QuestViewer : MonoBehaviour
    {
        [SerializeField]
        private Table table;

        private IEnumerable<Quest> AllQuests
        {
            get
            {
                foreach (var item in User.LocalUser.InProgress)
                    yield return item;
                foreach (var item in User.LocalUser.Complated)
                    yield return item;
            }
        }

        private void OnEnable()
        {
            table.Reload(AllQuests);
        }
    }
}
