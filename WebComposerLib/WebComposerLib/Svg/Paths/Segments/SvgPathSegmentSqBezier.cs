using System.Text;
using WebComposerLib.Html.Media;
using WebComposerLib.Svg.Values;

namespace WebComposerLib.Svg.Paths.Segments
{
    /// <summary>
    /// A Smooth Quadratic Bezier Curve Path Segment
    /// </summary>
    public sealed class SvgPathSegmentSqBezier : ISvgPathSegment
    {
        public static SvgPathSegmentSqBezier Create(double endPointX, double endPointY)
        {
            return new SvgPathSegmentSqBezier
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