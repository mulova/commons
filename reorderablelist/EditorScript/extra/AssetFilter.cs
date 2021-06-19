//----------------------------------------------
// Unity3D common libraries and editor tools
// License: The MIT License ( http://opensource.org/licenses/MIT )
// Copyright © 2013- mulova@gmail.com
//----------------------------------------------
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace mulova.unicore
{
    public class AssetFilter
    {
        private HashSet<string> filtered;
        public AssetFilter(string filter)
        {
            SetFilter(filter);
        }

        public void SetFilter(string filter)
        {
            filtered = new HashSet<string>(AssetDatabase.FindAssets(filter));
        }

        public bool Filter(Object obj)
        {
            return filtered.Contains(AssetDatabase.GetAssetPath(obj));
        }
    }
}

