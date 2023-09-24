

using DataStructuresLib;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.PovRay.SDL
{
    public interface ISdlDynamicVisitor : IDynamicTreeVisitor<ISdlElement>
    {
    }

    public interface ISdlDynamicVisitor<out TReturnValue> : IDynamicTreeVisitor<ISdlElement, TReturnValue>
    {
    }
}
