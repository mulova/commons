//----------------------------------------------
// Unity3D common libraries and editor tools
// License: The MIT License ( http://opensource.org/licenses/MIT )
// Copyright © 2013- mulova@gmail.com
//----------------------------------------------

using System.Collections.Generic;

namespace mulova.commons {
	public class FixedSizeList<T>
	{
        private readonly LinkedList<T> list;
        private readonly int capacity;

		public FixedSizeList(int capacity) {
			this.capacity = capacity;
            this.list = new LinkedList<T>();
		}

		public void AddFirst(T t) {
			if (capacity == list.Count) {
				list.RemoveLast();
			}
            list.AddFirst(t);
		}

		public void AddLast(T t) {
			if (capacity == list.Count) {
                list.RemoveFirst();
			}
            list.AddLast(t);
		}

		public void AddFirst(IList<T> list) {
			for (int i=list.Count-1; i>=0; --i) {
                AddFirst(list[i]);
			}
		}
		
		public void AddLast(IList<T> list) {
			foreach (T t in list) {
                AddLast(t);
			}
		}

		public bool isFull {
            get
            {
                return this.capacity == list.Count;
            }
		}
	}
}

