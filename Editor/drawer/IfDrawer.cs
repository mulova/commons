using UnityEditor;
using UnityEngine;
using UnityEngine.Ex;

namespace mulova.unicore
{
    [CustomPropertyDrawer(typeof(IfAttribute))]
    public class IfDrawer : PropertyDrawer
    {
        private float errorBoxHeight = 30;
        private string erroMsg = "Invalid";

        private IfAction GetAction(SerializedProperty property)
        {
            IfAttribute attr = attribute as IfAttribute;
            var refProp = attr.refPropertyPath != null? property.serializedObject.FindProperty(attr.refPropertyPath): property;
            if (refProp != null)
            {
                var match = object.Equals(refProp.GetValue(), attr.value);
                if (match)
                {
                    return attr.action;
                }
                else
                {
                    switch (attr.action)
                    {
                        case IfAction.Hide:
                            return IfAction.Show;
                        case IfAction.Show:
                            return IfAction.Hide;
                        case IfAction.Disable:
                            return IfAction.Enable;
                        case IfAction.Enable:
                            return IfAction.Disable;
                        case IfAction.Error:
                            return IfAction.None;
                        case IfAction.None:
                            return IfAction.None;
                        default:
                            throw new System.Exception("Unreachable");
                    }

                }
            }
            else
            {
               return IfAction.None;
            }
            //return refProp != null &&  == attr.value;
        }
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            IfAttribute attr = attribute as IfAttribute;
            var action = GetAction(property);

            if (action == IfAction.Error)
            {
                var rect = position.SplitByHeights((int)EditorGUIUtility.singleLineHeight);
                using (new ColorScope(Color.red))
                {
                    EditorGUI.PropertyField(rect[0], property, label);
                }
                rect[1].height -= 15;
                EditorGUI.HelpBox(rect[1], erroMsg, MessageType.Error);
            }
            else
            {
                using (new EditorGUI.DisabledScope(action == IfAction.Disable))
                {
                    if (action != IfAction.Hide)
                    {
                        EditorGUI.PropertyField(position, property, label);
                    }
                }
            }

        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            IfAttribute attr = attribute as IfAttribute;
            var action = GetAction(property);

            if (action == IfAction.Hide)
            {
                return 0;
            } else
            {
                var height = EditorGUIUtility.singleLineHeight;
                if (action == IfAction.Error)
                {
                    height += errorBoxHeight;
                }
                return height;
            }
        }
    }
}
