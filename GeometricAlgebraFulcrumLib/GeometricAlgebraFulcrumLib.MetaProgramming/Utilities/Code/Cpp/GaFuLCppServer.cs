using GeometricAlgebraFulcrumLib.Utilities.Code.Irony.Semantic.Type;
using GeometricAlgebraFulcrumLib.Utilities.Code.Languages.Cpp;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Utilities.Code.Cpp;

public sealed class GaFuLCppServer :
    GaFuLLanguageServerBase
{
    public override string DefaultFileExtension => "cpp";


    internal GaFuLCppServer(CclCppCodeGenerator codeComposer, CclCppSyntaxFactory syntaxFactory)
        : base(codeComposer, syntaxFactory, MetaExpressionToCppConverter.DefaultConverter)
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