using CodeComposerLib.Irony.Semantic.Type;
using CodeComposerLib.Languages.CSharp;

namespace GeometricAlgebraLib.CodeComposer.LanguageServers.CSharp
{
    // ReSharper disable once InconsistentNaming
    public sealed class GaClcCSharpLanguageServer : GaClcLanguageServer
    {
        public override string DefaultFileExtension => "cs";

        internal GaClcCSharpLanguageServer(CSharpCodeComposer codeComposer, CSharpSyntaxFactory syntaxFactory, GaClcLanguageExpressionConverter expressionConverter)
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
