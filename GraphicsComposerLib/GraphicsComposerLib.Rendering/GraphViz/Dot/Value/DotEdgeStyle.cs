namespace GraphicsComposerLib.Rendering.GraphViz.Dot.Value
{
    /// <summary>
    /// This class represents an edge style value
    /// See http://www.graphviz.org/content/attrs for more details
    /// </summary>
    public sealed class DotEdgeStyle : DotStoredValue
    {
        public static readonly DotEdgeStyle Solid = new DotEdgeStyle("solid");
        
        public static readonly DotEdgeStyle Dashed = new DotEdgeStyle("dashed");
        
        public static readonly DotEdgeStyle Dotted = new DotEdgeStyle("dotted");
        
        public static readonly DotEdgeStyle Bold = new DotEdgeStyle("bold");
        
        public static readonly DotEdgeStyle Invisible = new DotEdgeStyle("invis");
        
        public static readonly DotEdgeStyle Tapered = new DotEdgeStyle("tapered");


        private DotEdgeStyle(string value)
            : base(value)
        {
        }
    }
}
