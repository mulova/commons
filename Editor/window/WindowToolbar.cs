using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System;

namespace mulova.unicore
{
    public class WindowToolbar
    {
        class Button
        {
            public GUIContent content;
            public int width;
            public Func<bool> isEnabled;
            public Func<Color> getContentColor;
            public Func<Color> getBgColor;
            public Action callback;

            internal Button(GUIContent content, Action callback)
            {
                this.content = content;
                this.callback = callback;
            }
        }

        public class MenuButton
        {
            public GUIContent content;
            public GenericMenu.MenuFunction callback;
            public Func<bool> getSelected;
        }

        public int height = 20;
        private List<Button> buttons = new List<Button>();

        public void AddButton(GUIContent content, Action callback, int width = 0, Func<bool> isEnabled = null, Func<Color> getContentColor = null, Func<Color> getBgColor = null)
        {
            var btn = new Button(content, callback);
            btn.width = width;
            btn.isEnabled = isEnabled;
            btn.getContentColor = getContentColor;
            btn.getBgColor = getBgColor;
            buttons.Add(btn);
        }

        public void AddMenuButton(params MenuButton[] buttons)
        {
            AddMenuButton(new GUIContent(EditorBuiltInIcon.Setting, "Menu"), 30, buttons);
        }
        public void AddMenuButton(GUIContent content, int width, params MenuButton[] buttons)
        {
            AddButton(content, ShowMenu, width);

            void ShowMenu()
            {
                GenericMenu menu = new GenericMenu();
                foreach (var b in buttons)
                {
                    menu.AddItem(b.content, b.getSelected?.Invoke() ?? false, b.callback);
                }
                menu.ShowAsContext();
            }
        }

        public void Draw()
        {
            GUILayout.BeginHorizontal(EditorStyles.toolbar);
            foreach (var b in buttons)
            {
                var color = GUI.contentColor;
                if (b.getContentColor != null)
                {
                    GUI.contentColor = b.getContentColor();
                }
                var bgColor = GUI.backgroundColor;
                if (b.getBgColor != null)
                {
                    GUI.backgroundColor = b.getBgColor();
                }
                GUI.enabled = b.isEnabled == null || b.isEnabled();
                var clicked = b.width > 0 ?
                    GUILayout.Button(b.content, EditorStyles.toolbarButton, GUILayout.Height(height), GUILayout.Width(b.width))
                    : GUILayout.Button(b.content, EditorStyles.toolbarButton, GUILayout.Height(height));
                if (clicked)
                {
                    b.callback();
                }
                GUI.contentColor = color;
                GUI.backgroundColor = bgColor;
            }
            GUILayout.EndHorizontal();
            GUI.enabled = true;
        }
    }
}

