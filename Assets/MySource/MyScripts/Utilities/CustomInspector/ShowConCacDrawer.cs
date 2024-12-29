using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(ShowConCacAttribute))]
public class ShowConCacDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Xác minh xem loại trường có phải là Dictionary không
        if (fieldInfo.FieldType != typeof(Dictionary<string, int>))
        {
            EditorGUI.LabelField(position, label.text, "Use [ShowConCac] with Dictionary<string, int>");
            return;
        }

        // Lấy giá trị Dictionary từ đối tượng thực tế
        var dictionary = fieldInfo.GetValue(property.serializedObject.targetObject) as Dictionary<string, int>;
        if (dictionary == null)
        {
            dictionary = new Dictionary<string, int>();
            fieldInfo.SetValue(property.serializedObject.targetObject, dictionary);
        }

        // Vẽ giao diện trong Inspector
        property.isExpanded = EditorGUI.Foldout(new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight), property.isExpanded, label);
        if (!property.isExpanded) return;

        EditorGUI.indentLevel++;

        int index = 0;
        foreach (var kvp in new Dictionary<string, int>(dictionary)) // Tạo bản sao
        {
            float y = position.y + (index + 1) * EditorGUIUtility.singleLineHeight;

            Rect keyRect = new Rect(position.x, y, position.width * 0.4f, EditorGUIUtility.singleLineHeight);
            Rect valueRect = new Rect(position.x + position.width * 0.45f, y, position.width * 0.4f, EditorGUIUtility.singleLineHeight);
            Rect removeButtonRect = new Rect(position.x + position.width * 0.9f, y, position.width * 0.1f, EditorGUIUtility.singleLineHeight);

            // Chỉnh sửa key
            string newKey = EditorGUI.TextField(keyRect, kvp.Key);

            // Chỉnh sửa value
            int newValue = EditorGUI.IntField(valueRect, kvp.Value);

            // Nút xóa
            if (GUI.Button(removeButtonRect, "X"))
            {
                dictionary.Remove(kvp.Key);
            }
            else if (newKey != kvp.Key || newValue != kvp.Value)
            {
                dictionary.Remove(kvp.Key);
                dictionary[newKey] = newValue;
            }

            index++;
        }

        Rect addButtonRect = new Rect(position.x, position.y + (index + 1) * EditorGUIUtility.singleLineHeight, position.width, EditorGUIUtility.singleLineHeight);
        if (GUI.Button(addButtonRect, "Add Entry"))
        {
            dictionary.Add("NewKey", 0);
        }

        EditorGUI.indentLevel--;
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        if (!property.isExpanded) return EditorGUIUtility.singleLineHeight;

        var dictionary = fieldInfo.GetValue(property.serializedObject.targetObject) as Dictionary<string, int>;
        int count = dictionary != null ? dictionary.Count : 0;
        return (count + 2) * EditorGUIUtility.singleLineHeight; // +2 cho nút Add và Header
    }
}
