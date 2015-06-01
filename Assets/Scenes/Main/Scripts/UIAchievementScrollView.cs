using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using E7Assets;
using E7Assets.Achievement;

public class UIAchievementScrollView : MonoBehaviour
{
	public float cellHeight;
	public GameObject cellTemplate;


	private Dictionary<GameObject, IAchievement> cells = new Dictionary<GameObject, IAchievement>();


	private void Start()
	{
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
		var achievements = AchievementManager.Instance.GetAllAchievement();
		foreach (var achievement in achievements)
		{
			var newCell = Instantiate<GameObject>(cellTemplate);
			var t = newCell.transform;

			t.parent = transform;
			t.localScale = Vector3.one;

			t.Find("Title").GetComponent<Text>().text = achievement.Title;
			t.Find("Icon Frame").Find("Icon").GetComponent<Image>().sprite = achievement.Icon;
			t.Find("Description").GetComponent<Text>().text = achievement.Description;
			var button = t.Find("Button").GetComponent<Button>();
			if (achievement.IsFinish)
			{
				button.interactable = false;
			}
			else if (achievement.IsSatisfy == false)
			{
				button.interactable = false;
			}
			else
			{
				button.interactable = true;
			}


			cells.Add(newCell, achievement);
		}
		var rt = GetComponent<RectTransform>();
		var size = rt.sizeDelta;
		size.y = cellHeight * (Mathf.Ceil(achievements.Count() / 2) + 1);
		rt.sizeDelta = size;

		cellTemplate.SetActive(false);
	}


	public void OnSelectCell(GameObject sender)
	{
		cells[sender].ProvideReward();
	}
}
