using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UICharacterScrollView : MonoBehaviour
{
	public GameObject cellTemplate;


	private Dictionary<GameObject, PlayerCharacter> cells = new Dictionary<GameObject, PlayerCharacter>();


	IEnumerator Start()
	{
		var task = GameDataManager.Instance.LoginAsync();
		while (!task.IsCompleted) yield return null;

		Refresh();
	}


	public void Refresh()
	{
		cellTemplate.SetActive(true);

		// clear old datas
		foreach (var cellPair in cells)
			Destroy(cellPair.Key);
		cells.Clear();

		// create new
		var characters = GameDataManager.Instance.OwnedCharacters;
		foreach (var characterPrefab in characters)
		{
			var newCell = Instantiate<GameObject>(cellTemplate);
			var t = newCell.transform;

			t.parent = transform;
			t.localScale = Vector3.one;

			t.Find("Title").GetComponent<Text>().text = characterPrefab.Name;
			t.Find("Rank").GetComponent<Image>().sprite = GameSettings.Instance.rankSprites[characterPrefab.Rank];
			t.Find("Character Image").GetComponent<Image>().sprite = characterPrefab.Thumbnail;
			cells.Add(newCell, characterPrefab);
		}
		cellTemplate.SetActive(false);
	}


	public void OnSelectCell(GameObject sender)
	{
		print(cells[sender].ID.guid);
	}
}
