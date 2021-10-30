namespace GraphicsComposerLib.GraphViz.Dot.Value
{
    /// <summary>
    /// This class represents a graph ranking direction value
    /// See http://www.graphviz.org/content/attrs for more details
    /// </summary>
    public sealed class DotRankDirection : DotStoredValue
    {
        public static readonly DotRankDirection TopToBottom = new DotRankDirection("TB");

        public static readonly DotRankDirection BottomToTop = new DotRankDirection("BT");

        public static readonly DotRankDirection LeftToRight = new DotRankDirection("LR");

        public static readonly DotRankDirection RightToLeft = new DotRankDirection("RL");


        private DotRankDirection(string value)
            : base(value)
        {
        }
    }
}
