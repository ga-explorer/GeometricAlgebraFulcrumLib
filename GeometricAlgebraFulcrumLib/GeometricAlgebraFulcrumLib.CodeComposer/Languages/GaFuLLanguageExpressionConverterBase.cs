using CodeComposerLib.Languages;
using CodeComposerLib.SyntaxTree.Expressions;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Composite;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Numbers;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Variables;
using Microsoft.CSharp.RuntimeBinder;

namespace GeometricAlgebraFulcrumLib.CodeComposer.Languages
{
    public abstract class GaFuLLanguageExpressionConverterBase : 
        CclLanguageExpressionConverterBase<ISymbolicExpression>
    {
        protected GaFuLLanguageExpressionConverterBase(CclLanguageInfo targetLanguageInfo)
            : base(targetLanguageInfo)
        {
            
        }


        public override SteExpression Fallback(ISymbolicExpression item, RuntimeBinderException excException)
        {
            return item.ToSimpleTextExpression();
        }

        public SteExpression Visit(ISymbolicVariable variableExpr)
        {
            return SteExpression.CreateVariable(variableExpr.ExternalName);
        }

        public abstract SteExpression Visit(ISymbolicNumber numberExpr);

        public abstract SteExpression Visit(ISymbolicFunction functionExpr);
    }
}