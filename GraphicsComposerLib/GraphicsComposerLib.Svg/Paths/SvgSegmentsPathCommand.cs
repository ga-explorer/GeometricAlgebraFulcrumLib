using System.Text;
using GraphicsComposerLib.Svg.Paths.Segments;

namespace GraphicsComposerLib.Svg.Paths
{
    public abstract class SvgSegmentsPathCommand<T> : SvgPathCommand where T : ISvgPathSegment
    {
        public abstract char CommandSymbol { get; }

        public bool IsRelative { get; set; }

        public bool IsAbsolute
        {
            get { return !IsRelative; }
            set { IsRelative = !value; }
        }

        public SvgPathSegmentsList<T> Segments { get; }
            = SvgPathSegmentsList<T>.Create();

        public override string ValueText
        {
            get
            {
                var composer = new StringBuilder();

                composer
                    .Append(CommandSymbol)
                    .Append(' ')
                    .Append(Segments.ValueText);

                return composer.ToString();
            }
        }
    }
}