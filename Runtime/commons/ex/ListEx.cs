//----------------------------------------------
// Unity3D common libraries and editor tools
// License: The MIT License ( http://opensource.org/licenses/MIT )
// Copyright © 2013- mulova@gmail.com
//----------------------------------------------

using System.Diagnostics;

namespace System.Collections.Generic.Ex
{
	public static class ListEx
	{
		public static void Swap<T>(this IList<T> list, int i, int j)
		{
			T temp = list[i];
			list[i] = list[j];
			list[j] = temp;
		}

        public static void Move<T>(this IList<T> list, int i, int inserted)
        {
            T temp = list[i];
            list.RemoveAt(i);
            list.Insert(inserted, temp);
        }

        public static int GetCount<T>(this IList<T> list)
		{
			if (list == null)
			{
				return 0;
			}
			return list.Count;
		}
		
		public static T Get<T>(this IList<T> list, int i)
		{
			if (list == null||i >= list.Count)
			{
				return default(T);
			}
			return list[i];
		}
		
		public static T GetLast<T>(this IList<T> list)
		{
			if (list == null||list.Count == 0)
			{
				return default(T);
			}
			return list[list.Count-1];
		}
		
		public static T GetFirst<T>(this IList<T> list)
		{
			if (list == null||list.Count == 0)
			{
				return default(T);
			}
			return list[0];
		}
		
		public static T GetMax<T>(this IList<T> list, Func<T, float> eval)
		{
			float score = float.MinValue;
			T found = default(T);
			foreach (T t in list)
			{
				float newScore = eval(t);
				if (newScore > score)
				{
					score = newScore;
					found = t;   
				}
			}
			return found;
		}
		
		public static T GetMin<T>(this IList<T> list, Func<T, float> eval)
		{
			float score = float.MaxValue;
			T found = default(T);
			foreach (T t in list)
			{
				float newScore = eval(t);
				if (newScore < score)
				{
					score = newScore;
					found = t;   
				}
			}
			return found;
		}
		
		
		public static void ForEachIndex<T>(this IList<T> list, Action<T, int> action)
		{
			if (list == null)
			{
				return;
			}
            int i = 0;
			foreach (T t in list)
			{
#pragma warning disable RECS0017 // Possible compare of value type with 'null'
                if (t != null)
#pragma warning restore RECS0017 // Possible compare of value type with 'null'
                {
                    action(t, i);
				}
                ++i;
			}
		}
		
		public static void RemoveOne<T>(this IList<T> list, Predicate<T> predicate)
		{
			if (list == null)
			{
				return;
			}
			for (int i = 0; i < list.Count; ++i)
			{
				if (list[i] != null&&predicate(list[i]))
				{
					list.RemoveAt(i);
					return;
				}
			}
		}
		
		public static void Remove<T>(this IList<T> list, Predicate<T> predicate)
		{
			if (list == null)
			{
				return;
			}
			for (int i = 0; i < list.Count; ++i)
			{
				if (list[i] != null&&predicate(list[i]))
				{
					list.RemoveAt(i);
				}
			}
		}
		
		/// <summary>
		/// Serialize the specified list and index.
		/// </summary>
		/// <param name="list">List.</param>
		/// <param name="index">Index. (one based)</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public static List<T> Serialize<T>(this IList<T> list, IList<int> index, int indexBase = 0)
		{
			if (list == null)
			{
				return new List<T>();
			}
			List<T> ordered = new List<T>(list.Count);
			for (int i = 0; i < index.Count; ++i)
			{
				while (ordered.Count < index[i]-indexBase+1)
				{
					ordered.Add(default(T));
				}
				ordered[index[i]-indexBase] = list[i];
			}
			return ordered;
		}
		
		public static void Resize<T>(this IList<T> list, int size)
		{
			if (list.Count < size)
			{
				while (list.Count < size)
				{
					list.Add(default(T));
				}
			} else
			{
				for (int i = list.Count-1; i >= size; --i)
				{
					list.RemoveAt(i);
				}
			}
		}
		
		public static void Set<T>(this IList<T> list, int i, T val)
		{
			if (list.Count < i+1)
			{
				list.Resize(i+1);
			}
			list[i] = val;
		}

        public static IList<T> UnionWith<T>(this IList<T> list, IEnumerable<T> items)
        {
            if (list == null)
            {
                list = new List<T>(items);
            } else
            {
                foreach (var i in items)
                {
                    if (!list.Contains(i))
                    {
                        list.Add(i);
                    }
                }
            }
            return list;
        }

        public static void InsertToSorted<T>(this List<T> list, T val)
        {
            var i = list.BinarySearch(val);
            list.Insert((i >= 0) ? i : ~i, val);
        }

        public static T BinarySearchFirst<T>(this IList<T> list, System.Func<T, int> predicate)
        {
            if (list == null)
                return default;
            int min = 0;
            int max = list.Count;
            while (min < max)
            {
                int mid = (max + min) / 2;
                T midItem = list[mid];
                int comp = predicate(midItem);
                if (comp < 0)
                {
                    min = mid + 1;
                }
                else if (comp > 0)
                {
                    max = mid - 1;
                }
                else
                {
                    return midItem;
                }
            }
            if (min == max &&
                min < list.Count &&
                predicate(list[min]) == 0)
            {
                return list[min];
            }
            return default;
        }

        public static int BinarySearch<T>(this IList<T> list, int start, int size, Func<T, int> compare)
        {
            Debug.Assert(start >= 0);
            Debug.Assert(size >= 1);
            int end = start + size - 1;
            while (start <= end)
            {
                int mid = (start + end) / 2;
                int c = compare(list[mid]);
                if (c == 0)
                {
                    return mid;
                }
                else if (c < 0)
                {
                    end = mid - 1;
                }
                else
                {
                    start = mid + 1;
                }
            }
            return -start;
        }
    }
}
