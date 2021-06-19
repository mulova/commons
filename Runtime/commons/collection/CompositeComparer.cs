//----------------------------------------------
// Unity3D common libraries and editor tools
// License: The MIT License ( http://opensource.org/licenses/MIT )
// Copyright © 2013- mulova@gmail.com
//----------------------------------------------

using System.Collections;
using System.Collections.Generic;

namespace mulova.commons {

	public class CompositeComparer : IComparer
	{
		private IComparer[] elements;
		public CompositeComparer(params IComparer[] elements) {
			this.elements = elements;
		}

		public int Compare(object x, object y)
		{
			foreach (IComparer c in elements) {
				int result = c.Compare(x, y);
				if (result != 0) {
					return result;
				}
			}
			return 0;
		}
	}

	public class CompositeComparer<T> : IComparer<T>
	{
		private IComparer<T>[] elements;
		public CompositeComparer(params IComparer<T>[] elements) {
			this.elements = elements;
		}

		public int Compare(T x, T y)
		{
			foreach (IComparer<T> c in elements) {
				int result = c.Compare(x, y);
				if (result != 0) {
					return result;
				}
			}
			return 0;
		}
	}
	
}