using GreenPuffer.Accounts;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GreenPuffer.UI
{
    class MyCharacterList : MonoBehaviour
    {
        [SerializeField]
        private MyCharacterListCell cellTemplate;
        [SerializeField]
        private RectTransform contentPanel;
        [SerializeField]
        private float cellHeight;
        private List<MyCharacterListCell> cells;

        private void Awake()
        {
            User.LocalUser.CharacterCollection.Unlocked += OnUnlocked;
        }

        private void OnUnlocked(object sender, System.EventArgs e)
        {
            OnEnable();
        }

        private void OnDestroy()
        {
            User.LocalUser.CharacterCollection.Unlocked -= OnUnlocked;
        }

        private void OnEnable()
        {
            if (cells != null)
            {
                foreach (var view in cells)
                {
                    Destroy(view.gameObject);
                }
            }

            cells = new List<MyCharacterListCell>();

            int count = 0;
            cellTemplate.gameObject.SetActive(true);
            foreach (var character in User.LocalUser.CharacterCollection.UnlockedCharacters)
            {
                var newCell = Instantiate(cellTemplate);
                newCell.Setup(character);
                newCell.transform.SetParent(cellTemplate.transform.parent, false);
                cells.Add(newCell);
                count++;
            }
            var size = contentPanel.sizeDelta;
            size.y = cellHeight * count;
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