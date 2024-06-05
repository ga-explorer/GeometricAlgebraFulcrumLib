using GeometricAlgebraFulcrumLib.Utilities.Structures;

namespace GeometricAlgebraFulcrumLib.Utilities.Code.Irony.Semantic;

public interface IAstNodeDynamicVisitor : IDynamicTreeVisitor<IIronyAstObject>
{
}

public interface IAstNodeDynamicVisitor<out TReturnValue> : IDynamicTreeVisitor<IIronyAstObject, TReturnValue>
{
}