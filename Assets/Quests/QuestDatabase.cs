using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GreenPuffer.Quests
{
    class QuestDatabase : ScriptableObject, IEnumerable<QuestDescriptor>
    {
        [SerializeField]
        private List<QuestDescriptor> quests = null;

        public IEnumerator<QuestDescriptor> GetEnumerator()
        {
            return quests.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return quests.GetEnumerator();
        }
    }
}