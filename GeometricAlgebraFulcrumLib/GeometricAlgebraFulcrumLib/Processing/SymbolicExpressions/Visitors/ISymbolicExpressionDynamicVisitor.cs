using DataStructuresLib;

namespace GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Visitors
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