using GeometricAlgebraFulcrumLib.Algebra.Scalars;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Context.Processors;

public interface ISymbolicScalarProcessor<T> :
    IScalarProcessor<T>
{
    T Simplify(T scalar);

    T GetSymbol(string symbolNameText);

    T MetaExpressionToScalar(IMetaExpression expression);

    IMetaExpression ScalarToMetaExpression(MetaContext context, T scalar);
}