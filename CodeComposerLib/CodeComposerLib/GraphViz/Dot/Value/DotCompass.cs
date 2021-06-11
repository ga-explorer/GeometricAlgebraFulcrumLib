namespace CodeComposerLib.GraphViz.Dot.Value
{
    /// <summary>
    /// This class represents a compass direction value
    /// See http://www.graphviz.org/content/attrs for more details
    /// </summary>
    public sealed class DotCompass : DotStoredValue
    {
        public static readonly DotCompass Default = new DotCompass("_");

        public static readonly DotCompass North = new DotCompass("n");
        
        public static readonly DotCompass NorthEast = new DotCompass("ne");
        
        public static readonly DotCompass East = new DotCompass("e");
        
        public static readonly DotCompass SouthEast = new DotCompass("se");
        
        public static readonly DotCompass South = new DotCompass("s");
        
        public static readonly DotCompass SouthWest = new DotCompass("sw");
        
        public static readonly DotCompass West = new DotCompass("w");
        
        public static readonly DotCompass NorthWest = new DotCompass("nw");
        
        public static readonly DotCompass Center = new DotCompass("c");


        private DotCompass(string value)
            : base(value)
        {
        }
    }
}
