using CodeComposerLib.Irony.Semantic.Type;
using CodeComposerLib.Languages.Cpp;

namespace GeometricAlgebraFulcrumLib.CodeComposer.Languages.Cpp
{
    public sealed class GaCppLanguageServer : 
        GaLanguageServer
    {
        public override string DefaultFileExtension => "cpp";

        internal GaCppLanguageServer(CclCppCodeGenerator codeComposer, CclCppSyntaxFactory syntaxFactory, GaLanguageExpressionConverter expressionConverter)
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