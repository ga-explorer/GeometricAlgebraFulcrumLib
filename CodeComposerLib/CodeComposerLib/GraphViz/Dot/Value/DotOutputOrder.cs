namespace CodeComposerLib.GraphViz.Dot.Value
{
    /// <summary>
    /// This class represents the output ordering value
    /// See http://www.graphviz.org/content/attrs#doverlap for more details
    /// </summary>
    public sealed class DotOutputOrder : DotStoredValue
    {
        public static readonly DotOutputOrder BreadthFirst = new DotOutputOrder("breadthfirst");

        public static readonly DotOutputOrder NodesFirst = new DotOutputOrder("nodesfirst");

        public static readonly DotOutputOrder EdgesFirst = new DotOutputOrder("edgesfirst");


        private DotOutputOrder(string value)
            : base(value)
        {
        }
    }
}