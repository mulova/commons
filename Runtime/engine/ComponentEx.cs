//----------------------------------------------
// Common library for Unity3D libraries
// License: The MIT License ( http://opensource.org/licenses/MIT )
// Copyright © 2013- mulova@gmail.com
//----------------------------------------------

using mulova.commons;

namespace UnityEngine.Ex
{
    public static class ComponentEx
	{
        public static readonly ILog log = LogManager.GetLogger(nameof(ComponentEx));
		public static T FindComponent<T>(this Component c) where T:Component
		{
			if (c == null) {
				return null;
			}
			return c.gameObject.FindComponent<T>();
		}

		public static void SetVisible(this Component c, bool active) {
			if (c != null) {
				c.gameObject.SetActive(active);
			} else
            {
                log.Warn("Null");
            }
        }
	}
}


