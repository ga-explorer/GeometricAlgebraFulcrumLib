using GeometricAlgebraFulcrumLib.Utilities.Structures;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions.Visitors;

public interface IMetaExpressionDynamicVisitor :
    IDynamicTreeVisitor<IMetaExpression>
{
}

public interface IMetaExpressionDynamicVisitor<out TReturnValue> :
    IDynamicTreeVisitor<IMetaExpression, TReturnValue>
{
}