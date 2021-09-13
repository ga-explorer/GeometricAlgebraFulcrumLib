using DataStructuresLib;
using GeometricAlgebraFulcrumLib.Algebra.SymbolicAlgebra;

namespace GeometricAlgebraFulcrumLib.Processors.SymbolicAlgebra.Visitors
{
    public interface ISymbolicExpressionDynamicVisitor : 
        IDynamicTreeVisitor<ISymbolicExpression>
    {
    }

    public interface ISymbolicExpressionDynamicVisitor<out TReturnValue> : 
        IDynamicTreeVisitor<ISymbolicExpression, TReturnValue>
    {
    }
}