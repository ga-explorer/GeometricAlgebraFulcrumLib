using GeometricAlgebraFulcrumLib.Utilities.Structures;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions.Evaluators;

public interface ISymbolicFromMetaExpressionConverter<out T> :
    IDynamicTreeVisitor<IMetaExpression, T>
{

}