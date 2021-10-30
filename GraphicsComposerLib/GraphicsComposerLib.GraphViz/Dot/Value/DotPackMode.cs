namespace GraphicsComposerLib.GraphViz.Dot.Value
{
    /// <summary>
    /// This class represents a graph packing mode value
    /// See http://www.graphviz.org/content/attrs#kpackMode for more details
    /// </summary>
    public sealed class DotPackMode : DotStoredValue
    {
        public static readonly DotPackMode Node = new DotPackMode("node");

        public static readonly DotPackMode Cluster = new DotPackMode("clust");

        public static readonly DotPackMode Graph = new DotPackMode("graph");


        private DotPackMode(string value)
            : base(value)
        {
        }
    }
}