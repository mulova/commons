﻿//----------------------------------------------
// Common library for Unity3D libraries
// License: The MIT License ( http://opensource.org/licenses/MIT )
// Copyright © 2013- mulova@gmail.com
//----------------------------------------------

#if PHYSICS_2D
using System.Collections.Generic;
using System.Collections.Generic.Ex;

namespace UnityEngine.Ex
{
    public static class Physics2DEx
	{
		public static T Raycast<T>(Vector2 screenPos, int layer) where T: Component {
			return Raycasts<T>(screenPos, layer).GetFirst();
		}
		
		public static List<T> Raycasts<T>(Vector2 screenPos, int layer) where T: Component {
			Camera cam = CameraEx.GetCamera(layer);
			Ray ray = cam.ScreenPointToRay(screenPos);
			RaycastHit2D[] hits = Physics2D.RaycastAll(ray.origin, ray.direction, float.PositiveInfinity, cam.cullingMask);
			List<T> list = new List<T>();
			foreach (RaycastHit2D r in hits) {
				if (r.collider != null) {
					T t = r.collider.GetComponent<T>();
					if (t != null) {
						list.Add(t);
					}
				}
			}
			return list;
		}
	}
}

#endif