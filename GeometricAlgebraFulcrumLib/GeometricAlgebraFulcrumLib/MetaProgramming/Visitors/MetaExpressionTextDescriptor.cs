using GeometricAlgebraFulcrumLib.MetaProgramming.Expressions;
using Microsoft.CSharp.RuntimeBinder;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Visitors;

public sealed class MetaExpressionTextDescriptor :
    IMetaExpressionDynamicVisitor<string>
{
    public static MetaExpressionTextDescriptor DefaultDescriptor { get; }
        = new MetaExpressionTextDescriptor();


    public bool UseExceptions 
        => true;

    public bool IgnoreNullElements 
        => false;


    private MetaExpressionTextDescriptor()
    {
    }


    public string Fallback(IMetaExpression item, RuntimeBinderException excException)
    {
        throw new System.NotImplementedException();
    }


    //public string Visit(ISymbolicNumber expr)
    //{
    //    return expr.NumberHeadSpecs.IsLiteral
    //        ? expr.NumberText
    //        : ;
    //}
}