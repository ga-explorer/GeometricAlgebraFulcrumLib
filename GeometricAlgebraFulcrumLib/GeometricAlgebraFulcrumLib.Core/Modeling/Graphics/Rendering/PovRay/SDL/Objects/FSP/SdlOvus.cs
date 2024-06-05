using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.PovRay.SDL.Values;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.PovRay.SDL.Objects.FSP;

public class SdlOvus : SdlObject, ISdlFspObject
{
    public ISdlScalarValue BottomRadius { get; set; }

    public ISdlScalarValue TopRadius { get; set; }
}