using System.Text;

namespace GraphicsComposerLib.Rendering.GraphViz.Dot.Color
{
    /// <summary>
    /// This class represents an indixed color value
    /// See http://www.graphviz.org/content/attrs#kcolor
    /// and http://www.graphviz.org/content/attrs#kcolorList 
    /// and http://www.graphviz.org/content/color-names for more details
    /// </summary>
    public sealed class DotIndexedColor : DotColor
    {
        public DotColorScheme Scheme { get; }

        public int Index { get; }

        public override string Value
        {
            get
            {
                var dotIndex = (Index + 1).ToString();

                var s = new StringBuilder();

                if (Scheme != null)
                    s.Append('/').Append(Scheme.Value).Append('/').Append(dotIndex);

                else
                    s.Append(dotIndex);

                return s.ToString();
            }
        }

        internal DotIndexedColor(int index)
        {
            Scheme = null;
            Index = index;
        }

        internal DotIndexedColor(DotColorScheme scheme, int index)
        {
            if (scheme.IsIndexed == false)
                throw new InvalidOperationException("Color scheme is not indexed");

            Scheme = scheme;
            Index = index;
        }
    }
}
