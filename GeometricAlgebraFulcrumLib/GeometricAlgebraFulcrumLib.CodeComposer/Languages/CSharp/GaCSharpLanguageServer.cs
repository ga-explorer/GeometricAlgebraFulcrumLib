using CodeComposerLib.Irony.Semantic.Type;
using CodeComposerLib.Languages.CSharp;

namespace GeometricAlgebraFulcrumLib.CodeComposer.Languages.CSharp
{
    // ReSharper disable once InconsistentNaming
    public sealed class GaCSharpLanguageServer : 
        GaLanguageServer
    {
        public override string DefaultFileExtension 
            => "cs";


        internal GaCSharpLanguageServer(CclCSharpCodeGenerator codeComposer, CclCSharpSyntaxFactory syntaxFactory, GaLanguageExpressionConverter expressionConverter)
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
