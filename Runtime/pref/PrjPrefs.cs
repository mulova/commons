using System.Diagnostics;
using UnityEngine;

namespace mulova.unicore
{
    [DebuggerStepThrough]
    public partial class PrjPrefs : PreferenceImpl
    {
        public static readonly PrjPrefs global = new PrjPrefs(null);
        public static PrjPrefs _prj;
        public static PrjPrefs prj
        {
            get
            {
                if (_prj == null)
                {
    #if UNITY_EDITOR
                    string[] s = Application.dataPath.Split('/');
                    var prefix = s[s.Length - 2] + "/";
                    _prj = new PrjPrefs(prefix);
    #else
                    _prj = global;
    #endif
                }
                return _prj;
            }
        }

        public PrjPrefs(string prefix) : base(new EditorPrefImpl(prefix)) { }
    }
}

