using UnityEngine;
using UnityEditor;


[CustomPropertyDrawer(typeof(GUIDAttribute))]
public class GUIDPropertyDrawer : PropertyDrawer
{
	public override void OnGUI(UnityEngine.Rect position, SerializedProperty property, UnityEngine.GUIContent label)
	{
		EditorGUI.BeginProperty(position, label, property);
		position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
		int indent = EditorGUI.indentLevel;
		EditorGUI.indentLevel = 0;
		// 그냥 커스텀 클래스로 만들어야함
		//EditorGUI.TextField(position, property.FindPropertyRelative)
		GUI.Button(position, "GUID");

		EditorGUI.indentLevel = indent;
		EditorGUI.EndProperty();
	}
}