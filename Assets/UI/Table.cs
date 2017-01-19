using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GreenPuffer.UI
{
    class Table : MonoBehaviour
    {
        [SerializeField]
        private ScrollRect scrollRect;
        [SerializeField]
        private TableCell cellTemplate;

        private IEnumerable<Transform> Children
        {
            get
            {
                var content = scrollRect.content;
                int count = content.childCount;
                for (int i = 0; i < count; i++)
                {
                    yield return content.GetChild(i);
                }
            }
        }

        public void Reload(IEnumerable dataSource)
        {
            if (cellTemplate == null)
            {
                throw new ArgumentNullException("cellTemplate");
            }
            var content = scrollRect.content;
            if (content.GetComponent<LayoutGroup>() == null)
            {
                throw new InvalidOperationException("Cannot found LayoutGroup.");
            }
            if (content.GetComponent<ContentSizeFitter>() == null)
            {
                throw new InvalidOperationException("Cannot found ContentSizeFitter.");
            }

            Clear();

            cellTemplate.gameObject.SetActive(true);
            foreach (var item in dataSource)
            {
                var newCell = Instantiate(cellTemplate, content, false);
                newCell.Load(item);
            }
            cellTemplate.gameObject.SetActive(false);
        }

        public void Clear()
        {
            foreach (var child in Children)
            {
                if (child.gameObject == cellTemplate.gameObject)
                    continue;
                Destroy(child.gameObject);
            }
        }
    }
}
