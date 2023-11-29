﻿//----------------------------------------------
// Unity3D UI switch library
// License: The MIT License ( http://opensource.org/licenses/MIT )
// Copyright mulova@gmail.com
//----------------------------------------------

namespace mulova.unicore
{
    using System;
    using UnityEngine;

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
