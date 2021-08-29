using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Context;

namespace GeometricAlgebraFulcrumLib.Processing.Scalars
{
    public interface ISymbolicScalarProcessor<T> :
        IScalarProcessor<T>
    {
        T Simplify(T scalar);

        T GetSymbol(string symbolNameText);

        T SymbolicExpressionToScalar(ISymbolicExpression expression);

        ISymbolicExpression ScalarToSymbolicExpression(SymbolicContext context, T scalar);
    }
}