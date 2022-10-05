namespace GraphicsComposerLib.Rendering.GraphViz.Dot.Value
{
    /// <summary>
    /// This class represents a smoothing type value
    /// See http://www.graphviz.org/content/attrs#ksmoothType for more details
    /// </summary>
    public sealed class DotSmoothType : DotStoredValue
    {
        public static readonly DotSmoothType None = new DotSmoothType("none");

        public static readonly DotSmoothType AverageDist = new DotSmoothType("avg_dist");

        public static readonly DotSmoothType GraphDist = new DotSmoothType("graph_dist");

        public static readonly DotSmoothType PowerDist = new DotSmoothType("power_dist");

        public static readonly DotSmoothType Ring = new DotSmoothType("rng");

        public static readonly DotSmoothType Spring = new DotSmoothType("spring");

        public static readonly DotSmoothType Triangle = new DotSmoothType("triangle");


        private DotSmoothType(string value)
            : base(value)
        {
        }
    }
}