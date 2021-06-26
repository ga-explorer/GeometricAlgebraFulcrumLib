using CodeComposerLib.Irony.Semantic.Type;
using CodeComposerLib.Languages.Cpp;

namespace GeometricAlgebraLib.CodeComposer.LanguageServers.Cpp
{
    public sealed class GaClcCppLanguageServer : 
        GaClcLanguageServer
    {
        public override string DefaultFileExtension => "cpp";

        internal GaClcCppLanguageServer(CppCodeGenerator codeGenerator, CppSyntaxFactory syntaxFactory, GaClcLanguageExpressionConverter expressionConverter)
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