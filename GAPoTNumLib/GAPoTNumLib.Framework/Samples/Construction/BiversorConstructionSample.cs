using System;
using System.Globalization;
using GAPoTNumLib.GAPoT;

namespace GAPoTNumLib.Framework.Samples.Construction
{
    public static class BiversorConstructionSample
    {
        public static void Execute()
        {
            CultureInfo.CurrentCulture = new CultureInfo("en-US");

            var v1 = "p(233.92, -90) <1,2>, p(-120, 30) <3,4>"
                .GaPoTNumParseVector();

            var v2 = "r(10, 20) <1,2>, p(-120, 30) <3,4>"
                .GaPoTNumParseVector();

            var biversorsArray = new [] { 
                "-1.3<>, 1.2<1,2>, -4.6<3,4>".GaPoTNumParseBiversor(),
                v1.Gp(v2)
            };

            foreach (var biversor in biversorsArray)
            {
                //Display the GAPoT biversor in various formats
                Console.WriteLine($@"Display the GAPoT biversor in various formats:");
                Console.WriteLine($@"`{biversor.TermsToText()}`");
                Console.WriteLine($@"${biversor.TermsToLaTeX()}$");
                Console.WriteLine();
            }
        }
    }
}