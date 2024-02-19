using GeometricAlgebraFulcrumLib.MetaProgramming.Composers;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Languages.Cpp;

public sealed class SingleMetaContextCppCodeComposer : 
    GaFuLSingleMetaContextCodeComposerBase
{
    public SingleMetaContextCppCodeComposer(MetaContext context)
        : base(context, GaFuLLanguageServerBase.Cpp11())
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
    //        ActiveFileTextComposer.Append("new " + typeInfo.GeoClcTypeSignature + "()");
    //}

    protected override void GenerateMetaContextCode()
    {
        ActiveFileTextComposer.AppendAtNewLine("public ");
        //GenerateTypeName(macroBinding.BaseMetaContext.OutputType);
        GenerateTypeName();
        ActiveFileTextComposer.Append(" ");
        ActiveFileTextComposer.Append(Context.ContextOptions.ContextName);
        ActiveFileTextComposer.Append("(");

        GenerateInputsCode(Context);

        ActiveFileTextComposer.AppendLine(")");

        ActiveFileTextComposer.AppendLineAtNewLine("{");
        ActiveFileTextComposer.IncreaseIndentation();

        ActiveFileTextComposer.AppendAtNewLine(GeoLanguage.ScalarTypeName);
        ActiveFileTextComposer.AppendLine(" result;");

        //if (macroBinding.BaseMetaContext.OutputType.IsValidScalarType)
        //{
        //    ActiveFileTextComposer.AppendAtNewLine(GeoClcLanguage.ScalarTypeName);
        //    ActiveFileTextComposer.AppendLine(" result;");
        //}
        //else
        //{
        //    ActiveFileTextComposer.AppendAtNewLine("var result = ");

        //    GenerateTypeDefaultValue(macroBinding.BaseMetaContext.OutputType);

        //    ActiveFileTextComposer.AppendLine(";");
        //}

        ActiveFileTextComposer.AppendLine();

        if (AllowGenerateMetaContextCode)
        {
            var macroComposer = InitMetaContextCodeComposer();

            ActiveFileTextComposer.AppendLineAtNewLine(
                macroComposer.Generate()
            );
        }

        ActiveFileTextComposer.AppendLineAtNewLine("return result;");

        ActiveFileTextComposer.DecreaseIndentation();
        ActiveFileTextComposer.AppendLineAtNewLine("}");
        ActiveFileTextComposer.AppendLine();
    }


    public override GaFuLCodeLibraryComposerBase CreateEmptyComposer()
    {
        return new SingleMetaContextCppCodeComposer(Context);
    }
}