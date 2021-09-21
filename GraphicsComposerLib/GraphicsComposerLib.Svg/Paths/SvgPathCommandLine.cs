using System.Collections.Generic;
using GraphicsComposerLib.Svg.Paths.Segments;
using GraphicsComposerLib.Svg.Values;

namespace GraphicsComposerLib.Svg.Paths
{
    public sealed class SvgPathCommandLine : SvgSegmentsPathCommand<SvgPathSegmentLine>
    {
        public static SvgPathCommandLine Create()
        {
            return new SvgPathCommandLine();
        }

        public static SvgPathCommandLine Create(bool isRelative)
        {
            return new SvgPathCommandLine() { IsRelative = isRelative };
        }

        public static SvgPathCommandLine Create(bool isRelative, SvgValueLengthUnit unit)
        {
            var command = new SvgPathCommandLine { IsRelative = isRelative };

            command.Segments.Unit = unit;

            return command;
        }

        public static SvgPathCommandLine Create(SvgPathSegmentLine segment)
        {
            var command = new SvgPathCommandLine();

            command.Segments.AddSegment(segment);

            return command;
        }

        public static SvgPathCommandLine Create(bool isRelative, SvgPathSegmentLine segment)
        {
            var command = new SvgPathCommandLine { IsRelative = isRelative };

            command.Segments.AddSegment(segment);

            return command;
        }

        public static SvgPathCommandLine Create(bool isRelative, SvgValueLengthUnit unit, SvgPathSegmentLine segment)
        {
            var command = new SvgPathCommandLine { IsRelative = isRelative };

            command.Segments.Unit = unit;
            command.Segments.AddSegment(segment);

            return command;
        }

        public static SvgPathCommandLine Create(IEnumerable<SvgPathSegmentLine> segments)
        {
            var command = new SvgPathCommandLine();

            command.Segments.AddSegments(segments);

            return command;
        }

        public static SvgPathCommandLine Create(bool isRelative, IEnumerable<SvgPathSegmentLine> segments)
        {
            var command = new SvgPathCommandLine { IsRelative = isRelative };

            command.Segments.AddSegments(segments);

            return command;
        }

        public static SvgPathCommandLine Create(bool isRelative, SvgValueLengthUnit unit, IEnumerable<SvgPathSegmentLine> segments)
        {
            var command = new SvgPathCommandLine { IsRelative = isRelative };

            command.Segments.Unit = unit;
            command.Segments.AddSegments(segments);

            return command;
        }

        public static SvgPathCommandLine Create(params SvgPathSegmentLine[] segments)
        {
            var command = new SvgPathCommandLine();

            command.Segments.AddSegments(segments);

            return command;
        }

        public static SvgPathCommandLine Create(bool isRelative, params SvgPathSegmentLine[] segments)
        {
            var command = new SvgPathCommandLine { IsRelative = isRelative };

            command.Segments.AddSegments(segments);

            return command;
        }

        public static SvgPathCommandLine Create(bool isRelative, SvgValueLengthUnit unit, params SvgPathSegmentLine[] segments)
        {
            var command = new SvgPathCommandLine { IsRelative = isRelative };

            command.Segments.Unit = unit;
            command.Segments.AddSegments(segments);

            return command;
        }


        public override char CommandSymbol => IsRelative ? 'l' : 'L';


        private SvgPathCommandLine()
        {
        }
    }
}