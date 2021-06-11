using System.Linq;
using System.Text;
using TextComposerLib.Text;

namespace TextComposerLib.Samples.Samples
{
    public static class SimpleTextSamples
    {
        internal static string Task1()
        {
            var s = new StringBuilder();

            s.AppendLine(
                Enumerable.Range(1, 5).Concatenate()
                );

            s.AppendLine(
                Enumerable.Range(1, 5).Concatenate(", ")
                );

            s.AppendLine(
                Enumerable.Range(1, 5).Concatenate(", ", "{ ", " }")
                );

            s.AppendLine(
                Enumerable.Range(1, 5).Concatenate(", ", "{ ", " }", "<", ">")
                );

            return s.ToString();
        }

        internal static string Task2()
        {
            var s = new StringBuilder();

            var first = new[] { "a", "b", "c", "d" };
            var second = new[] { "1", "2", "3", "4" };

            s.AppendLine(
                first.JoinPairs(second).Concatenate("; ")
                ).AppendLine();

            s.AppendLine(
                first.JoinPairs(second, " = ").Concatenate("; ")
                ).AppendLine();

            s.AppendLine(
                first.JoinPairs(second, " = ", "( ", " )").Concatenate("; ")
                ).AppendLine();

            return s.ToString();
        }


    }
}
