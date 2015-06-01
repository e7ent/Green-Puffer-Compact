using UnityEngine;
using System.Collections;

public class UMTest : MonoBehaviour {

	void Start () 
	{
		DataManager.Instance.Init();
	}


	public void OnGUI()
	{
		if (DataManager.Instance.User == null)
			return;


	}


}
