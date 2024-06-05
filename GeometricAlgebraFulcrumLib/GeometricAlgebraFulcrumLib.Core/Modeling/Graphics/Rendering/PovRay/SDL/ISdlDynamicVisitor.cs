

using GeometricAlgebraFulcrumLib.Utilities.Structures;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.PovRay.SDL;

public interface ISdlDynamicVisitor : IDynamicTreeVisitor<ISdlElement>
{
}

public interface ISdlDynamicVisitor<out TReturnValue> : IDynamicTreeVisitor<ISdlElement, TReturnValue>
{
}