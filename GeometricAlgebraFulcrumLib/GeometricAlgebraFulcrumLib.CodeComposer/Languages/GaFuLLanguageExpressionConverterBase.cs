using CodeComposerLib.Languages;
using CodeComposerLib.SyntaxTree.Expressions;
using GeometricAlgebraFulcrumLib.Algebra.SymbolicAlgebra;
using GeometricAlgebraFulcrumLib.Algebra.SymbolicAlgebra.Composite;
using GeometricAlgebraFulcrumLib.Algebra.SymbolicAlgebra.Numbers;
using GeometricAlgebraFulcrumLib.Algebra.SymbolicAlgebra.Variables;
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