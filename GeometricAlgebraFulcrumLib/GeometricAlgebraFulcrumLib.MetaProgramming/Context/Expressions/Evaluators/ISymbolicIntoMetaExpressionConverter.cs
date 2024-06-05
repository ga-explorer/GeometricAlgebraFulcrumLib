using GeometricAlgebraFulcrumLib.Utilities.Structures;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions.Evaluators;

public interface ISymbolicIntoMetaExpressionConverter<in T> :
    IDynamicTreeVisitor<T, IMetaExpression> where T : class
{
    MetaContext Context { get; }
}