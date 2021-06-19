using System;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace mulova.unicore
{
    public delegate string ConvToString(object o);
    public delegate Object ConvToObject(object o);

    public class ItemDrawer<T> : IItemDrawer<T> where T:class
    {
        public const float LINE_HEIGHT = 16f;
        public ConvToString toStr = o=> o?.ToString();
        public ConvToObject toObj = o=> o as Object;

        public virtual void DrawItemBackground(Rect bound, int index, T obj)
        {
        }

        public virtual bool DrawItem(Rect bound, int index, T obj, out T newObj)
        {
            try
            {
                Object o = toObj(obj);
                newObj = EditorGUI.ObjectField(bound, o, typeof(T), true) as T;
                return obj != newObj;
            } catch (Exception ex)
            {
                newObj = obj;
                if (!(ex is ExitGUIException))
                {
                    throw;
                }
                return false;
            }
        }

        public virtual float GetItemHeight(int index, T obj)
        {
            return LINE_HEIGHT;
        }
    }
}

