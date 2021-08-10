using CodeComposerLib.Languages.CSharp;

namespace TextComposerLib.Samples.Samples
{
    public static class CodeComposersSamples
    {
        internal static string Task1()
        {
            //Create a syntax tree for C#
            var syntaxFactory = CclCSharpUtils.CSharp4SyntaxFactory();

            var mainCode = syntaxFactory.SyntaxElementsList();

            mainCode.Add(
                syntaxFactory.ImportNamespaces(
                    "System",
                    "System.Collections.Generic",
                    "System.Linq",
                    "System.Text"
                    )
                )
            .AddEmptyLine();

            var classCode = syntaxFactory.SingleLineComment("Your Code Goes Here!");

            mainCode.Add(
                syntaxFactory.SetNamespace("MyNamespace", classCode)
            );

            //Generate C# code from the syntax tree
            var codeComposer = CclCSharpUtils.CSharp4CodeComposer();

            return codeComposer.GenerateCode(mainCode);
        }
    }
}
