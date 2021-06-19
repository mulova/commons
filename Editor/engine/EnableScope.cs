//----------------------------------------------
// Unity3D common libraries and editor tools
// License: The MIT License ( http://opensource.org/licenses/MIT )
// Copyright © 2013- mulova@gmail.com
//----------------------------------------------
using System;
using UnityEngine;

namespace mulova.unicore
{
    public class EnableScope : IDisposable
    {
        private bool enabled;
        public EnableScope(bool e, bool overwrite = true)
        {
            enabled = GUI.enabled;
            if (overwrite)
            {
                GUI.enabled = e;
            }
            else
            {
                GUI.enabled &= e;
            }
        }

        public void Dispose()
        {
            GUI.enabled = enabled;
        }
    }
}
