using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Parametric;

namespace GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;

public static class LinearTextComposerUtils
{
    public static LinearTextComposer Append(this LinearTextComposer textBuilder, ParametricTextComposer template, string parameterName, object parameterValue)
    {
        return textBuilder.Append(
            template
                .SetParametersValues(parameterName, parameterValue)
                .GenerateText()
        );
    }

    public static LinearTextComposer Append(this LinearTextComposer textBuilder, ParametricTextComposer template, string parameterName, string parameterValue)
    {
        return textBuilder.Append(
            template
                .SetParametersValues(parameterName, parameterValue)
                .GenerateText()
        );
    }

    public static LinearTextComposer Append(this LinearTextComposer textBuilder, ParametricTextComposer template, params object[] parametersValues)
    {
        var text = template.GenerateText(parametersValues);

        return textBuilder.Append(text);
    }

    public static LinearTextComposer Append(this LinearTextComposer textBuilder, ParametricTextComposer template, params string[] parametersValues)
    {
        var text = template.GenerateText(parametersValues);

        return textBuilder.Append(text);
    }

    public static LinearTextComposer Append(this LinearTextComposer textBuilder, ParametricTextComposer template, IDictionary<string, string> parametersValues)
    {
        var text = template.GenerateText(parametersValues);

        return textBuilder.Append(text);
    }

    public static LinearTextComposer Append(this LinearTextComposer textBuilder, ParametricTextComposer template, IParametricTextComposerValueSource parametersValues)
    {
        var text = template.GenerateText(parametersValues);

        return textBuilder.Append(text);
    }


    public static LinearTextComposer AppendLine(this LinearTextComposer textBuilder, ParametricTextComposer template, string parameterName, object parameterValue)
    {
        return textBuilder.AppendLine(
            template
                .SetParametersValues(parameterName, parameterValue)
                .GenerateText()
        );
    }

    public static LinearTextComposer AppendLine(this LinearTextComposer textBuilder, ParametricTextComposer template, string parameterName, string parameterValue)
    {
        return textBuilder.AppendLine(
            template
                .SetParametersValues(parameterName, parameterValue)
                .GenerateText()
        );
    }

    public static LinearTextComposer AppendLine(this LinearTextComposer textBuilder, ParametricTextComposer template, params object[] parametersValues)
    {
        var text = template.GenerateText(parametersValues);

        return textBuilder.AppendLine(text);
    }

    public static LinearTextComposer AppendLine(this LinearTextComposer textBuilder, ParametricTextComposer template, params string[] parametersValues)
    {
        var text = template.GenerateText(parametersValues);

        return textBuilder.AppendLine(text);
    }

    public static LinearTextComposer AppendLine(this LinearTextComposer textBuilder, ParametricTextComposer template, IDictionary<string, string> parametersValues)
    {
        var text = template.GenerateText(parametersValues);

        return textBuilder.AppendLine(text);
    }

    public static LinearTextComposer AppendLine(this LinearTextComposer textBuilder, ParametricTextComposer template, IParametricTextComposerValueSource parametersValues)
    {
        var text = template.GenerateText(parametersValues);

        return textBuilder.AppendLine(text);
    }


    public static LinearTextComposer AppendNewLine(this LinearTextComposer textBuilder, ParametricTextComposer template, string parameterName, object parameterValue)
    {
        return textBuilder.AppendNewLine(
            template
                .SetParametersValues(parameterName, parameterValue)
                .GenerateText()
        );
    }

    public static LinearTextComposer AppendNewLine(this LinearTextComposer textBuilder, ParametricTextComposer template, string parameterName, string parameterValue)
    {
        return textBuilder.AppendNewLine(
            template
                .SetParametersValues(parameterName, parameterValue)
                .GenerateText()
        );
    }

    public static LinearTextComposer AppendNewLine(this LinearTextComposer textBuilder, ParametricTextComposer template, params object[] parametersValues)
    {
        var text = template.GenerateText(parametersValues);

        return textBuilder.AppendNewLine(text);
    }

    public static LinearTextComposer AppendNewLine(this LinearTextComposer textBuilder, ParametricTextComposer template, params string[] parametersValues)
    {
        var text = template.GenerateText(parametersValues);

        return textBuilder.AppendNewLine(text);
    }

