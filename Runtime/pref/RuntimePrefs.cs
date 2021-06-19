namespace mulova.unicore
{
    [System.Diagnostics.DebuggerStepThrough]
    public class RuntimePrefs : PreferenceImpl
    {
        public RuntimePrefs() : base(new RuntimePrefImpl()) { }
    }
}
