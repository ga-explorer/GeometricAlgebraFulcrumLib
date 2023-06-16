using WebComposerLib.Svg.Values;

namespace WebComposerLib.Svg.Paths.Segments
{
    public interface ISvgPathSegment
    {
        string SegmentText(SvgValueLengthUnit unit);
    }
}