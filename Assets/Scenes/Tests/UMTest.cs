using UnityEngine;
using System.Collections;

public class UMTest : MonoBehaviour {

	void Start () 
	{
		DataManager.Instance.LoginAsync();
	}


	public void OnGUI()
	{
		if (DataManager.Instance.User == null)
			return;


	}


}
