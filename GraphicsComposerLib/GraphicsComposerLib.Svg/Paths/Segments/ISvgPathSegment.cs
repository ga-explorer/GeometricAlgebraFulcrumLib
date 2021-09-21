using GraphicsComposerLib.Svg.Values;

namespace GraphicsComposerLib.Svg.Paths.Segments
{
    public interface ISvgPathSegment
    {
        string SegmentText(SvgValueLengthUnit unit);
    }
}