﻿using GeometricAlgebraFulcrumLib.CodeComposer.Composers;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Context;

namespace GeometricAlgebraFulcrumLib.CodeComposer.LanguageServers.CSharp
{
    public sealed class GaClcSingleSymbolicContextCSharpComposer : 
        GaClcSingleSymbolicContextCodeComposerBase
    {
        public GaClcSingleSymbolicContextCSharpComposer(SymbolicContext context, GaClcLanguageExpressionConverter expressionConverter)
            : base(context, GaClcLanguageServer.CSharp(expressionConverter))
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
                var macroComposer = InitSymbolicContextCodeComposer();

                ActiveFileTextComposer.AppendLineAtNewLine(
                    macroComposer.Generate()
                    );
            }

            ActiveFileTextComposer.AppendLineAtNewLine("return result;");

            ActiveFileTextComposer.DecreaseIndentation();
            ActiveFileTextComposer.AppendLineAtNewLine("}");
            ActiveFileTextComposer.AppendLine();
        }


        public override GaCodeLibraryComposerBase CreateEmptyComposer()
        {
            return new GaClcSingleSymbolicContextCSharpComposer(
                Context,
                GaClcLanguage.ExpressionConverter
            );
        }
    }
}