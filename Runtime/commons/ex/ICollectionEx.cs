//----------------------------------------------
// Unity3D common libraries and editor tools
// License: The MIT License ( http://opensource.org/licenses/MIT )
// Copyright © 2013- mulova@gmail.com
//----------------------------------------------

namespace System.Collections.Generic.Ex
{
	public static class ICollectionEx {
		
		public static bool IsEmpty<T>(this ICollection<T> col) {
			if (col == null) {
				return true;
			}
			return col.Count <= 0;
		}
		
		public static bool Empty(this ICollection col) {
			if (col == null) {
				return true;
			}
			return col.Count <= 0;
		}

        public static Dictionary<K, V> ToDictionary<K, V>(this ICollection<V> src, Converter<V, K> converter)
        {
            if (src == null)
            {
                return null;
            }
            Dictionary<K, V> dst = new Dictionary<K, V>();
            foreach (V v in src)
            {
                K k = converter(v);
                if (k != null)
                {
                    dst[k] = v;
                }
            }
            return dst;
        }
    }
}
