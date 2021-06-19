//----------------------------------------------
// Common library for Unity3D libraries
// License: The MIT License ( http://opensource.org/licenses/MIT )
// Copyright © 2013- mulova@gmail.com
//----------------------------------------------

namespace UnityEngine.Ex
{
    public static class ObjectEx {
        
        public static GameObject GetGameObject(this Object o)
        {
            if (o is GameObject)
            {
                return o as GameObject;
            }
            else if (o is Component)
            {
                return ((Component)o).gameObject;
            }
            else
            {
                return null;
            }
        }

        public static void DestroyEx(this Object o) {
            if (o == null) {
                return;
            }
            if (o is GameObject) {
                (o as GameObject).SetActive(false);
            }
            if (Application.isPlaying) {
                Object.Destroy(o);
            } else {
                Object.DestroyImmediate(o);
            }
        }
    }
}

