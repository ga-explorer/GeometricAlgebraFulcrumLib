using GeometricAlgebraLib.Processing.SymbolicExpressions;
using GeometricAlgebraLib.Processing.SymbolicExpressions.Context;

namespace GeometricAlgebraLib.Processing.Scalars
{
    public interface IGaSymbolicScalarProcessor<TScalar> :
        IGaScalarProcessor<TScalar>
    {
        TScalar Simplify(TScalar scalar);

        TScalar GetSymbol(string symbolNameText);

        TScalar SymbolicExpressionToScalar(ISymbolicExpression expression);

        ISymbolicExpression ScalarToSymbolicExpression(SymbolicContext context, TScalar scalar);
    }
}