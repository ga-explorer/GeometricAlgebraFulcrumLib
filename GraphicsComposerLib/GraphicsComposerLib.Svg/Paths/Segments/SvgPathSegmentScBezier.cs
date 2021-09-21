using System.Text;
using GraphicsComposerLib.Svg.Values;

namespace GraphicsComposerLib.Svg.Paths.Segments
{
    /// <summary>
    /// A Smooth Cubic Bezier Curve Path Segment
    /// </summary>
    public sealed class SvgPathSegmentScBezier : ISvgPathSegment
    {
        public static SvgPathSegmentScBezier Create(double endPointX, double endPointY, double controlEndPointX, double controlEndPointY)
        {
            return new SvgPathSegmentScBezier
            {
                EndPointX = endPointX,
                EndPointY = endPointY,
                ControlEndPointX = controlEndPointX,
                ControlEndPointY = controlEndPointY
            };
        }


        public double EndPointX { get; set; }

        public double EndPointY { get; set; }

        public double ControlEndPointX { get; set; }

        public double ControlEndPointY { get; set; }


        public string SegmentText(SvgValueLengthUnit unit)
        {
            return new StringBuilder()
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