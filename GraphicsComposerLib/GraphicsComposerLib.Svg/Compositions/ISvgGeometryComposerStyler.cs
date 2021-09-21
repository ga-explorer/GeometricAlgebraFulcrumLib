using GraphicsComposerLib.Svg.Elements;

namespace GraphicsComposerLib.Svg.Compositions
{
    public interface ISvgGeometryComposerStyler
    {
        SvgElement ComposedElement { get; }

        ISvgGeometryComposerIDs ComposedElementsIDs { get; }

        SvgElement ApplyStyles();
    }
}