using UnityEditor;
using UnityEngine;

namespace mulova.unicore
{
    [CustomPropertyDrawer(typeof(NullableFieldAttribute))]
    public class NullableFieldDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            Color old = GUI.contentColor;
            if (property.objectReferenceValue == null)
            {
                GUI.contentColor = new Color32(211, 211, 211, 255);
            }
            EditorGUI.PropertyField(position, property);
            if (property.objectReferenceValue == null)
            {
                GUI.contentColor = old;
            }
        }
    }
}
