using CodeComposerLib.Irony.Semantic.Type;
using CodeComposerLib.Languages;
using CodeComposerLib.Languages.Cpp;
using CodeComposerLib.Languages.CSharp;
using CodeComposerLib.Languages.Excel;
using GeometricAlgebraLib.CodeComposer.LanguageServers.Cpp;
using GeometricAlgebraLib.CodeComposer.LanguageServers.CSharp;
using GeometricAlgebraLib.CodeComposer.LanguageServers.Excel;
using GeometricAlgebraLib.Symbolic.Mathematica;
using Wolfram.NETLink;

namespace GeometricAlgebraLib.CodeComposer.LanguageServers
{
    /// <summary>
    /// This class can be used to generate syntactically correct text code for some target language like
    /// comments, assignments, variable declarations, expressions, etc.
    /// </summary>
    public abstract class GaClcLanguageServer : 
        LanguageServer
    {
        public static GaClcCSharpLanguageServer CSharp4(GaClcLanguageExpressionConverter expressionConverter)
        {
            return new GaClcCSharpLanguageServer(
                CSharpUtils.CSharp4CodeGenerator(), 
                CSharpUtils.CSharp4SyntaxFactory(),
                expressionConverter
            );
        }

        public static GaClcExcelLanguageServer Excel2007(GaClcLanguageExpressionConverter expressionConverter)
        {
            return new GaClcExcelLanguageServer(
                ExcelUtils.ExcelCodeGenerator(),
                ExcelUtils.ExcelSyntaxFactory(),
                expressionConverter
            );
        }

        public static GaClcCppLanguageServer Cpp11(GaClcLanguageExpressionConverter expressionConverter)
        {
            return new GaClcCppLanguageServer(
                CppUtils.Cpp11CodeGenerator(), 
                CppUtils.Cpp11SyntaxFactory(),
                expressionConverter
            );
        }
        

        /// <summary>
        /// This can be used for converting symbolic expressions into target language expressions
        /// </summary>
        public GaClcLanguageExpressionConverter ExpressionConverter { get; }


        protected GaClcLanguageServer(LanguageCodeGenerator codeGenerator, LanguageSyntaxFactory syntaxFactory, GaClcLanguageExpressionConverter expressionConverter)
            : base(codeGenerator, syntaxFactory)
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
