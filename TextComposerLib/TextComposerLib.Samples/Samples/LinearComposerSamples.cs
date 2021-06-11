using TextComposerLib.Text.Linear;

namespace TextComposerLib.Samples.Samples
{
    public static class LinearComposerSamples
    {
        internal static string Task1()
        {
            var myClassName = "MyClass";
            var baseClassName = "BaseClass";

            var composer = new LinearTextComposer()
            {
                LineHeadersSeparator = "  "
            };

            composer.AddLineCountHeader("0000");

            composer
                .Append("public sealed class ")
                .Append(myClassName)
                .Append(" : ")
                .Append(baseClassName)
                .AppendLineAtNewLine("{");

            composer.IncreaseIndentation();

            composer
                .AppendAtNewLine("public ")
                .Append(myClassName)
                .Append("()")
                .AppendLineAtNewLine("{");

            composer.IncreaseIndentation();

            composer
                .AppendLineAtNewLine(
@"//You can add more constructor code here
//Note the correct handling of indentation
//and line numbers for this multi-line text"
                );

            composer.DecreaseIndentation();

            composer.AppendLineAtNewLine("}");

            composer
                .AppendLine()
                .AppendLineAtNewLine(@"//You can add more methods here");

            composer.DecreaseIndentation();

            composer.AppendLineAtNewLine("}");

            return composer.ToString();
        }

    }
}
