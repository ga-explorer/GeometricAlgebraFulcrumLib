using System;
using System.Globalization;
using GAPoTNumLib.GAPoT;

namespace GAPoTNumLib.Framework.Samples
{
    public static class Sample6
    {
        public static void Execute()
        {
            CultureInfo.CurrentCulture = new CultureInfo("en-US");

            //Combine 3-phase voltage components into a single GAPoT vector
            var mvUp = "2<1>, 3<2>".GaPoTNumParseVector();
            var mvUn = "-2<1>, -1<2>".GaPoTNumParseVector();
            var mvU0 = "1<1>".GaPoTNumParseVector();

            var mvU = mvUp + mvUn.OffsetTermIDs(2) + mvU0.OffsetTermIDs(4);

            //Create a GAPoT rotor from text expression
            var mvZ = $"3 <>, -2 <1,2>, 5 <3,4>".GaPoTNumParseBiversor();

            //Compute geometric product of U Z that returns only vector components
            var mvVector = mvU * mvZ;

            //Display results
            Console.WriteLine($@"U = {mvU.TermsToLaTeX()}");
            Console.WriteLine($@"Z = {mvZ.TermsToLaTeX()}");
            Console.WriteLine($@"U gp Z = {mvVector.TermsToLaTeX()}");
            Console.WriteLine();
        }
    }
}