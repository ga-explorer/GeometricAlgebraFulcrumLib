using GeometricAlgebraLib.SymbolicExpressions;
using GeometricAlgebraLib.SymbolicExpressions.Context;

namespace GeometricAlgebraLib.Processors.Scalars
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