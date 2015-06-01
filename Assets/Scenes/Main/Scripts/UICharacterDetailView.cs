using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UICharacterDetailView : MonoBehaviour
{
	public Image rank;
	public Text name;
	public Image image;
	public Text attack, defense, hp, force, luck, special;
	public Text description;
	public Button select;


	private PlayerCharacter character;


	public void Apply(PlayerCharacter pc)
	{
		character = pc;
		rank.sprite = GameSettings.Instance.rankSprites[pc.Rank];
		name.text = pc.Name;
		image.sprite = pc.Thumbnail;
		attack.text = pc.Damage.ToString();
		defense.text = pc.Defense.ToString();
		hp.text = pc.MaxHP.ToString();
		force.text = pc.Force.ToString();
		luck.text = pc.Luck.ToString();

		description.text = pc.Description;

		select.interactable = DataManager.Instance.SelectedCharacter.ID.guid != pc.ID.guid;

		SendMessage("Show");
	}


	public void Select()
	{
		DataManager.Instance.SelectedCharacter = character;
		select.interactable = DataManager.Instance.SelectedCharacter.ID.guid != character.ID.guid;
	}
}
