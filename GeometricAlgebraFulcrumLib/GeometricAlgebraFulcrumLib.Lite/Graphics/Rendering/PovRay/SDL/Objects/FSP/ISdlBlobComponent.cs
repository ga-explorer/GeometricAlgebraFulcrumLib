using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.PovRay.SDL.Values;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.PovRay.SDL.Objects.FSP
{
    public interface ISdlBlobComponent : ISdlFspObject
    {
        ISdlScalarValue Strength { get; set; }
    }
}