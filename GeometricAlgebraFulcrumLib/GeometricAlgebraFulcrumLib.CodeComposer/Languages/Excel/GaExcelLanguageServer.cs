using CodeComposerLib.Irony.Semantic.Type;
using CodeComposerLib.Languages.Excel;

namespace GeometricAlgebraFulcrumLib.CodeComposer.Languages.Excel
{
    public sealed class GaExcelLanguageServer : 
        GaLanguageServer
    {
        public override string DefaultFileExtension => "xlsx";

        internal GaExcelLanguageServer(CclExcelCodeGenerator codeComposer, CclExcelSyntaxFactory syntaxFactory, GaLanguageExpressionConverter expressionConverter)
            : base(codeComposer, syntaxFactory, expressionConverter)
        {
        }

        public override string TargetTypeName(TypePrimitive itemType)
        {
            if (itemType.IsBoolean())
                return "bool";

            if (itemType.IsInteger())
                return "int";

            if (itemType.IsScalar())
                return CodeGenerator.ScalarTypeName;

            return @"/*<Unknown type>*/";
        }

    }
}