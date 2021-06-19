using System.Ex;
using UnityEditor;
using UnityEngine;

namespace mulova.unicore
{
    [CustomPropertyDrawer(typeof(AssignRequiredAttribute))]
    public class AssignRequiredDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType == SerializedPropertyType.ObjectReference)
            {
                if (property.objectReferenceValue == null)
                {
                    var f = property.serializedObject.targetObject.GetFieldInfo(property.propertyPath);
                    var type = f.FieldType;
                    //var type = Type.GetType(property.propertyPath);
                    var target = property.serializedObject.targetObject as Component;
                    property.objectReferenceValue = target.GetComponent(type);
                    property.serializedObject.ApplyModifiedProperties();
                }
            }
            bool valid = property.objectReferenceValue;
            using (new ColorScope(Color.red, !valid))
            {
                EditorGUI.PropertyField(position, property, label);
            }
        }
    }
}
