using System;
using System.Linq;
using TextComposerLib.Text.Structured;

namespace TextComposerLib.Samples.Samples
{
    public static class StructuredComposersSamples
    {
        internal static string Task1()
        {
            var composer = new ListTextComposer()
            {
                Separator = ", ",
                FinalPrefix = "{",
                FinalSuffix = "}",
                ActiveItemPrefix = "<",
                ActiveItemSuffix = ">"
            };

            composer.AddRange(Enumerable.Range(1, 5));

            return composer.ToString();
        }

        internal static string Task2()
        {
            return
                Enumerable.Range(1, 5)
                .ComposeToList(", ", "{", "}", "'", "'")
                .ToString();
        }

        internal static string Task3()
        {
            var listComposer = new ListTextComposer(Environment.NewLine);

            var stackComposer =
                Enumerable.Range(1, 3)
                    .ComposeToStack(", ", "{", "}", "'", "'");

            stackComposer.ActiveItemPrefix = "<";
            stackComposer.ActiveItemSuffix = ">";

            stackComposer.PushRange("a", "b", "c");

            while (stackComposer.Count > 0)
            {
                listComposer.Add(stackComposer.ToString());

                stackComposer.Pop();
            }

            return listComposer.ToString();
        }

    }
}
