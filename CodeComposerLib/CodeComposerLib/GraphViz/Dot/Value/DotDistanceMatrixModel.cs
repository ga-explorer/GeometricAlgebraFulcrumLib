namespace CodeComposerLib.GraphViz.Dot.Value
{
    /// <summary>
    /// This class represents a distance matrix computation model value
    /// See http://www.graphviz.org/content/attrs for more details
    /// </summary>
    public sealed class DotDistanceMatrixModel : DotStoredValue
    {
        public static readonly DotDistanceMatrixModel Circuit = new DotDistanceMatrixModel("circuit");

        public static readonly DotDistanceMatrixModel Subset = new DotDistanceMatrixModel("subset");

        public static readonly DotDistanceMatrixModel Mds = new DotDistanceMatrixModel("mds");


        private DotDistanceMatrixModel(string value)
            : base(value)
        {
        }
    }
}