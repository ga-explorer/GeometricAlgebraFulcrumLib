using System;
using GAPoTNumLib.GAPoT;
using GAPoTNumLib.Structures;
using GAPoTNumLib.Text.Markdown;

namespace GAPoTNumLib.Framework.Samples
{
    public static class ParsingSample1
    {
        public static void Execute()
        {
            //Examples:
            //Single Phase GAPoT vector using terms form:
            //  -1.3<1>, 1.2<3>, -4.6<5>
            //
            //Single Phase GAPoT vector using polar form:
            //  p(233.92, −1.57) <1,2>, p(120, 0) <3,4>
            //
            //Single Phase GAPoT vector using rectangular form:
            //  r(10, 20) <1,2>, r(30, 0) <3,4>

            var sourceText = 
                "r(10, 20) <1,2>, -1.3<1>, r(30, 0) <3,4>, 1.2<3>, p(233.92, -1.57) <1,2>, p(120, 0) <3,4>";

            var parsingResults = new IronyParsingResults(
                new GaPoTNumVectorConstructorGrammar(), 
                sourceText
            );

            var spVector = sourceText.GaPoTNumParseVector();

            var composer = new MarkdownComposer();

            composer
                .AppendLine(parsingResults.ToString());

            if (!parsingResults.ContainsErrorMessages && spVector != null)
            {
                composer
                    .AppendLine()
                    .AppendLine(spVector.TermsToText())
                    .AppendLine()
                    .AppendLine(spVector.TermsToLaTeX())
                    .AppendLine()
                    .AppendLine(spVector.PolarPhasorsToText())
                    .AppendLine()
                    .AppendLine(spVector.PolarPhasorsToLaTeX())
                    .AppendLine()
                    .AppendLine(spVector.RectPhasorsToText())
                    .AppendLine()
                    .AppendLine(spVector.RectPhasorsToLaTeX());
            }

            Console.WriteLine(composer.ToString());
        }
    }
}
