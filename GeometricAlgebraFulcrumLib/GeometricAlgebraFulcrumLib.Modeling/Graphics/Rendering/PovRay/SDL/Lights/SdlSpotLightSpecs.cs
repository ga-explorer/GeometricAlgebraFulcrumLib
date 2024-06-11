using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.SDL.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.SDL.Lights;

public enum SdlSpotLightShape
{
    Conic = 0, Cylindrical = 1
}

public sealed class SdlSpotLightSpecs
{
    public SdlSpotLightShape Shape { get; set; }

    public ISdlScalarValue Radius { get; set; }

    public ISdlScalarValue FallOff { get; set; }

    public ISdlScalarValue Tightness { get; set; }

    public ISdlVectorValue PointAt { get; set; }

}