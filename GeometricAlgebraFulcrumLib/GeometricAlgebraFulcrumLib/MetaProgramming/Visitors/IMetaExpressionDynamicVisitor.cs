using DataStructuresLib;
using GeometricAlgebraFulcrumLib.MetaProgramming.Expressions;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Visitors
{
    public interface IMetaExpressionDynamicVisitor : 
        IDynamicTreeVisitor<IMetaExpression>
    {
    }

    public interface IMetaExpressionDynamicVisitor<out TReturnValue> : 
        IDynamicTreeVisitor<IMetaExpression, TReturnValue>
    {
    }
}