using GeometricAlgebraFulcrumLib.Utilities.Web.Svg.Elements;

namespace GeometricAlgebraFulcrumLib.Utilities.Web.Svg.Compositions;

public interface ISvgGeometryComposerStyler
{
    SvgElement ComposedElement { get; }

    ISvgGeometryComposerIDs ComposedElementsIDs { get; }

    SvgElement ApplyStyles();
}