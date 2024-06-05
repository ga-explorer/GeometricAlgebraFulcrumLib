using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.PovRay.SDL.Transforms;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.PovRay.SDL;

public interface ISdlTransformable : ISdlElement
{
    List<ISdlTransform> Transforms { get; }
}