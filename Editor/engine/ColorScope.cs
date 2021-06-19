//----------------------------------------------
// Unity3D common libraries and editor tools
// License: The MIT License ( http://opensource.org/licenses/MIT )
// Copyright © 2013- mulova@gmail.com
//----------------------------------------------
using System;
using UnityEngine;

namespace mulova.unicore
{
    public class ColorScope : IDisposable
    {
        private Color old;
        public ColorScope(Color c, bool apply = true)
        {
            old = GUI.color;
            if (apply)
            {
                GUI.color = c;
            }
        }

        public void Dispose()
        {
            GUI.color = old;
        }
    }
}
