using GeometricAlgebraFulcrumLib.Utilities.Web.Svg.Elements;

namespace GeometricAlgebraFulcrumLib.Utilities.Web.Svg.Compositions;

public interface ISvgGeometryComposer
{
    ISvgGeometryComposerIDs ElementsIDs { get; }

    ISvgGeometryComposerStyler ElementsStyler { get; }

    SvgElement Compose(bool applyStyles);
}