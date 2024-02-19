using DataStructuresLib;
using GeometricAlgebraFulcrumLib.MetaProgramming.Expressions;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Evaluators;

public interface ISymbolicIntoMetaExpressionConverter<in T> :
    IDynamicTreeVisitor<T, IMetaExpression> where T : class
{

}