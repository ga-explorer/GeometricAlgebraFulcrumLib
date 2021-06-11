using System.Collections.Generic;
using TextComposerLib.Text.Linear;
using TextComposerLib.TextExpressions;
using TextComposerLib.TextExpressions.Ast;

namespace TextComposerLib.Samples.Samples
{
    public static class TextExpressionSamples
    {
        /// <summary>
        /// A simple class that replaces the names of composite expressions using a dictionary
        /// </summary>
        internal class Task1Class : TextExpressionVisitor
        {
            private readonly Dictionary<string, string> _namesDictionary;

            private Task1Class(Dictionary<string, string> namesDictionary)
            {
                _namesDictionary = namesDictionary;
            }

            public override void Visit(TeLiteralNumber textExpr) { }

            public override void Visit(TeLiteralString textExpr) { }

            public override void Visit(TeIdentifier textExpr) { }

            public override void Visit(TeList textExpr)
            {
                string newName;

                if (textExpr.IsNamed && _namesDictionary.TryGetValue(textExpr.Name.ToString(), out newName))
                    textExpr.Name.Text = newName;

                foreach (var subExpr in textExpr.ChildExpressions)
                    Visit(subExpr);
            }

            public override void Visit(TeDictionary textExpr)
            {
                string newName;

                if (textExpr.IsNamed && _namesDictionary.TryGetValue(textExpr.Name.ToString(), out newName))
                    textExpr.Name.Text = newName;

                foreach (var subExpr in textExpr.ChildExpressions)
                    Visit(subExpr);
            }

            public static void ReplaceNames(ITextExpression textExpr, Dictionary<string, string> namesDictionary)
            {
                var task1Object = new Task1Class(namesDictionary);

                task1Object.Visit(textExpr);
            }
        }

        internal static string Task1()
        {
            var dict = new TeDictionary
            {
                {"A", new TeLiteralNumber(1)},
                {"B", new TeLiteralNumber(2)},
                {"C", new TeLiteralNumber(3)}
            };

            var listExpr = new TeList
            {
                new TeLiteralNumber(2.7D),
                new TeIdentifier("Var1"),
                new TeLiteralString("Testing 1 2 3"),
                dict
            };

            listExpr.Name = new TeIdentifier("List");

            return TextExpressionComposer.Generate(listExpr);
        }

        internal static string Task2()
        {
            //Parse text into an expression tree
            var textExpr = 
                "Add{ V1 : Add[-2.5, Vector[2, 3, -4]], V2 : Vector[x, 2, -4.7] }".ParseToTextExpression();

            var composer = new LinearTextComposer();

            composer
                .AppendLine("Before replacement: ")
                .AppendLine(TextExpressionComposer.Generate(textExpr))
                .AppendLine();

            //Replace "Add" by "Plus" and "Vector" by "List" in the expression tree
            Task1Class.ReplaceNames(
                textExpr,
                new Dictionary<string, string>{{"Add","Plus"},{"Vector","List"}}
                );

            composer
                .AppendLine("After replacement: ")
                .AppendLine(TextExpressionComposer.Generate(textExpr));

            return composer.ToString();
        }
    }
}
