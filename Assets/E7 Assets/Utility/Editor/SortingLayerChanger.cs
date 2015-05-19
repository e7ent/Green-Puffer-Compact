using UnityEngine;
using UnityEditor;
using System.Collections;

public class SortingLayerChanger : EditorWindow
{
	private string layer;

	[MenuItem("Tools/Sorting Layer Changer")]
	public static void ShowWindow()
	{
		EditorWindow.GetWindow<SortingLayerChanger>();
	}

	private void OnGUI()
	{
		layer = EditorGUILayout.TextField("Layer", layer);
		
		if (GUILayout.Button("Change"))
		{
			var objects = Selection.activeGameObject;
			var renderers = objects.GetComponentsInChildren<Renderer>();
			for (int i = 0; i < renderers.Length; i++)
			{
				renderers[i].sortingLayerName = layer;
			}
		}
	}
}
