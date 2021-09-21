using System.Collections.Generic;
using GraphicsComposerLib.Svg.Paths.Segments;
using GraphicsComposerLib.Svg.Values;

namespace GraphicsComposerLib.Svg.Paths
{
    /// <summary>
    /// Cubic Bezier Path Command
    /// </summary>
    public sealed class SvgPathCommandCBezier : SvgSegmentsPathCommand<SvgPathSegmentCBezier>
    {
        public static SvgPathCommandCBezier Create()
        {
            return new SvgPathCommandCBezier();
        }

        public static SvgPathCommandCBezier Create(bool isRelative)
        {
            return new SvgPathCommandCBezier() { IsRelative = isRelative };
        }

        public static SvgPathCommandCBezier Create(bool isRelative, SvgValueLengthUnit unit)
        {
            var command = new SvgPathCommandCBezier { IsRelative = isRelative };

            command.Segments.Unit = unit;

            return command;
        }

        public static SvgPathCommandCBezier Create(SvgPathSegmentCBezier segment)
        {
            var command = new SvgPathCommandCBezier();

            command.Segments.AddSegment(segment);

            return command;
        }

        public static SvgPathCommandCBezier Create(bool isRelative, SvgPathSegmentCBezier segment)
        {
            var command = new SvgPathCommandCBezier { IsRelative = isRelative };

            command.Segments.AddSegment(segment);

            return command;
        }

        public static SvgPathCommandCBezier Create(bool isRelative, SvgValueLengthUnit unit, SvgPathSegmentCBezier segment)
        {
            var command = new SvgPathCommandCBezier { IsRelative = isRelative };

            command.Segments.Unit = unit;
            command.Segments.AddSegment(segment);

            return command;
        }

        public static SvgPathCommandCBezier Create(IEnumerable<SvgPathSegmentCBezier> segments)
        {
            var command = new SvgPathCommandCBezier();

            command.Segments.AddSegments(segments);

            return command;
        }

        public static SvgPathCommandCBezier Create(bool isRelative, IEnumerable<SvgPathSegmentCBezier> segments)
        {
            var command = new SvgPathCommandCBezier { IsRelative = isRelative };

            command.Segments.AddSegments(segments);

            return command;
        }

        public static SvgPathCommandCBezier Create(bool isRelative, SvgValueLengthUnit unit, IEnumerable<SvgPathSegmentCBezier> segments)
        {
            var command = new SvgPathCommandCBezier { IsRelative = isRelative };

            command.Segments.Unit = unit;
            command.Segments.AddSegments(segments);

            return command;
        }

        public static SvgPathCommandCBezier Create(params SvgPathSegmentCBezier[] segments)
        {
            var command = new SvgPathCommandCBezier();

            command.Segments.AddSegments(segments);

            return command;
        }

        public static SvgPathCommandCBezier Create(bool isRelative, params SvgPathSegmentCBezier[] segments)
        {
            var command = new SvgPathCommandCBezier { IsRelative = isRelative };

            command.Segments.AddSegments(segments);

            return command;
        }

        public static SvgPathCommandCBezier Create(bool isRelative, SvgValueLengthUnit unit, params SvgPathSegmentCBezier[] segments)
        {
            var command = new SvgPathCommandCBezier { IsRelative = isRelative };

            command.Segments.Unit = unit;
            command.Segments.AddSegments(segments);

            return command;
        }


        public override char CommandSymbol => IsRelative ? 'c' : 'C';


        private SvgPathCommandCBezier()
        {
        }
    }
}