//----------------------------------------------
// Unity3D common libraries and editor tools
// License: The MIT License ( http://opensource.org/licenses/MIT )
// Copyright © 2013- mulova@gmail.com
//----------------------------------------------
using System;
using UnityEngine;

namespace mulova.unicore
{
    public class BgColorScope : IDisposable
    {
        private Color old;
        public BgColorScope(Color c, bool apply = true)
        {
            old = GUI.backgroundColor;
            if (apply)
            {
                GUI.backgroundColor = c;
            }
        }

        public void Dispose()
        {
            GUI.backgroundColor = old;
        }
    }
}
