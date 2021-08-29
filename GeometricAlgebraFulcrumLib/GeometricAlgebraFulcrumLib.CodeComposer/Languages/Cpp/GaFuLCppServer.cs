using CodeComposerLib.Irony.Semantic.Type;
using CodeComposerLib.Languages.Cpp;

namespace GeometricAlgebraFulcrumLib.CodeComposer.Languages.Cpp
{
    public sealed class GaFuLCppServer : 
        GaFuLLanguageServerBase
    {
        public override string DefaultFileExtension => "cpp";


        internal GaFuLCppServer(CclCppCodeGenerator codeComposer, CclCppSyntaxFactory syntaxFactory)
            : base(codeComposer, syntaxFactory, GaFuLCppExpressionConverter.DefaultConverter)
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