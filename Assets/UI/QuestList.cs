using GreenPuffer.Accounts;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GreenPuffer.UI
{
    class QuestList : MonoBehaviour
    {
        [SerializeField]
        private QuestListCell cellTemplate;
        [SerializeField]
        private RectTransform contentPanel;
        private List<QuestListCell> cells;

        private void OnEnable()
        {
            if (cells != null)
            {
                foreach (var view in cells)
                {
                    Destroy(view.gameObject);
                }
            }

            cells = new List<QuestListCell>();
            
            int count = 0;
            cellTemplate.gameObject.SetActive(true);
            foreach (var quest in User.LocalUser.InProgress)
            {
                var newCell = Instantiate(cellTemplate);
                newCell.Setup(quest);
                newCell.transform.SetParent(cellTemplate.transform.parent, false);
                cells.Add(newCell);
                count++;
            }
            foreach (var quest in User.LocalUser.Complated)
            {
                var newCell = Instantiate(cellTemplate);
                newCell.Setup(quest);
                newCell.transform.SetParent(cellTemplate.transform.parent, false);
                cells.Add(newCell);
                count++;
            }
            var size = contentPanel.sizeDelta;
            size.y = (cellTemplate.transform as RectTransform).sizeDelta.y * count;
            var layoutGroup = contentPanel.GetComponent<VerticalLayoutGroup>();
            if (layoutGroup != null)
            {
                size.y += layoutGroup.spacing * count;
            }
            contentPanel.sizeDelta = size;
            cellTemplate.gameObject.SetActive(false);
        }
    }
}