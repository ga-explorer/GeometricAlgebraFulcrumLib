using System.Collections.Generic;
using System.Linq;
using TextComposerLib.Text;
using TextComposerLib.Text.Mapped;

namespace TextComposerLib.Samples.Samples
{
    public static class MappingComposerSamples
    {
        internal static string Task1()
        {
            var dict = new Dictionary<string, string> {{"a", "firstVar"}, {"b", "5"}};

            var text = "if (@a > @b) return @a + @b; else return @a - @b";

            var textBuilder = new MappingComposer();

            textBuilder
                .SetIdentifiedText(text, "@")
                .UniqueMarkedSegments
                .TransformUsing(dict);

            return textBuilder.FinalText;
        }

        internal static string Task2()
        {
            var text = "let v$i$ = v$i$ + Multivector(#E$id$# = 'v$i$c$id$')";

            var textBuilder = new MappingComposer();

            textBuilder
                .SetDelimitedText(text, "$", "$")
                .UniqueMarkedSegments
                .TransformByIndexUsing(index => "{" + index + "}");

            return
                textBuilder
                .UniqueMarkedSegments
                .Select(s => s.InitialText)
                .Concatenate(
                    ", ",
                    "String.Format(" + textBuilder.FinalText.ValueToQuotedLiteral() + ", ",
                    ")"
                );
        }

    }
}
