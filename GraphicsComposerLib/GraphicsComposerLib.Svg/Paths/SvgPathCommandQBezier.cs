using System.Collections.Generic;
using GraphicsComposerLib.Svg.Paths.Segments;
using GraphicsComposerLib.Svg.Values;

namespace GraphicsComposerLib.Svg.Paths
{
    /// <summary>
    /// Quadratic Bezier Path Command
    /// </summary>
    public sealed class SvgPathCommandQBezier : SvgSegmentsPathCommand<SvgPathSegmentQBezier>
    {
        public static SvgPathCommandQBezier Create()
        {
            return new SvgPathCommandQBezier();
        }

        public static SvgPathCommandQBezier Create(bool isRelative)
        {
            return new SvgPathCommandQBezier() { IsRelative = isRelative };
        }

        public static SvgPathCommandQBezier Create(bool isRelative, SvgValueLengthUnit unit)
        {
            var command = new SvgPathCommandQBezier { IsRelative = isRelative };

            command.Segments.Unit = unit;

            return command;
        }

        public static SvgPathCommandQBezier Create(SvgPathSegmentQBezier segment)
        {
            var command = new SvgPathCommandQBezier();

            command.Segments.AddSegment(segment);

            return command;
        }

        public static SvgPathCommandQBezier Create(bool isRelative, SvgPathSegmentQBezier segment)
        {
            var command = new SvgPathCommandQBezier { IsRelative = isRelative };

            command.Segments.AddSegment(segment);

            return command;
        }

        public static SvgPathCommandQBezier Create(bool isRelative, SvgValueLengthUnit unit, SvgPathSegmentQBezier segment)
        {
            var command = new SvgPathCommandQBezier { IsRelative = isRelative };

            command.Segments.Unit = unit;
            command.Segments.AddSegment(segment);

            return command;
        }

        public static SvgPathCommandQBezier Create(IEnumerable<SvgPathSegmentQBezier> segments)
        {
            var command = new SvgPathCommandQBezier();

            command.Segments.AddSegments(segments);

            return command;
        }

        public static SvgPathCommandQBezier Create(bool isRelative, IEnumerable<SvgPathSegmentQBezier> segments)
        {
            var command = new SvgPathCommandQBezier { IsRelative = isRelative };

            command.Segments.AddSegments(segments);

            return command;
        }

        public static SvgPathCommandQBezier Create(bool isRelative, SvgValueLengthUnit unit, IEnumerable<SvgPathSegmentQBezier> segments)
        {
            var command = new SvgPathCommandQBezier { IsRelative = isRelative };

            command.Segments.Unit = unit;
            command.Segments.AddSegments(segments);

            return command;
        }

        public static SvgPathCommandQBezier Create(params SvgPathSegmentQBezier[] segments)
        {
            var command = new SvgPathCommandQBezier();

            command.Segments.AddSegments(segments);

            return command;
        }

        public static SvgPathCommandQBezier Create(bool isRelative, params SvgPathSegmentQBezier[] segments)
        {
            var command = new SvgPathCommandQBezier { IsRelative = isRelative };

            command.Segments.AddSegments(segments);

            return command;
        }

        public static SvgPathCommandQBezier Create(bool isRelative, SvgValueLengthUnit unit, params SvgPathSegmentQBezier[] segments)
        {
            var command = new SvgPathCommandQBezier { IsRelative = isRelative };

            command.Segments.Unit = unit;
            command.Segments.AddSegments(segments);

            return command;
        }



        public override char CommandSymbol => IsRelative ? 'q' : 'Q';


        private SvgPathCommandQBezier()
        {
        }
    }
}
