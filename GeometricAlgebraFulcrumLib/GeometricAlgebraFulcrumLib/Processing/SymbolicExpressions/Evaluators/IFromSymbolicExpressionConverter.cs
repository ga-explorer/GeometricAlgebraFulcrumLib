using DataStructuresLib;

namespace GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Evaluators
{
    public interface IFromSymbolicExpressionConverter<out T> :
        IDynamicTreeVisitor<ISymbolicExpression, T>
    {

    }
}