using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;

namespace mulova.unicore
{
    public class SceneViewContextMenu : IDisposable
    {
        private static List<SceneViewContextMenu> instances = new List<SceneViewContextMenu>();

        private static bool drag;
        public delegate void MenuFunc(GenericMenu m);
        public static Func<bool> isAvailable;
        private static readonly List<MenuItem> contextMenuCallback = new List<MenuItem>();
        public delegate bool IsTriggered(Event e);
        public IsTriggered isTriggered = e=> e.shift;

        static SceneViewContextMenu()
        {
#if UNITY_2019_1_OR_NEWER
            SceneView.duringSceneGui += OnSceneGUI;
#else
			SceneView.onSceneGUIDelegate += OnSceneGUI;
#endif
        }

        private class MenuItem : IComparable<MenuItem>
        {
            public readonly MenuFunc func;
            public readonly int order;

            public MenuItem(MenuFunc func, int order)
            {
                this.func = func;
                this.order = order;
            }

            public int CompareTo(MenuItem that)
            {
                return this.order-that.order;
            }
        }

        public SceneViewContextMenu()
        {
            instances.Add(this);
        }

        public void AddContextMenu(MenuFunc func, int order)
        {
            contextMenuCallback.Add(new MenuItem(func, order));
            contextMenuCallback.Sort();
        }

        private static void OnSceneGUI(SceneView sceneview)
        {
            var evt = Event.current;
            if (evt.type == EventType.MouseDown)
            {
                drag = false;
            }
            else if (evt.type == EventType.MouseDrag)
            {
                drag = true;
            }
            else if (evt.type == EventType.MouseUp && !drag)
            {
                if (isAvailable == null || isAvailable())
                {
                    var menu = new GenericMenu();
                    foreach (var s in instances)
                    {
                        if (s.isTriggered(evt))
                        {
                            contextMenuCallback.ForEach(f => f.func(menu));
                        }
                    }
                    Event.current.Use();
                    menu.ShowAsContext();
                }
            }
        }

        public void Dispose()
        {
            instances.Remove(this);
        }
    }
}

