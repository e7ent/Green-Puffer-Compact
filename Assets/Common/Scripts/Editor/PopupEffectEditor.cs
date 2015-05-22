using UnityEngine;
using UnityEditor;
using System.Collections;


[CustomEditor(typeof(PopupEffect))]
public class PopupEffectEditor : Editor
{
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();

		if (GUILayout.Button("Show"))
		{
			(target as MonoBehaviour).transform.localScale = Vector3.one;
		}

		if (GUILayout.Button("Hide"))
		{
			(target as MonoBehaviour).transform.localScale = Vector3.zero;
		}
	}
}
