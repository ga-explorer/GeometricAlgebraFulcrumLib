namespace CodeComposerLib.GraphViz.Dot.Value
{
    /// <summary>
    /// This class represents a graph layout optimization mode value
    /// See http://www.graphviz.org/content/attrs for more details
    /// </summary>
    public sealed class DotLayoutOptMode : DotStoredValue
    {
        public static readonly DotLayoutOptMode Major = new DotLayoutOptMode("major");

        public static readonly DotLayoutOptMode Kk = new DotLayoutOptMode("kk");

        public static readonly DotLayoutOptMode Hier = new DotLayoutOptMode("hier");

        public static readonly DotLayoutOptMode IpSep = new DotLayoutOptMode("ipsep");

        public static readonly DotLayoutOptMode Spring = new DotLayoutOptMode("spring");

        public static readonly DotLayoutOptMode Maxent = new DotLayoutOptMode("maxent");


        private DotLayoutOptMode(string value)
            : base(value)
        {
        }
    }
}