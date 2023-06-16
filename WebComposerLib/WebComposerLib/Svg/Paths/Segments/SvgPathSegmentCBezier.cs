using System.Text;
using WebComposerLib.Html.Media;
using WebComposerLib.Svg.Values;

namespace WebComposerLib.Svg.Paths.Segments
{
    /// <summary>
    /// A Cubic Bezier Curve Path Segment
    /// </summary>
    public sealed class SvgPathSegmentCBezier : ISvgPathSegment
    {
        public static SvgPathSegmentCBezier Create(double endPointX, double endPointY, double controlStartPointX, double controlStartPointY, double controlEndPointX, double controlEndPointY)
        {
            return new SvgPathSegmentCBezier
            {
                EndPointX = endPointX,
                EndPointY = endPointY,
                ControlStartPointX = controlStartPointX,
                ControlStartPointY = controlStartPointY,
                ControlEndPointX = controlEndPointX,
                ControlEndPointY = controlEndPointY
            };
        }


        public double EndPointX { get; set; }

        public double EndPointY { get; set; }

        public double ControlStartPointX { get; set; }

        public double ControlStartPointY { get; set; }

        public double ControlEndPointX { get; set; }

        public double ControlEndPointY { get; set; }


        public string SegmentText(SvgValueLengthUnit unit)
        {
            return new StringBuilder()
                .Append(ControlStartPointX.ToSvgLengthText(unit))
                .Append(",")
                .Append(ControlStartPointY.ToSvgLengthText(unit))
                .Append(" ")
                .Append(ControlEndPointX.ToSvgLengthText(unit))
                .Append(",")
                .Append(ControlEndPointY.ToSvgLengthText(unit))
                .Append(" ")
                .Append(EndPointX.ToSvgLengthText(unit))
                .Append(",")
                .Append(EndPointY.ToSvgLengthText(unit))
                .ToString();
        }
    }
}