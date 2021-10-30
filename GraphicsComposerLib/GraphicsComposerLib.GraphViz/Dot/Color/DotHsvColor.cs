using System;
using System.Text;

namespace GraphicsComposerLib.GraphViz.Dot.Color
{
    /// <summary>
    /// This class represents an HSV encoded color value
    /// See http://www.graphviz.org/content/attrs#kcolor
    /// and http://www.graphviz.org/content/attrs#kcolorList for more details
    /// </summary>
    public sealed class DotHsvColor : DotColor
    {
        public float H { get; }

        public float S { get; }

        public float V { get; }


        public override string Value
        {
            get
            {
                var s = new StringBuilder(7);

                s
                    .Append(H.ToDotDouble())
                    .Append(',')
                    .Append(S.ToDotDouble())
                    .Append(',')
                    .Append(V.ToDotDouble());

                return s.ToString();
            }
        }


        internal DotHsvColor(System.Drawing.Color color)
        {
            H = color.GetHue();
            S = color.GetSaturation();
            V = color.GetBrightness();
        }

        internal DotHsvColor(float h, float s, float v)
        {
            if (h < 0 || h > 1.0f)
                throw new ArgumentOutOfRangeException(nameof(h));

            if (s < 0 || s > 1.0f)
                throw new ArgumentOutOfRangeException(nameof(s));

            if (v < 0 || v > 1.0f)
                throw new ArgumentOutOfRangeException(nameof(v));

            H = h;
            S = s;
            V = v;
        }
    }
}
