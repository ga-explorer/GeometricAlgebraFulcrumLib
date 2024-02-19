using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.PovRay.SDL.Transforms;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.PovRay.SDL;

public interface ISdlTransformable : ISdlElement
{
    List<ISdlTransform> Transforms { get; }
}