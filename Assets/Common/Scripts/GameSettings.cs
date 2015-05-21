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

	[Header("Character")]
	public GameObject[] character;


	[Header("Common")]
	public GameObject coinPrefab;
	public float maxSize = 3;


	[Header("Effect Prefabs")]
	public GameObject[] explosion;
	public HUDEffect coinHUD;
	public HUDEffect hitHUD;
	public HUDEffect expHUD;
}
