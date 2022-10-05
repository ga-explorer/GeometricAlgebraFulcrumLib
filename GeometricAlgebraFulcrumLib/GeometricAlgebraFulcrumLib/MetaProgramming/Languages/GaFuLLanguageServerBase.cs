using CodeComposerLib.Irony.Semantic.Type;
using CodeComposerLib.Languages;
using CodeComposerLib.Languages.Cpp;
using CodeComposerLib.Languages.CSharp;
using CodeComposerLib.Languages.Excel;
using GeometricAlgebraFulcrumLib.MetaProgramming.Expressions;
using GeometricAlgebraFulcrumLib.MetaProgramming.Languages.Cpp;
using GeometricAlgebraFulcrumLib.MetaProgramming.Languages.CSharp;
using GeometricAlgebraFulcrumLib.MetaProgramming.Languages.Excel;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Languages
{
    /// <summary>
    /// This class can be used to generate syntactically correct text code for some target language like
    /// comments, assignments, variable declarations, expressions, etc.
    /// </summary>
    public abstract class GaFuLLanguageServerBase : 
        CclLanguageServerBase
    {
        public static GaFuLCSharpServer CSharpScalarProcessor()
        {
            return new GaFuLCSharpServer(
                CclCSharpUtils.CSharp4CodeComposer(), 
                CclCSharpUtils.CSharp4SyntaxFactory(),
                MetaExpressionToCSharpScalarProcessorConverter.DefaultConverter
            );
        }
        
        public static GaFuLCSharpServer CSharpScalarProcessor(string scalarProcessorVariableName)
        {
            return new GaFuLCSharpServer(
                CclCSharpUtils.CSharp4CodeComposer(), 
                CclCSharpUtils.CSharp4SyntaxFactory(),
                MetaExpressionToCSharpScalarProcessorConverter.Create(scalarProcessorVariableName)
            );
        }
        
        public static GaFuLCSharpServer CSharpFloat64()
        {
            return new GaFuLCSharpServer(
                CclCSharpUtils.CSharp4CodeComposer(), 
                CclCSharpUtils.CSharp4SyntaxFactory(),
                MetaExpressionToCSharpFloat64Converter.DefaultConverter
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
        public MetaExpressionToLanguageConverterBase ExpressionConverter { get; }


        protected GaFuLLanguageServerBase(CclLanguageCodeGeneratorBase codeComposer, CclLanguageSyntaxFactory syntaxFactory, MetaExpressionToLanguageConverterBase expressionConverter)
            : base(codeComposer, syntaxFactory)
        {
            ExpressionConverter = expressionConverter;
        }


        /// <summary>
        /// Gets the target language type name equivalent to the given GeoClcDSL primitive type
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
        public virtual string GenerateCode(IMetaExpression expr)
        {
            if (ReferenceEquals(ExpressionConverter, null))
                return expr.ToString();

            var targetExprTextTree = ExpressionConverter.Convert(expr);

            return CodeGenerator.GenerateCode(targetExprTextTree);
        }
    }
}
