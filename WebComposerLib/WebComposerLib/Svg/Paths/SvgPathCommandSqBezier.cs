using WebComposerLib.Svg.Paths.Segments;
using WebComposerLib.Svg.Values;

namespace WebComposerLib.Svg.Paths
{
    /// <summary>
    /// Smooth Quadratic Bezier Path Command
    /// </summary>
    public sealed class SvgPathCommandSqBezier : SvgSegmentsPathCommand<SvgPathSegmentSqBezier>
    {
        public static SvgPathCommandSqBezier Create()
        {
            return new SvgPathCommandSqBezier();
        }

        public static SvgPathCommandSqBezier Create(bool isRelative)
        {
            return new SvgPathCommandSqBezier() { IsRelative = isRelative };
        }

        public static SvgPathCommandSqBezier Create(bool isRelative, SvgValueLengthUnit unit)
        {
            var command = new SvgPathCommandSqBezier { IsRelative = isRelative };

            command.Segments.Unit = unit;

            return command;
        }

        public static SvgPathCommandSqBezier Create(SvgPathSegmentSqBezier segment)
        {
            var command = new SvgPathCommandSqBezier();

            command.Segments.AddSegment(segment);

            return command;
        }

        public static SvgPathCommandSqBezier Create(bool isRelative, SvgPathSegmentSqBezier segment)
        {
            var command = new SvgPathCommandSqBezier { IsRelative = isRelative };

            command.Segments.AddSegment(segment);

            return command;
        }

        public static SvgPathCommandSqBezier Create(bool isRelative, SvgValueLengthUnit unit, SvgPathSegmentSqBezier segment)
        {
            var command = new SvgPathCommandSqBezier { IsRelative = isRelative };

            command.Segments.Unit = unit;
            command.Segments.AddSegment(segment);

            return command;
        }

        public static SvgPathCommandSqBezier Create(IEnumerable<SvgPathSegmentSqBezier> segments)
        {
            var command = new SvgPathCommandSqBezier();

            command.Segments.AddSegments(segments);

            return command;
        }

        public static SvgPathCommandSqBezier Create(bool isRelative, IEnumerable<SvgPathSegmentSqBezier> segments)
        {
            var command = new SvgPathCommandSqBezier { IsRelative = isRelative };

            command.Segments.AddSegments(segments);

            return command;
        }

        public static SvgPathCommandSqBezier Create(bool isRelative, SvgValueLengthUnit unit, IEnumerable<SvgPathSegmentSqBezier> segments)
        {
            var command = new SvgPathCommandSqBezier { IsRelative = isRelative };

            command.Segments.Unit = unit;
            command.Segments.AddSegments(segments);

            return command;
        }

        public static SvgPathCommandSqBezier Create(params SvgPathSegmentSqBezier[] segments)
        {
            var command = new SvgPathCommandSqBezier();

            command.Segments.AddSegments(segments);

            return command;
        }

        public static SvgPathCommandSqBezier Create(bool isRelative, params SvgPathSegmentSqBezier[] segments)
        {
            var command = new SvgPathCommandSqBezier { IsRelative = isRelative };

            command.Segments.AddSegments(segments);

            return command;
        }

        public static SvgPathCommandSqBezier Create(bool isRelative, SvgValueLengthUnit unit, params SvgPathSegmentSqBezier[] segments)
        {
            var command = new SvgPathCommandSqBezier { IsRelative = isRelative };

            command.Segments.Unit = unit;
            command.Segments.AddSegments(segments);

            return command;
        }



        public override char CommandSymbol => IsRelative ? 't' : 'T';


        private SvgPathCommandSqBezier()
        {
        }
    }
}