using DataStructuresLib;
using GeometricAlgebraFulcrumLib.Algebra.SymbolicAlgebra;

namespace GeometricAlgebraFulcrumLib.Processors.SymbolicAlgebra.Evaluators
{
    public interface IIntoSymbolicExpressionConverter<in T> :
        IDynamicTreeVisitor<T, ISymbolicExpression> where T : class
    {

    }
}