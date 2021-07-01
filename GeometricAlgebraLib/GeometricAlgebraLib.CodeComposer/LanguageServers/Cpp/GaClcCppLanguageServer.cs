using CodeComposerLib.Irony.Semantic.Type;
using CodeComposerLib.Languages.Cpp;

namespace GeometricAlgebraLib.CodeComposer.LanguageServers.Cpp
{
    public sealed class GaClcCppLanguageServer : 
        GaClcLanguageServer
    {
        public override string DefaultFileExtension => "cpp";

        internal GaClcCppLanguageServer(CppCodeComposer codeComposer, CppSyntaxFactory syntaxFactory, GaClcLanguageExpressionConverter expressionConverter)
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