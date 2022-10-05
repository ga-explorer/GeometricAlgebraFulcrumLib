using GraphicsComposerLib.Rendering.Svg.Elements;

namespace GraphicsComposerLib.Rendering.Svg.Compositions
{
    public interface ISvgGeometryComposer
    {
        ISvgGeometryComposerIDs ElementsIDs { get; }

        ISvgGeometryComposerStyler ElementsStyler { get; }

        SvgElement Compose(bool applyStyles);
    }
}