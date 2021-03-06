﻿using UnityEngine;
using UnityEditor;
using mulova.commons;
using System.Collections.Generic.Ex;

namespace mulova.unicore
{
    public class AssetLabel : EnumClass<AssetLabel> {
        public static readonly AssetLabel ALPHA_SD = new AssetLabel("Alpha_sd");
        
        private AssetLabel(string label) : base(label) {}
        
        public bool Is(Object asset)
        {
            string[] labels = AssetDatabase.GetLabels(asset);
            if (!labels.IsEmpty())
            {
                foreach (string l in labels)
                {
                    if (id == l)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
