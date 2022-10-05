namespace GraphicsComposerLib.Rendering.GraphViz.Dot.Value
{
    /// <summary>
    /// This class represents an alignment value
    /// See http://www.graphviz.org/content/attrs for more details
    /// </summary>
    public sealed class DotAlign : DotStoredValue
    {
        public static readonly DotAlign Left = new DotAlign("left");

        public static readonly DotAlign Right = new DotAlign("right");
        
        public static readonly DotAlign Center = new DotAlign("center");


        private DotAlign(string value)
            : base(value)
        {
        }
    }
}
