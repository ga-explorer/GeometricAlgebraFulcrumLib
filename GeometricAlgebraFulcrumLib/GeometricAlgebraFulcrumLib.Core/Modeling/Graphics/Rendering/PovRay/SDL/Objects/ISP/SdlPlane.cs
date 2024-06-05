using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.PovRay.SDL.Values;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.PovRay.SDL.Objects.ISP;

public class SdlPlane : SdlObject, ISdlIspObject
{
    public ISdlVectorValue Normal { get; set; }

    public ISdlScalarValue Distance { get; set; }
}