using GraphicsComposerLib.Svg.Elements;

namespace GraphicsComposerLib.Svg.Compositions
{
    public interface ISvgGeometryComposer
    {
        ISvgGeometryComposerIDs ElementsIDs { get; }

        ISvgGeometryComposerStyler ElementsStyler { get; }

        SvgElement Compose(bool applyStyles);
    }
}