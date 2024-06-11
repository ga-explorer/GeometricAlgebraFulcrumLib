using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.SDL.Transforms;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.SDL;

public interface ISdlTransformable : ISdlElement
{
    List<ISdlTransform> Transforms { get; }
}