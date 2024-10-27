using GeometricAlgebraFulcrumLib.Utilities.Code.Irony.Semantic.Type;
using GeometricAlgebraFulcrumLib.Utilities.Code.Languages.Matlab;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Utilities.Code.Matlab;

// ReSharper disable once InconsistentNaming
public sealed class GaFuLMatlabServer :
    GaFuLLanguageServerBase
{
    public override string DefaultFileExtension
        => "cs";


    internal GaFuLMatlabServer(CclMatlabCodeGenerator codeComposer, CclMatlabSyntaxFactory syntaxFactory)
        : base(codeComposer, syntaxFactory, MetaExpressionToMatlabFloat64Converter.DefaultConverter)
    {
    }

    internal GaFuLMatlabServer(CclMatlabCodeGenerator codeComposer, CclMatlabSyntaxFactory syntaxFactory, MetaExpressionToLanguageConverterBase expressionToLanguageConverter)
        : base(codeComposer, syntaxFactory, expressionToLanguageConverter)
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