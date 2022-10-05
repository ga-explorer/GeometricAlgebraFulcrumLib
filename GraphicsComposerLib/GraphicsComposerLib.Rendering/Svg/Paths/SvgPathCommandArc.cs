using GraphicsComposerLib.Rendering.Svg.Paths.Segments;
using GraphicsComposerLib.Rendering.Svg.Values;

namespace GraphicsComposerLib.Rendering.Svg.Paths
{
    /// <summary>
    /// Elliptical Arc Path Command
    /// </summary>
    public sealed class SvgPathCommandArc : SvgSegmentsPathCommand<SvgPathSegmentArc>
    {
        public static SvgPathCommandArc Create()
        {
            return new SvgPathCommandArc();
        }

        public static SvgPathCommandArc Create(bool isRelative)
        {
            return new SvgPathCommandArc() { IsRelative = isRelative };
        }

        public static SvgPathCommandArc Create(bool isRelative, SvgValueLengthUnit unit)
        {
            var command = new SvgPathCommandArc { IsRelative = isRelative };

            command.Segments.Unit = unit;

            return command;
        }

        public static SvgPathCommandArc Create(SvgPathSegmentArc segment)
        {
            var command = new SvgPathCommandArc();

            command.Segments.AddSegment(segment);

            return command;
        }

        public static SvgPathCommandArc Create(bool isRelative, SvgPathSegmentArc segment)
        {
            var command = new SvgPathCommandArc { IsRelative = isRelative };

            command.Segments.AddSegment(segment);

            return command;
        }

        public static SvgPathCommandArc Create(bool isRelative, SvgValueLengthUnit unit, SvgPathSegmentArc segment)
        {
            var command = new SvgPathCommandArc { IsRelative = isRelative };

            command.Segments.Unit = unit;
            command.Segments.AddSegment(segment);

            return command;
        }

        public static SvgPathCommandArc Create(IEnumerable<SvgPathSegmentArc> segments)
        {
            var command = new SvgPathCommandArc();

            command.Segments.AddSegments(segments);

            return command;
        }

        public static SvgPathCommandArc Create(bool isRelative, IEnumerable<SvgPathSegmentArc> segments)
        {
            var command = new SvgPathCommandArc { IsRelative = isRelative };

            command.Segments.AddSegments(segments);

            return command;
        }

        public static SvgPathCommandArc Create(bool isRelative, SvgValueLengthUnit unit, IEnumerable<SvgPathSegmentArc> segments)
        {
            var command = new SvgPathCommandArc { IsRelative = isRelative };

            command.Segments.Unit = unit;
            command.Segments.AddSegments(segments);

            return command;
        }

        public static SvgPathCommandArc Create(params SvgPathSegmentArc[] segments)
        {
            var command = new SvgPathCommandArc();

            command.Segments.AddSegments(segments);

            return command;
        }

        public static SvgPathCommandArc Create(bool isRelative, params SvgPathSegmentArc[] segments)
        {
            var command = new SvgPathCommandArc { IsRelative = isRelative };

            command.Segments.AddSegments(segments);

            return command;
        }

        public static SvgPathCommandArc Create(bool isRelative, SvgValueLengthUnit unit, params SvgPathSegmentArc[] segments)
        {
            var command = new SvgPathCommandArc { IsRelative = isRelative };

            command.Segments.Unit = unit;
            command.Segments.AddSegments(segments);

            return command;
        }


        public override char CommandSymbol => IsRelative ? 'a' : 'A';


        private SvgPathCommandArc()
        {
        }
    }
}