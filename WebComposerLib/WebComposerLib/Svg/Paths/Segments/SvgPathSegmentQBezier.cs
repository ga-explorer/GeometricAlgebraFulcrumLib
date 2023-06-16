using System.Text;
using WebComposerLib.Html.Media;
using WebComposerLib.Svg.Values;

namespace WebComposerLib.Svg.Paths.Segments
{
    /// <summary>
    /// A Quadratic Bezier Curve Path Segment
    /// </summary>
    public sealed class SvgPathSegmentQBezier : ISvgPathSegment
    {
        public static SvgPathSegmentQBezier Create(double endPointX, double endPointY, double controlPointX, double controlPointY)
        {
            return new SvgPathSegmentQBezier
            {
                EndPointX = endPointX,
                EndPointY = endPointY,
                ControlPointX = controlPointX,
                ControlPointY = controlPointY
            };
        }


        public double EndPointX { get; set; }

        public double EndPointY { get; set; }

        public double ControlPointX { get; set; }

        public double ControlPointY { get; set; }


        public string SegmentText(SvgValueLengthUnit unit)
        {
            return new StringBuilder()
                .Append(ControlPointX.ToSvgLengthText(unit))
                .Append(",")
                .Append(ControlPointY.ToSvgLengthText(unit))
                .Append(" ")
                .Append(EndPointX.ToSvgLengthText(unit))
                .Append(",")
                .Append(EndPointY.ToSvgLengthText(unit))
                .ToString();
        }
    }
}
