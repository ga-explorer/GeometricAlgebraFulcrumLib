using CodeComposerLib.Irony.Semantic.Type;
using CodeComposerLib.Languages;
using CodeComposerLib.Languages.Cpp;
using CodeComposerLib.Languages.CSharp;
using CodeComposerLib.Languages.Excel;
using GeometricAlgebraFulcrumLib.CodeComposer.Languages.Cpp;
using GeometricAlgebraFulcrumLib.CodeComposer.Languages.CSharp;
using GeometricAlgebraFulcrumLib.CodeComposer.Languages.Excel;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions;

namespace GeometricAlgebraFulcrumLib.CodeComposer.Languages
{
    /// <summary>
    /// This class can be used to generate syntactically correct text code for some target language like
    /// comments, assignments, variable declarations, expressions, etc.
    /// </summary>
    public abstract class GaFuLLanguageServerBase : 
        CclLanguageServerBase
    {
        public static GaFuLCSharpServer CSharp()
        {
            return new GaFuLCSharpServer(
                CclCSharpUtils.CSharp4CodeComposer(), 
                CclCSharpUtils.CSharp4SyntaxFactory()
            );
        }
        
        public static GaFuLExcelServer Excel2007()
        {
            return new GaFuLExcelServer(
                CclExcelUtils.ExcelCodeComposer(),
                CclExcelUtils.ExcelSyntaxFactory()
            );
        }

        public static GaFuLCppServer Cpp11()
        {
            return new GaFuLCppServer(
                CclCppUtils.Cpp11CodeComposer(), 
                CclCppUtils.Cpp11SyntaxFactory()
            );
        }
        

        /// <summary>
        /// This can be used for converting symbolic expressions into target language expressions
        /// </summary>
        public GaFuLLanguageExpressionConverterBase ExpressionConverter { get; }


        protected GaFuLLanguageServerBase(CclLanguageCodeGeneratorBase codeComposer, CclLanguageSyntaxFactory syntaxFactory, GaFuLLanguageExpressionConverterBase expressionConverter)
            : base(codeComposer, syntaxFactory)
        {
            ExpressionConverter = expressionConverter;
        }


        /// <summary>
        /// Gets the target language type name equivalent to the given GaClcDSL primitive type
        /// </summary>
        /// <param name="itemType"></param>
        /// <returns></returns>
        public abstract string TargetTypeName(TypePrimitive itemType);

        /// <summary>
        /// Convert the given symbolic expression object into target language expression using the
        /// internal ExpressionConverter object if possible
        /// </summary>
        /// <param name="expr"></param>
        /// <returns></returns>
        public virtual string GenerateCode(ISymbolicExpression expr)
        {
            if (ReferenceEquals(ExpressionConverter, null))
                return expr.ToString();

            var targetExprTextTree = ExpressionConverter.Convert(expr);

            return CodeGenerator.GenerateCode(targetExprTextTree);
        }
    }
}
