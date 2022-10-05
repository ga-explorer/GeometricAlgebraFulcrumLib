using CodeComposerLib.Languages;
using CodeComposerLib.SyntaxTree.Expressions;
using GeometricAlgebraFulcrumLib.MetaProgramming.Expressions;
using GeometricAlgebraFulcrumLib.MetaProgramming.Expressions.Composite;
using GeometricAlgebraFulcrumLib.MetaProgramming.Expressions.Numbers;
using GeometricAlgebraFulcrumLib.MetaProgramming.Expressions.Variables;
using Microsoft.CSharp.RuntimeBinder;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Languages
{
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
}