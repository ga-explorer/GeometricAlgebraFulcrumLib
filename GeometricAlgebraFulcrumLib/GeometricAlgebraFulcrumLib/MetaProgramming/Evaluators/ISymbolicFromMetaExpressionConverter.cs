using DataStructuresLib;
using GeometricAlgebraFulcrumLib.MetaProgramming.Expressions;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Evaluators;

public interface ISymbolicFromMetaExpressionConverter<out T> :
    IDynamicTreeVisitor<IMetaExpression, T>
{

}