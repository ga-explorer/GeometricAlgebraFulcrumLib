using System.Collections.Generic;
using TextComposerLib.Text.Parametric;

namespace TextComposerLib.Text.Linear
{
    public static class LinearTextComposerUtils
    {
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
}
