using System;
using GAPoTNumLib.GAPoT;
using GAPoTNumLib.Structures;
using GAPoTNumLib.Text.Markdown;

namespace GAPoTNumLib.Framework.Samples
{
    public static class ParsingSample2
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
                "-1.3<1>, 1.2<3>, -4.6<5>, p(233.92, −1.57) <7,8>, r(10, 20) <9,10>, r(30, 0) <11,12>";

            var parsingResults = new IronyParsingResults(
                new GaPoTNumVectorConstructorGrammar(), 
                sourceText
            );

            var mpVector = sourceText.GaPoTNumParseVector();

            var composer = new MarkdownComposer();

            composer
                .AppendLine(parsingResults.ToString());

            if (!parsingResults.ContainsErrorMessages && mpVector != null)
            {
                composer
                    .AppendLine()
                    .AppendLine(mpVector.TermsToText())
                    .AppendLine()
                    .AppendLine(mpVector.TermsToLaTeX())
                    .AppendLine()
                    .AppendLine(mpVector.PolarPhasorsToText())
                    .AppendLine()
                    .AppendLine(mpVector.PolarPhasorsToLaTeX())
                    .AppendLine()
                    .AppendLine(mpVector.RectPhasorsToText())
                    .AppendLine()
                    .AppendLine(mpVector.RectPhasorsToLaTeX());
            }

            Console.WriteLine(composer.ToString());
        }
    }
}