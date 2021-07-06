using CodeComposerLib.Irony.Semantic.Type;
using CodeComposerLib.Languages.Excel;

namespace GeometricAlgebraFulcrumLib.CodeComposer.LanguageServers.Excel
{
    public sealed class GaClcExcelLanguageServer : 
        GaClcLanguageServer
    {
        public override string DefaultFileExtension => "xlsx";

        internal GaClcExcelLanguageServer(ExcelCodeComposer codeComposer, ExcelSyntaxFactory syntaxFactory, GaClcLanguageExpressionConverter expressionConverter)
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
                return CodeComposer.ScalarTypeName;

            return @"/*<Unknown type>*/";
        }

    }
}