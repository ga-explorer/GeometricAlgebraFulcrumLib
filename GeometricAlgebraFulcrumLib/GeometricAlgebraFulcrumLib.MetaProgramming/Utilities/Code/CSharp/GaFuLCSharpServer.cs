using GeometricAlgebraFulcrumLib.Utilities.Code.Irony.Semantic.Type;
using GeometricAlgebraFulcrumLib.Utilities.Code.Languages.CSharp;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Utilities.Code.CSharp;

// ReSharper disable once InconsistentNaming
public sealed class GaFuLCSharpServer :
    GaFuLLanguageServerBase
{
    public override string DefaultFileExtension
        => "cs";


    internal GaFuLCSharpServer(CclCSharpCodeGenerator codeComposer, CclCSharpSyntaxFactory syntaxFactory)
        : base(codeComposer, syntaxFactory, MetaExpressionToCSharpFloat64Converter.DefaultConverter)
    {
    }

    internal GaFuLCSharpServer(CclCSharpCodeGenerator codeComposer, CclCSharpSyntaxFactory syntaxFactory, MetaExpressionToLanguageConverterBase expressionToLanguageConverter)
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