    public static LinearTextComposer AppendNewLine(this LinearTextComposer textBuilder, ParametricTextComposer template, IDictionary<string, string> parametersValues)
    {
        var text = template.GenerateText(parametersValues);

        return textBuilder.AppendNewLine(text);
    }

    public static LinearTextComposer AppendNewLine(this LinearTextComposer textBuilder, ParametricTextComposer template, IParametricTextComposerValueSource parametersValues)
    {
        var text = template.GenerateText(parametersValues);

        return textBuilder.AppendNewLine(text);
    }


    public static LinearTextComposer AppendAtNewLine(this LinearTextComposer textBuilder, ParametricTextComposer template, string parameterName, object parameterValue)
    {
        return textBuilder.AppendAtNewLine(
            template
                .SetParametersValues(parameterName, parameterValue)
                .GenerateText()
        );
    }

    public static LinearTextComposer AppendAtNewLine(this LinearTextComposer textBuilder, ParametricTextComposer template, string parameterName, string parameterValue)
    {
        return textBuilder.AppendAtNewLine(
            template
                .SetParametersValues(parameterName, parameterValue)
                .GenerateText()
        );
    }

    public static LinearTextComposer AppendAtNewLine(this LinearTextComposer textBuilder, ParametricTextComposer template, params object[] parametersValues)
    {
        var text = template.GenerateText(parametersValues);

        return textBuilder.AppendAtNewLine(text);
    }

    public static LinearTextComposer AppendAtNewLine(this LinearTextComposer textBuilder, ParametricTextComposer template, params string[] parametersValues)
    {
        var text = template.GenerateText(parametersValues);

        return textBuilder.AppendAtNewLine(text);
    }

    public static LinearTextComposer AppendAtNewLine(this LinearTextComposer textBuilder, ParametricTextComposer template, IDictionary<string, string> parametersValues)
    {
        var text = template.GenerateText(parametersValues);

        return textBuilder.AppendAtNewLine(text);
    }

    public static LinearTextComposer AppendAtNewLine(this LinearTextComposer textBuilder, ParametricTextComposer template, IParametricTextComposerValueSource parametersValues)
    {
        var text = template.GenerateText(parametersValues);

        return textBuilder.AppendAtNewLine(text);
    }


    public static LinearTextComposer AppendLineAtNewLine(this LinearTextComposer textBuilder, ParametricTextComposer template, string parameterName, object parameterValue)
    {
        return textBuilder.AppendLineAtNewLine(
            template
                .SetParametersValues(parameterName, parameterValue)
                .GenerateText()
        );
    }

    public static LinearTextComposer AppendLineAtNewLine(this LinearTextComposer textBuilder, ParametricTextComposer template, string parameterName, string parameterValue)
    {
        return textBuilder.AppendLineAtNewLine(
            template
                .SetParametersValues(parameterName, parameterValue)
                .GenerateText()
        );
    }

    public static LinearTextComposer AppendLineAtNewLine(this LinearTextComposer textBuilder, ParametricTextComposer template, params object[] parametersValues)
    {
        var text = template.GenerateText(parametersValues);

        return textBuilder.AppendLineAtNewLine(text);
    }

    public static LinearTextComposer AppendLineAtNewLine(this LinearTextComposer textBuilder, ParametricTextComposer template, params string[] parametersValues)
    {
        var text = template.GenerateText(parametersValues);

        return textBuilder.AppendLineAtNewLine(text);
    }

    public static LinearTextComposer AppendLineAtNewLine(this LinearTextComposer textBuilder, ParametricTextComposer template, IDictionary<string, string> parametersValues)
    {
        var text = template.GenerateText(parametersValues);

        return textBuilder.AppendLineAtNewLine(text);
    }

    public static LinearTextComposer AppendLineAtNewLine(this LinearTextComposer textBuilder, ParametricTextComposer template, IParametricTextComposerValueSource parametersValues)
    {
        var text = template.GenerateText(parametersValues);

        return textBuilder.AppendLineAtNewLine(text);
    }
}