using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context;
using GeometricAlgebraFulcrumLib.MetaProgramming.Expressions;

namespace GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra
{
    public interface ISymbolicScalarProcessor<T> :
        IScalarProcessor<T>
    {
        T Simplify(T scalar);

        T GetSymbol(string symbolNameText);

        T MetaExpressionToScalar(IMetaExpression expression);

        IMetaExpression ScalarToMetaExpression(MetaContext context, T scalar);
    }
}