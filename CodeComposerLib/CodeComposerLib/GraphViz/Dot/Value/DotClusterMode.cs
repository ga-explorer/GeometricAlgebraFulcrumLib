namespace CodeComposerLib.GraphViz.Dot.Value
{
    /// <summary>
    /// This class represents a cluster mode value
    /// See http://www.graphviz.org/content/attrs for more details
    /// </summary>
    public sealed class DotClusterMode : DotStoredValue
    {
        public static readonly DotClusterMode None = new DotClusterMode("none");

        public static readonly DotClusterMode Local = new DotClusterMode("local");

        public static readonly DotClusterMode Global = new DotClusterMode("global");


        private DotClusterMode(string value)
            : base(value)
        {
        }
    }
}