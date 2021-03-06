﻿using UnityEngine;
using UnityEditor;

namespace mulova.unicore
{
	public class ObjRefDrawer : ItemDrawer<ObjRef>
	{
		public bool allowSceneObject = true;

		public override bool DrawItem(Rect rect, int index, ObjRef item, out ObjRef newItem)
		{
			Rect[] area = EditorGUILayoutEx.SplitRectHorizontally(rect, (int)rect.width-15);
			Object obj = item?.reference;
			item.reference = EditorGUI.ObjectField(area[0], obj, typeof(Object), allowSceneObject);
			newItem = item;
			return obj != item?.reference;
		}
	}
	
}


