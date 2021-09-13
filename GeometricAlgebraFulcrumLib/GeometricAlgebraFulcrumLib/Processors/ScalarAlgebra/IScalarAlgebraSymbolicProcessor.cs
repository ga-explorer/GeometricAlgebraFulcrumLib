using GeometricAlgebraFulcrumLib.Algebra.SymbolicAlgebra;
using GeometricAlgebraFulcrumLib.Processors.SymbolicAlgebra.Context;

namespace GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra
{
    public interface IScalarAlgebraSymbolicProcessor<T> :
        IScalarAlgebraProcessor<T>
    {
        T Simplify(T scalar);

        T GetSymbol(string symbolNameText);

        T SymbolicExpressionToScalar(ISymbolicExpression expression);

        ISymbolicExpression ScalarToSymbolicExpression(SymbolicContext context, T scalar);
    }
}