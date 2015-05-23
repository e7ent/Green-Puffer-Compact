using System;
using UnityEngine;
using UnityEditor;


[CustomPropertyDrawer(typeof(GUID))]
public class GUIDPropertyDrawer : PropertyDrawer
{
	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		EditorGUI.BeginProperty(position, label, property);
		var guidProperry = property.FindPropertyRelative("guid");
		
		position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
		
		EditorGUI.PropertyField(
		   new Rect(position.x, position.y, position.width - 50, position.height),
		   guidProperry, GUIContent.none);

		if (GUI.Button(new Rect(position.x + position.width - 46, position.y, 46, position.height), "GUID"))
		{
			guidProperry.stringValue = Guid.NewGuid().ToString();
		}

		EditorGUI.EndProperty();
	}
}