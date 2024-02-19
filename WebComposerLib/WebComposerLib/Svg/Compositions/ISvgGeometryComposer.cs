using WebComposerLib.Svg.Elements;

namespace WebComposerLib.Svg.Compositions;

public interface ISvgGeometryComposer
{
    ISvgGeometryComposerIDs ElementsIDs { get; }

    ISvgGeometryComposerStyler ElementsStyler { get; }

    SvgElement Compose(bool applyStyles);
}