using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.PovRay.SDL.Values;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.PovRay.SDL.Objects.FSP;

public class SdlCone : SdlObject, ISdlFspObject
{
    public ISdlVectorValue BasePoint { get; set; }

    public ISdlVectorValue CapPoint { get; set; }

    public ISdlScalarValue BaseRadius { get; set; }

    public ISdlScalarValue CapRadius { get; set; }

    public bool Open { get; set; }

}