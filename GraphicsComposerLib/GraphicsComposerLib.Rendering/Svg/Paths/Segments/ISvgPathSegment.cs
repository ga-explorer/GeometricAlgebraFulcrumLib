using GraphicsComposerLib.Rendering.Svg.Values;

namespace GraphicsComposerLib.Rendering.Svg.Paths.Segments
{
    public interface ISvgPathSegment
    {
        string SegmentText(SvgValueLengthUnit unit);
    }
}