namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.GraphViz.Dot.Value
{
    /// <summary>
    /// This class represents a spline type value
    /// See http://www.graphviz.org/content/attrs for more details
    /// </summary>
    public sealed class DotSplines : DotStoredValue
    {
        public static readonly DotSplines None = new DotSplines("none");

        /// <summary>
        /// Same as Spline
        /// </summary>
        public static readonly DotSplines True = new DotSplines("true");

        /// <summary>
        /// Same as Line
        /// </summary>
        public static readonly DotSplines False = new DotSplines("false");

        /// <summary>
        /// Same as False
        /// </summary>
        public static readonly DotSplines Line = new DotSplines("line");

        /// <summary>
        /// Same as True
        /// </summary>
        public static readonly DotSplines Spline = new DotSplines("spline");

        public static readonly DotSplines Polyline = new DotSplines("polyline");

        public static readonly DotSplines Ortho = new DotSplines("ortho");

        public static readonly DotSplines Curved = new DotSplines("curved");

        public static readonly DotSplines Compound = new DotSplines("compound");


        private DotSplines(string value)
            : base(value)
        {
        }
    }
}
