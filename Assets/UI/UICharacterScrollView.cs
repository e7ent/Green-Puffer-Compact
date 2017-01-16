using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using GreenPuffer.Characters;

#pragma warning disable 0649
namespace GreenPuffer.UI
{
    class UICharacterScrollView : MonoBehaviour
    {
        [SerializeField] private float cellWidth;
        [SerializeField] private float cellHeight;
        [SerializeField] private GridLayoutGroup.Axis axis;
        [SerializeField] private GameObject cellTemplate;

        private Dictionary<GameObject, PlayerCharacter> cells = new Dictionary<GameObject, PlayerCharacter>();

        private void Start()
        {
            Refresh();
        }


        public void Refresh()
        {
            //cellTemplate.SetActive(true);

            //// clear old datas
            //foreach (var cellPair in cells)
            //	Destroy(cellPair.Key);
            //cells.Clear();

            //// create new
            //var characters = DataManager.Instance.OwnedCharacters;
            //foreach (var characterPrefab in characters)
            //{
            //	var newCell = Instantiate<GameObject>(cellTemplate);
            //	var t = newCell.transform;

            //	t.parent = transform;
            //	t.localScale = Vector3.one;

            //	t.Find("Title").GetComponent<Text>().text = characterPrefab.Name;
            //	t.Find("Rank").GetComponent<Image>().sprite = GameSettings.Instance.rankSprites[characterPrefab.Rank];
            //	t.Find("Character Image").GetComponent<Image>().sprite = characterPrefab.Thumbnail;
            //	cells.Add(newCell, characterPrefab);
            //}
            //var rt = GetComponent<RectTransform>();
            //var size = rt.sizeDelta;
            //if (axis == GridLayoutGroup.Axis.Horizontal)
            //	size.y = cellHeight * (Mathf.Ceil(characters.Count() / 2) + 1);
            //if (axis == GridLayoutGroup.Axis.Vertical)
            //	size.x = cellWidth * (Mathf.Ceil(characters.Count()) + 2);
            //rt.sizeDelta = size;

            //cellTemplate.SetActive(false);
        }


        public void OnSelectCell(GameObject sender)
        {
            FindObjectOfType<CharacterInfomationViewer>().Apply(cells[sender]);
        }
    }
}