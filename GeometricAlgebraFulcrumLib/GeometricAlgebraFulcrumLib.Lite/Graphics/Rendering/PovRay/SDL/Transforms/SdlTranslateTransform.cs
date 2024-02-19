using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.PovRay.SDL.Values;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.PovRay.SDL.Transforms;

public sealed class SdlTranslateTransform : SdlTransform
{
    public ISdlVectorValue Direction { get; private set; }


    internal SdlTranslateTransform(ISdlVectorValue direction)
    {
        Direction = direction;
    }
}