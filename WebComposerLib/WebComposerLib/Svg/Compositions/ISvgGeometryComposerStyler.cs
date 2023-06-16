using WebComposerLib.Svg.Elements;

namespace WebComposerLib.Svg.Compositions
{
    public interface ISvgGeometryComposerStyler
    {
        SvgElement ComposedElement { get; }

        ISvgGeometryComposerIDs ComposedElementsIDs { get; }

        SvgElement ApplyStyles();
    }
}