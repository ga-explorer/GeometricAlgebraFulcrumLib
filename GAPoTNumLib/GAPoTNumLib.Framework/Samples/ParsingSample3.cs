using System;
using GAPoTNumLib.GAPoT;
using GAPoTNumLib.Structures;
using GAPoTNumLib.Text.Markdown;

namespace GAPoTNumLib.Framework.Samples
{
    public static class ParsingSample3
    {
        public static void Execute()
        {
            //Examples:
            //GAPoT biversor using terms form:
            //  -1.3<>, 1.2<1,2>, -4.6<3,4>

            var sourceText =
                "-1.3<>, 1.2<1,2>, -4.6<3,4>";

            var parsingResults = new IronyParsingResults(
                new GaPoTNumBiversorConstructorGrammar(), 
                sourceText
            );

            var biversor = sourceText.GaPoTNumParseBiversor();

            var composer = new MarkdownComposer();

            composer
                .AppendLine(parsingResults.ToString());

            if (!parsingResults.ContainsErrorMessages && biversor != null)
            {
                composer
                    .AppendLine()
                    .AppendLine(biversor.TermsToText())
                    .AppendLine()
                    .AppendLine(biversor.TermsToLaTeX());
            }

            Console.WriteLine(composer.ToString());
        }
    }
}