using GeometricAlgebraFulcrumLib.Utilities.Code.Languages;
using GeometricAlgebraFulcrumLib.Utilities.Code.SyntaxTree.Expressions;
using Microsoft.CSharp.RuntimeBinder;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions.Composite;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions.Numbers;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions.Variables;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Utilities.Code;

public abstract class MetaExpressionToLanguageConverterBase :
    CclExpressionToLanguageConverterBase<IMetaExpression>
{
    protected MetaExpressionToLanguageConverterBase(CclLanguageInfo targetLanguageInfo)
        : base(targetLanguageInfo)
    {

    }


    public override SteExpression Fallback(IMetaExpression item, RuntimeBinderException excException)
    {
        return item.ToSimpleTextExpression();
    }

    public SteExpression Visit(IMetaExpressionVariable variableExpr)
    {
        return SteExpression.CreateVariable(variableExpr.ExternalName);
    }

    public abstract SteExpression Visit(IMetaExpressionNumber numberExpr);

    public abstract SteExpression Visit(IMetaExpressionFunction functionExpr);
}