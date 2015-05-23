using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class RandomLotteryEgg : MonoBehaviour, IPointerUpHandler
{
	public void OnPointerUp(PointerEventData eventData)
	{
		SendMessage("Buy");
	}

	public IEnumerator Buy()
	{
		var list = GameSettings.Instance.characters;
		var newCharacter = list[Random.Range(0, list.Length)];
		float fadeDuration = 0.3f;


		FadeManager.Instance.FadeTo(Color.white, fadeDuration);
		yield return new WaitForSeconds(fadeDuration);
		FadeManager.Instance.FadeTo(Color.black, fadeDuration);
		yield return new WaitForSeconds(fadeDuration);
		FadeManager.Instance.FadeTo(Color.white, fadeDuration);
		yield return new WaitForSeconds(fadeDuration);
		FadeManager.FadeIn();
		yield return new WaitForSeconds(1);
		GameDataManager.Instance.AddCharacter(newCharacter);
		FindObjectOfType<UICharacterDetailView>().Apply(newCharacter);
	}
}
