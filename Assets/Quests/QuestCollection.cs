using System;
using System.Collections;
using System.Collections.Generic;
using GreenPuffer.Accounts;
using UnityEngine;

namespace GreenPuffer.Quests
{
    class QuestCollection : IEnumerable<Quest>
    {
        private List<Quest> quests;

        public QuestCollection(User user)
        {
            quests = new List<Quest>();
            var db = Resources.Load<QuestDatabase>("Quests");
            foreach (var questDescriptor in db)
            {
                quests.Add(new Quest(user, questDescriptor));
            }
        }

        public void Add(Quest item)
        {
            quests.Add(item);
        }

        public IEnumerator<Quest> GetEnumerator()
        {
            return quests.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return quests.GetEnumerator();
        }
    }
}
