using System;
using System.Globalization;
using GAPoTNumLib.GAPoT;

namespace GAPoTNumLib.Framework.Samples.Construction
{
    public static class VectorConstructionSample
    {
        public static void Execute()
        {
            CultureInfo.CurrentCulture = new CultureInfo("en-US");

            var vectorsTextArray = new []{ 
                "-1.3<1>, 1.2<3>, -4.6<6>",
                "p(233.92, -90) <1,2>, p(-120, 30) <3,4>",
                "r(10, 20) <1,2>, r(30, 0) <3,4>",
                "1<1>, r(10, 20) <3,4>, p(-120, 30) <5,6>"
            };

            foreach (var vectorText in vectorsTextArray)
            {
                var vector = vectorText.GaPoTNumParseVector();

                //Display the GAPoT vector in various text formats
                Console.WriteLine($@"Display `{vectorText}` in various text formats:");
                Console.WriteLine($@"`{vector.TermsToText()}`");
                Console.WriteLine($@"`{vector.PolarPhasorsToText()}`");
                Console.WriteLine($@"`{vector.RectPhasorsToText()}`");
                Console.WriteLine();

                //Display the GAPoT vector in various LaTeX formats
                Console.WriteLine($@"Display `{vectorText}` in various LaTeX formats:");
                Console.WriteLine($@"${vector.TermsToLaTeX()}$");
                Console.WriteLine($@"${vector.PolarPhasorsToLaTeX()}$");
                Console.WriteLine($@"${vector.RectPhasorsToLaTeX()}$");
                Console.WriteLine();
            }
        }
    }
}