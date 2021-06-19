//----------------------------------------------
// Unity3D common libraries and editor tools
// License: The MIT License ( http://opensource.org/licenses/MIT )
// Copyright © 2013- mulova@gmail.com
//----------------------------------------------

using System.Collections.Generic;

namespace System.Ex
{
	public static partial class ArrayEx
    {

        public static T Random<T>(params T[] arr)
        {
            return arr[UnityEngine.Random.Range(0, arr.Length)];
        }

    }
}
