using UnityEngine;
using System.Collections;

public class UMTest : MonoBehaviour {

	void Start () 
	{
		GameDataManager.Instance.LoginAsync();
	}


	public void OnGUI()
	{
		if (GameDataManager.Instance.User == null)
			return;


	}


}
