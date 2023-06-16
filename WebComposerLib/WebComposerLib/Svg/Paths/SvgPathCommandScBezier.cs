using WebComposerLib.Svg.Paths.Segments;
using WebComposerLib.Svg.Values;

namespace WebComposerLib.Svg.Paths
{
    /// <summary>
    /// Smooth Cubic Bezier Path Command
    /// </summary>
    public sealed class SvgPathCommandScBezier : SvgSegmentsPathCommand<SvgPathSegmentScBezier>
    {
        public static SvgPathCommandScBezier Create()
        {
            return new SvgPathCommandScBezier();
        }

        public static SvgPathCommandScBezier Create(bool isRelative)
        {
            return new SvgPathCommandScBezier() { IsRelative = isRelative };
        }

        public static SvgPathCommandScBezier Create(bool isRelative, SvgValueLengthUnit unit)
        {
            var command = new SvgPathCommandScBezier { IsRelative = isRelative };

            command.Segments.Unit = unit;

            return command;
        }

        public static SvgPathCommandScBezier Create(SvgPathSegmentScBezier segment)
        {
            var command = new SvgPathCommandScBezier();

            command.Segments.AddSegment(segment);

            return command;
        }

        public static SvgPathCommandScBezier Create(bool isRelative, SvgPathSegmentScBezier segment)
        {
            var command = new SvgPathCommandScBezier { IsRelative = isRelative };

            command.Segments.AddSegment(segment);

            return command;
        }

        public static SvgPathCommandScBezier Create(bool isRelative, SvgValueLengthUnit unit, SvgPathSegmentScBezier segment)
        {
            var command = new SvgPathCommandScBezier { IsRelative = isRelative };

            command.Segments.Unit = unit;
            command.Segments.AddSegment(segment);

            return command;
        }

        public static SvgPathCommandScBezier Create(IEnumerable<SvgPathSegmentScBezier> segments)
        {
            var command = new SvgPathCommandScBezier();

            command.Segments.AddSegments(segments);

            return command;
        }

        public static SvgPathCommandScBezier Create(bool isRelative, IEnumerable<SvgPathSegmentScBezier> segments)
        {
            var command = new SvgPathCommandScBezier { IsRelative = isRelative };

            command.Segments.AddSegments(segments);

            return command;
        }

        public static SvgPathCommandScBezier Create(bool isRelative, SvgValueLengthUnit unit, IEnumerable<SvgPathSegmentScBezier> segments)
        {
            var command = new SvgPathCommandScBezier { IsRelative = isRelative };

            command.Segments.Unit = unit;
            command.Segments.AddSegments(segments);

            return command;
        }

        public static SvgPathCommandScBezier Create(params SvgPathSegmentScBezier[] segments)
        {
            var command = new SvgPathCommandScBezier();

            command.Segments.AddSegments(segments);

            return command;
        }

        public static SvgPathCommandScBezier Create(bool isRelative, params SvgPathSegmentScBezier[] segments)
        {
            var command = new SvgPathCommandScBezier { IsRelative = isRelative };

            command.Segments.AddSegments(segments);

            return command;
        }

        public static SvgPathCommandScBezier Create(bool isRelative, SvgValueLengthUnit unit, params SvgPathSegmentScBezier[] segments)
        {
            var command = new SvgPathCommandScBezier { IsRelative = isRelative };

            command.Segments.Unit = unit;
            command.Segments.AddSegments(segments);

            return command;
        }



        public override char CommandSymbol => IsRelative ? 's' : 'S';


        private SvgPathCommandScBezier()
        {
        }
    }
}