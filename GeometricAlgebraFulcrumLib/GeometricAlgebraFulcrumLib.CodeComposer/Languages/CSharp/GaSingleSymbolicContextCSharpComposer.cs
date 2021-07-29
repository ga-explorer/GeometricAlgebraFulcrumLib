using GeometricAlgebraFulcrumLib.CodeComposer.Composers;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Context;

namespace GeometricAlgebraFulcrumLib.CodeComposer.Languages.CSharp
{
    public sealed class GaSingleSymbolicContextCSharpComposer : 
        GaSingleSymbolicContextCodeComposerBase
    {
        public GaSingleSymbolicContextCSharpComposer(SymbolicContext context, GaLanguageExpressionConverter expressionConverter)
            : base(context, GaLanguageServer.CSharp(expressionConverter))
        {
        }


        //protected override void GenerateTypeDefaultValue(AstType typeInfo)
        //{
        //    if (typeInfo.IsValidIntegerType)
        //        ActiveFileTextComposer.Append("0");

        //    else if (typeInfo.IsValidBooleanType)
        //        ActiveFileTextComposer.Append("false");

        //    else if (typeInfo.IsValidScalarType)
        //        ActiveFileTextComposer.Append("0.0D");

        //    else
        //        ActiveFileTextComposer.Append("new " + typeInfo.GaClcTypeSignature + "()");
        //}

        protected override void GenerateSymbolicContextCode()
        {
            ActiveFileTextComposer.AppendAtNewLine("public static ");
            GenerateTypeName();
            ActiveFileTextComposer.Append(" ");
            ActiveFileTextComposer.Append(Context.ContextOptions.ContextName);
            ActiveFileTextComposer.Append("(");

            GenerateInputsCode(Context);

            ActiveFileTextComposer.AppendLine(")");

            ActiveFileTextComposer.AppendLineAtNewLine("{");
            ActiveFileTextComposer.IncreaseIndentation();

            ActiveFileTextComposer.AppendAtNewLine(GaLanguage.ScalarTypeName);
            ActiveFileTextComposer.AppendLine(" result;");

            //if (macroBinding.BaseSymbolicContext.OutputType.IsValidScalarType)
            //{
            //    ActiveFileTextComposer.AppendAtNewLine(GaClcLanguage.ScalarTypeName);
            //    ActiveFileTextComposer.AppendLine(" result;");
            //}
            //else
            //{
            //    ActiveFileTextComposer.AppendAtNewLine("var result = ");

            //    GenerateTypeDefaultValue(macroBinding.BaseSymbolicContext.OutputType);

            //    ActiveFileTextComposer.AppendLine(";");
            //}

            ActiveFileTextComposer.AppendLine();

            if (AllowGenerateSymbolicContextCode)
            {
                var contextComposer = InitSymbolicContextCodeComposer();

                ActiveFileTextComposer.AppendLineAtNewLine(
                    contextComposer.Generate()
                );
            }

            ActiveFileTextComposer.AppendLineAtNewLine("return result;");

            ActiveFileTextComposer.DecreaseIndentation();
            ActiveFileTextComposer.AppendLineAtNewLine("}");
            ActiveFileTextComposer.AppendLine();
        }


        public override GaCodeLibraryComposerBase CreateEmptyComposer()
        {
            return new GaSingleSymbolicContextCSharpComposer(
                Context,
                GaLanguage.ExpressionConverter
            );
        }
    }
}
