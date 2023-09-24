using System.Text;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.GraphViz.Dot.Color
{
    /// <summary>
    /// This class represents a color list value
    /// See http://www.graphviz.org/content/attrs#kcolor
    /// and http://www.graphviz.org/content/attrs#kcolorList for more details
    /// </summary>
    public sealed class DotColorList : DotValue
    {
        internal List<DotColor> ColorsList = new List<DotColor>();

        internal List<float> WeightsList = new List<float>();

        /// <summary>
        /// The colors of this color list
        /// </summary>
        public IEnumerable<DotColor> Colors => ColorsList;

        /// <summary>
        /// The weights of this color list
        /// </summary>
        public IEnumerable<float> Weights => WeightsList;


        public override string Value
        {
            get
            {
                var s = new StringBuilder(7);

                for (var i = 0; i < ColorsList.Count; i++)
                {
                    if (i > 0) s.Append(":");

                    s.Append(ColorsList[i].Value);

                    if (i < WeightsList.Count && WeightsList[i] > 0)
                        s.Append(";").Append(WeightsList[i].ToDotDouble());
                }

                return s.ToString();
            }
        }


        internal DotColorList(IEnumerable<DotColor> colors)
        {
            ColorsList.AddRange(colors);
        }

        internal DotColorList(IEnumerable<DotColor> colors, IEnumerable<float> weights)
        {
            ColorsList.AddRange(colors);
            WeightsList.AddRange(weights);

            if (WeightsList.Count > ColorsList.Count)
                WeightsList.Capacity = ColorsList.Count;
        }
    }
}
