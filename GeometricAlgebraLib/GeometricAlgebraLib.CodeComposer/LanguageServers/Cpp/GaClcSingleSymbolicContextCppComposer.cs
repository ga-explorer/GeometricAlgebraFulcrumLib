using GeometricAlgebraLib.CodeComposer.Composers;
using GeometricAlgebraLib.SymbolicExpressions.Context;

namespace GeometricAlgebraLib.CodeComposer.LanguageServers.Cpp
{
    public sealed class GaClcSingleSymbolicContextCppComposer : 
        GaClcSingleSymbolicContextCodeComposerBase
    {
        public GaClcSingleSymbolicContextCppComposer(SymbolicContext context, GaClcLanguageExpressionConverter expressionConverter)
            : base(context, GaClcLanguageServer.Cpp11(expressionConverter))
        {
        }


        //protected override void GenerateTypeDefaultValue(AstType typeInfo)
        //{
        //    if (typeInfo.IsValidIntegerType)
        //        ActiveFileTextComposer.Append("0");

        //    else if (typeInfo.IsValidBooleanType)
        //        ActiveFileTextComposer.Append("false");

        //    else if (typeInfo.IsValidScalarType)
        //        ActiveFileTextComposer.Append("0.0");

        //    else
        //        ActiveFileTextComposer.Append("new " + typeInfo.GaClcTypeSignature + "()");
        //}

        protected override void GenerateSymbolicContextCode()
        {
            ActiveFileTextComposer.AppendAtNewLine("public ");
            //GenerateTypeName(macroBinding.BaseSymbolicContext.OutputType);
            GenerateTypeName();
            ActiveFileTextComposer.Append(" ");
            ActiveFileTextComposer.Append(Context.ContextOptions.ContextName);
            ActiveFileTextComposer.Append("(");

            GenerateSymbolicContextInputsCode(Context);

            ActiveFileTextComposer.AppendLine(")");

            ActiveFileTextComposer.AppendLineAtNewLine("{");
            ActiveFileTextComposer.IncreaseIndentation();

            ActiveFileTextComposer.AppendAtNewLine(GaClcLanguage.ScalarTypeName);
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
                var macroGenerator = InitSymbolicContextCodeGenerator();

                ActiveFileTextComposer.AppendLineAtNewLine(
                    macroGenerator.Generate()
                );
            }

            ActiveFileTextComposer.AppendLineAtNewLine("return result;");

            ActiveFileTextComposer.DecreaseIndentation();
            ActiveFileTextComposer.AppendLineAtNewLine("}");
            ActiveFileTextComposer.AppendLine();
        }


        public override GaCodeLibraryComposerBase CreateEmptyGenerator()
        {
            return new GaClcSingleSymbolicContextCppComposer(
                Context, 
                GaClcLanguage.ExpressionConverter
            );
        }
    }
}
