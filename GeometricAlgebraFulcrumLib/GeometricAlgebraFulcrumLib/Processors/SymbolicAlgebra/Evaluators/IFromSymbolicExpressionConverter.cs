using DataStructuresLib;
using GeometricAlgebraFulcrumLib.Algebra.SymbolicAlgebra;

namespace GeometricAlgebraFulcrumLib.Processors.SymbolicAlgebra.Evaluators
{
    public interface IFromSymbolicExpressionConverter<out T> :
        IDynamicTreeVisitor<ISymbolicExpression, T>
    {

    }
}