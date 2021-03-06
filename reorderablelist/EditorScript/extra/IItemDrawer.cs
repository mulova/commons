﻿using UnityEngine;

namespace mulova.unicore
{
    public interface IItemDrawer<T>
    {
        bool DrawItem(Rect rect, int index, T obj, out T changedObj);
        void DrawItemBackground(Rect rect, int index, T obj);
        float GetItemHeight(int index, T obj);
    }
}

