//----------------------------------------------
// Unity3D common libraries and editor tools
// License: The MIT License ( http://opensource.org/licenses/MIT )
// Copyright © 2013- mulova@gmail.com
//----------------------------------------------
using System;
using UnityEngine;

namespace mulova.unicore
{
    public class ContentColorScope : IDisposable
    {
        private Color old;
        public ContentColorScope(Color c, bool apply = true)
        {
            old = GUI.contentColor;
            if (apply)
            {
                GUI.contentColor = c;
            }
        }

        public void Dispose()
        {
            GUI.contentColor = old;
        }
    }
}
