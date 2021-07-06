using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Context;

namespace GeometricAlgebraFulcrumLib.Processing.Scalars
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