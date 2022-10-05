using GraphicsComposerLib.Rendering.Svg.Elements;

namespace GraphicsComposerLib.Rendering.Svg.Compositions
{
    public interface ISvgGeometryComposerStyler
    {
        SvgElement ComposedElement { get; }

        ISvgGeometryComposerIDs ComposedElementsIDs { get; }

        SvgElement ApplyStyles();
    }
}