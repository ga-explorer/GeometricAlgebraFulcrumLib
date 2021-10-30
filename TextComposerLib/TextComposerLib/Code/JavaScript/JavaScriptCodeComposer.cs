using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using TextComposerLib.Text;
using TextComposerLib.Text.Linear;

namespace TextComposerLib.Code.JavaScript
{
    public sealed class JavaScriptCodeComposer
    {
        public static JavaScriptCodeComposer DefaultComposer { get; }
            = new JavaScriptCodeComposer();


        public LinearTextComposer TextComposer { get; }
            = new LinearTextComposer();


        private JavaScriptCodeComposer()
        {
        }


        public JavaScriptCodeComposer Clear()
        {
            TextComposer.Clear();

            return this;
        }

        public JavaScriptCodeComposer VarLet([NotNull] string variableName)
        {
            TextComposer.AppendLineAtNewLine($"let {variableName};");

            return this;
        }

        public JavaScriptCodeComposer VarLet(params string[] variableNames)
        {
            TextComposer.AppendLineAtNewLine($"let {variableNames.Concatenate(", ")};");

            return this;
        }

        public JavaScriptCodeComposer EmptyLine()
        {
            TextComposer.AppendLineAtNewLine();

            return this;
        }

        public JavaScriptCodeComposer EmptyLines(int count)
        {
            TextComposer.AppendEmptyLines(count);

            return this;
        }

        public JavaScriptCodeComposer CodeLine([NotNull] string codeText)
        {
            TextComposer.AppendLineAtNewLine(codeText);

            return this;
        }

        public JavaScriptCodeComposer CodeLines([NotNull] IEnumerable<string> codeTextList)
        {
            foreach (var codeText in codeTextList)
                TextComposer.AppendLineAtNewLine(codeText);

            return this;
        }

        public JavaScriptCodeComposer AssignToVariable([NotNull] string variableName, [NotNull] string codeText)
        {
            TextComposer.AppendLineAtNewLine($"{variableName} = {codeText};");

            return this;
        }
        
        public JavaScriptCodeComposer AssignToVariableLet([NotNull] string variableName, [NotNull] string codeText)
        {
            TextComposer.AppendLineAtNewLine($"let {variableName} = {codeText};");

            return this;
        }
        
        public JavaScriptCodeComposer AssignToVariableConst([NotNull] string variableName, [NotNull] string codeText)
        {
            TextComposer.AppendLineAtNewLine($"const {variableName} = {codeText};");

            return this;
        }

        
        public string GetJsCode()
        {
            return TextComposer.ToString();
        }

        public override string ToString()
        {
            return GetJsCode();
        }
    }
}