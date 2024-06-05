using GeometricAlgebraFulcrumLib.Utilities.Code.Irony.Semantic.Type;
using GeometricAlgebraFulcrumLib.Utilities.Code.Languages.Excel;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Utilities.Code.Excel;

public sealed class GaFuLExcelServer :
    GaFuLLanguageServerBase
{
    public override string DefaultFileExtension => "xlsx";


    internal GaFuLExcelServer(CclExcelCodeGenerator codeComposer, CclExcelSyntaxFactory syntaxFactory)
        : base(codeComposer, syntaxFactory, MetaExpressionToExcelConverter.DefaultConverter)
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