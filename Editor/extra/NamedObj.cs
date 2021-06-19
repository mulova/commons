//----------------------------------------------
// Common library for Unity3D libraries
// License: The MIT License ( http://opensource.org/licenses/MIT )
// Copyright © 2013- mulova@gmail.com
//----------------------------------------------

using Object = UnityEngine.Object;

namespace mulova.unicore
{
	public interface NamedObj {
		Object Obj { get; }
		string Name { get; }
	}
}

