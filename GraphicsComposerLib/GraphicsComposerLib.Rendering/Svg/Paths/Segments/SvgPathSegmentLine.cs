using System.Text;
using GraphicsComposerLib.Rendering.Svg.Values;

namespace GraphicsComposerLib.Rendering.Svg.Paths.Segments
{
    /// <summary>
    /// A Line Path Segment
    /// </summary>
    public sealed class SvgPathSegmentLine : ISvgPathSegment
    {
        public static SvgPathSegmentLine Create(double endPointX, double endPointY)
        {
            return new SvgPathSegmentLine
            {
                EndPointX = endPointX,
                EndPointY = endPointY
            };
        }


        public double EndPointX { get; set; }

        public double EndPointY { get; set; }


        public string SegmentText(SvgValueLengthUnit unit)
        {
            return new StringBuilder()
                .Append(EndPointX.ToSvgLengthText(unit))
                .Append(",")
                .Append(EndPointY.ToSvgLengthText(unit))
                .ToString();
        }
    }
}