using CodeComposerLib.Irony.Semantic.Type;
using CodeComposerLib.Languages;
using CodeComposerLib.Languages.Cpp;
using CodeComposerLib.Languages.CSharp;
using CodeComposerLib.Languages.Excel;
using GeometricAlgebraFulcrumLib.CodeComposer.Languages.Cpp;
using GeometricAlgebraFulcrumLib.CodeComposer.Languages.CSharp;
using GeometricAlgebraFulcrumLib.CodeComposer.Languages.Excel;
using GeometricAlgebraFulcrumLib.Symbolic.Mathematica;
using Wolfram.NETLink;

namespace GeometricAlgebraFulcrumLib.CodeComposer.Languages
{
    /// <summary>
    /// This class can be used to generate syntactically correct text code for some target language like
    /// comments, assignments, variable declarations, expressions, etc.
    /// </summary>
    public abstract class GaLanguageServer : 
        CclLanguageServerBase
    {
        public static GaCSharpLanguageServer CSharp(GaLanguageExpressionConverter expressionConverter)
        {
            return new GaCSharpLanguageServer(
                CclCSharpUtils.CSharp4CodeComposer(), 
                CclCSharpUtils.CSharp4SyntaxFactory(),
                expressionConverter
            );
        }

        public static GaCSharpLanguageServer CSharpWithMathematica()
        {
            return new GaCSharpLanguageServer(
                CclCSharpUtils.CSharp4CodeComposer(), 
                CclCSharpUtils.CSharp4SyntaxFactory(),
                new GaMathematicaExprToCSharpConverter()
            );
        }

        public static GaExcelLanguageServer Excel2007(GaLanguageExpressionConverter expressionConverter)
        {
            return new GaExcelLanguageServer(
                CclExcelUtils.ExcelCodeComposer(),
                CclExcelUtils.ExcelSyntaxFactory(),
                expressionConverter
            );
        }

        public static GaCppLanguageServer Cpp11(GaLanguageExpressionConverter expressionConverter)
        {
            return new GaCppLanguageServer(
                CclCppUtils.Cpp11CodeComposer(), 
                CclCppUtils.Cpp11SyntaxFactory(),
                expressionConverter
            );
        }
        

        /// <summary>
        /// This can be used for converting symbolic expressions into target language expressions
        /// </summary>
        public GaLanguageExpressionConverter ExpressionConverter { get; }


        protected GaLanguageServer(CclLanguageCodeGeneratorBase codeComposer, CclLanguageSyntaxFactory syntaxFactory, GaLanguageExpressionConverter expressionConverter)
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
        public virtual string GenerateCode(Expr expr)
        {
            if (ReferenceEquals(ExpressionConverter, null))
                return expr.ToString();

            var symbolicTextExpr = expr.ToSimpleTextExpression();
            
            var targetExprTextTree = ExpressionConverter.Convert(symbolicTextExpr);

            return CodeGenerator.GenerateCode(targetExprTextTree);
        }
    }
}
