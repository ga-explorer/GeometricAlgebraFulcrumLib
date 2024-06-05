using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.PovRay.SDL.Values;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.PovRay.SDL.Objects.FSP;

public class SdlTorus : SdlPolynomialObject, ISdlFspObject
{
    public ISdlScalarValue MajorRadius { get; set; }

    public ISdlScalarValue MinorRadius { get; set; }
}