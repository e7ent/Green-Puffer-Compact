using System.Linq;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;

public class GameSettings : ScriptableObject
{
	private static GameSettings instance;


	public static GameSettings Instance
	{
		get
		{
			if (instance != null)
				return instance;

			instance = Resources.Load<ScriptableObject>("GameSettings") as GameSettings;
			return instance;
		}
	}


#if UNITY_EDITOR
	[MenuItem("Tools/Game Settings")]
	public static void SelectGameSettings()
	{
		Selection.activeObject = Resources.Load<ScriptableObject>("GameSettings") as GameSettings;
	}
#endif


	[Header("Parse")]
	public string appID;
	public string dotnetID;
	public string TestID;


	[Header("Character")]
	public PlayerCharacter[] characters;
	public PlayerCharacter defaultCharacters;


	[Header("Common")]
	public float maxSize = 3;
	public Sprite[] rankSprites;


	[Header("Effect Prefabs")]
	public GameObject[] explosion;
	public HUDEffect coinHUD;
	public HUDEffect hitHUD;
	public HUDEffect expHUD;
	public GameObject createEffect;


	[Header("Prefabs")]
	public GameObject warningPrefab;


	public PlayerCharacter GetPlayerPrefabFromGUID(string id)
	{
		var characters = GameSettings.Instance.characters;
		var prefabs = from character in characters where character.ID == id select character;

		return prefabs.First();
	}
}
