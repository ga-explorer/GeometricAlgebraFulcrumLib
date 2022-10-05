namespace GraphicsComposerLib.Rendering.GraphViz.Dot.Value
{
    /// <summary>
    /// This class represents a vertical location value
    /// See http://www.graphviz.org/content/attrs for more details
    /// </summary>
    public sealed class DotVerticalLocation : DotStoredValue
    {
        public static readonly DotVerticalLocation Top = new DotVerticalLocation("t");

        public static readonly DotVerticalLocation Bottom = new DotVerticalLocation("b");

        public static readonly DotVerticalLocation Center = new DotVerticalLocation("c");


        private DotVerticalLocation(string value)
            : base(value)
        {
        }
    }
}
