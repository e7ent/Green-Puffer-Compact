using Astro.Features.Quests;
using GreenPuffer.Quests;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GreenPuffer.Accounts
{
    class QuestManager : IQuestPerformer<Quest, QuestDescriptor>
    {
        public IEnumerable<Quest> InProgress { get { return quests.Where(x => !x.AlreadyProvide); } }
        public IEnumerable<Quest> Complated { get { return quests.Where(x => x.AlreadyProvide); } }

        private List<Quest> quests;

        public QuestManager(User user)
        {
            quests = new List<Quest>();
            var db = Resources.Load<QuestDatabase>("Quests");
            foreach (var questDescriptor in db)
            {
                quests.Add(new Quest(user, questDescriptor));
            }
        }

        public void Perform(Quest quest)
        {
            throw new NotImplementedException();
        }
    }
}
