using CodeComposerLib.Irony.Semantic.Type;
using CodeComposerLib.Languages.Excel;

namespace GeometricAlgebraLib.CodeComposer.LanguageServers.Excel
{
    public sealed class GaClcExcelLanguageServer : 
        GaClcLanguageServer
    {
        public override string DefaultFileExtension => "xlsx";

        internal GaClcExcelLanguageServer(ExcelCodeGenerator codeGenerator, ExcelSyntaxFactory syntaxFactory, GaClcLanguageExpressionConverter expressionConverter)
            : base(codeGenerator, syntaxFactory, expressionConverter)
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