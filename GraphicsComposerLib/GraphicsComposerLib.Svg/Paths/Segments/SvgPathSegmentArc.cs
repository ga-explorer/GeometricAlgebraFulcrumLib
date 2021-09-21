using System.Text;
using GraphicsComposerLib.Svg.Values;

namespace GraphicsComposerLib.Svg.Paths.Segments
{
    public sealed class SvgPathSegmentArc : ISvgPathSegment
    {
        public static SvgPathSegmentArc Create(double endPointX, double endPointY, double radiusX, double radiusY, double xRotationAngle, bool largsArcFlag, bool sweepFlag)
        {
            return new SvgPathSegmentArc
            {
                EndPointX = endPointX,
                EndPointY = endPointY,
                RadiusX = radiusX,
                RadiusY = radiusY,
                XRotationAngle = xRotationAngle,
                LargeArcFlag = largsArcFlag,
                SweepFlag = sweepFlag
            };
        }


        public double EndPointX { get; set; }

        public double EndPointY { get; set; }

        public double RadiusX { get; set; }

        public double RadiusY { get; set; }

        public double XRotationAngle { get; set; }

        public bool LargeArcFlag { get; set; }

        public bool SweepFlag { get; set; }


        public string SegmentText(SvgValueLengthUnit unit)
        {
            return new StringBuilder()
                .Append(RadiusX.ToSvgLengthText(unit))
                .Append(",")
                .Append(RadiusY.ToSvgLengthText(unit))
                .Append(" ")
                .Append(XRotationAngle.ToSvgLengthText(unit))
                .Append(" ")
                .Append(LargeArcFlag ? '1' : '0')
                .Append(",")
                .Append(SweepFlag ? '1' : '0')
                .Append(" ")
                .Append(EndPointX.ToSvgLengthText(unit))
                .Append(",")
                .Append(EndPointY.ToSvgLengthText(unit))
                .ToString();
        }
    }
}
