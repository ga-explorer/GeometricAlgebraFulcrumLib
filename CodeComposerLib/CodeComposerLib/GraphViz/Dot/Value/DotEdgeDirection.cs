namespace CodeComposerLib.GraphViz.Dot.Value
{
    /// <summary>
    /// This class represents an edge direction value
    /// See http://www.graphviz.org/content/attrs for more details
    /// </summary>
    public sealed class DotEdgeDirection : DotStoredValue
    {
        public static readonly DotEdgeDirection Forward = new DotEdgeDirection("forward");
        
        public static readonly DotEdgeDirection Back = new DotEdgeDirection("back");
        
        public static readonly DotEdgeDirection Both = new DotEdgeDirection("both");
        
        public static readonly DotEdgeDirection None = new DotEdgeDirection("none");


        private DotEdgeDirection(string value)
            : base(value)
        {
        }
    }
}
