using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomPropertyDrawer (typeof(Health))]
public class HealthDrawer : PropertyDrawer
{
    public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
    {
        // Using BeginProperty / EndProperty on the parent property means that
        // prefab override logic works on the entire property.
        EditorGUI.BeginProperty (position, label, property);
		
        // Draw label
        position = EditorGUI.PrefixLabel (position, GUIUtility.GetControlID (FocusType.Passive), label);
		
        // Don't make child fields be indented
        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;
		
        // Calculate rects
        var fieldLenght = (position.width - 2) / 2;
        var currentRect = new Rect (position.x, position.y, fieldLenght, position.height);
        var maxRect = new Rect (position.x + fieldLenght + 2, position.y, fieldLenght, position.height);
		
        // Draw fields - passs GUIContent.none to each so they are drawn without labels
        EditorGUI.PropertyField (currentRect, property.FindPropertyRelative ("current"), GUIContent.none);
        EditorGUI.PropertyField (maxRect, property.FindPropertyRelative ("max"), GUIContent.none);
		
        // Set indent back to what it was
        EditorGUI.indentLevel = indent;
		
        EditorGUI.EndProperty ();
    }
}