

using DataStructuresLib;

namespace GraphicsComposerLib.PovRay.SDL
{
    public interface ISdlDynamicVisitor : IDynamicTreeVisitor<ISdlElement>
    {
    }

    public interface ISdlDynamicVisitor<out TReturnValue> : IDynamicTreeVisitor<ISdlElement, TReturnValue>
    {
    }
}
