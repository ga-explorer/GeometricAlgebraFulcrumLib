using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.PovRay.SDL.Values;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.PovRay.SDL.Objects.FSP;

public class SdlSuperquadricEllipsoid : SdlObject, ISdlFspObject
{
    public ISdlScalarValue EastWestExponent { get; set; }

    public ISdlScalarValue NorthSouthExponent { get; set; }
}