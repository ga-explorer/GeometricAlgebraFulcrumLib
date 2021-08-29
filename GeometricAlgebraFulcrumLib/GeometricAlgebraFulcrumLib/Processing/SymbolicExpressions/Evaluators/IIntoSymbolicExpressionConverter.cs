using DataStructuresLib;

namespace GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Evaluators
{
    public interface IIntoSymbolicExpressionConverter<in T> :
        IDynamicTreeVisitor<T, ISymbolicExpression> where T : class
    {

    }
}