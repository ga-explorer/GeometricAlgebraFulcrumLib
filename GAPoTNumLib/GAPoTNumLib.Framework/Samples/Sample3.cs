using System;
using System.Globalization;
using GAPoTNumLib.GAPoT;

namespace GAPoTNumLib.Framework.Samples
{
    public static class Sample3
    {
        public static void Execute()
        {
            CultureInfo.CurrentCulture = new CultureInfo("en-US");

            var mvU = 
                "p(233.92, -1.57) <1,2>, p(0.46, -2.61) <3,4>, p(4.74, 1.28) <5,6>, p(4.02, -0.07) <7,8>, p(0.42, -2.60) <9,10>"
                    .GaPoTNumParseVector();

            var mvI = 
                "p(2.33, -0.72) <1,2>, p(0.93, 1.85) <3,4>, p(0.45, -1.69) <5,6>, p(0.49, 1.70) <7,8>, p(0.16, -1.44) <9,10>"
                    .GaPoTNumParseVector();

            var mvM = mvU * mvI;

            Console.WriteLine(@"Display multivector terms in text form");
            Console.WriteLine($@"U = {mvU}");
            Console.WriteLine($@"I = {mvI}");
            Console.WriteLine($@"M = {mvM}");
            Console.WriteLine();

            Console.WriteLine(@"Display multivector terms in LaTeX form");
            Console.WriteLine($@"U = {mvU.TermsToLaTeX()}");
            Console.WriteLine($@"I = {mvI.TermsToLaTeX()}");
            Console.WriteLine($@"M = {mvM.TermsToLaTeX()}");
            Console.WriteLine();

            Console.WriteLine(@"Display multivector polar phasors in text form");
            Console.WriteLine($@"U = {mvU.PolarPhasorsToText()}");
            Console.WriteLine($@"I = {mvI.PolarPhasorsToText()}");
            Console.WriteLine();

            Console.WriteLine(@"Display multivector polar phasors in LaTeX form");
            Console.WriteLine($@"U = {mvU.PolarPhasorsToLaTeX()}");
            Console.WriteLine($@"I = {mvI.PolarPhasorsToLaTeX()}");
            Console.WriteLine();

            Console.WriteLine(@"Display U gp inv(U) in LaTeX form");
            Console.WriteLine($@"U * inv(U) = {(mvU * mvU.Inverse()).TermsToLaTeX()}");
            Console.WriteLine($@"I * inv(I) = {(mvI * mvI.Inverse()).TermsToLaTeX()}");
            Console.WriteLine();

            Console.WriteLine(@"Display M gp inv(U) in LaTeX form");
            Console.WriteLine($@"M * inv(I) = {(mvM * mvI.Inverse()).TermsToLaTeX()}");
            Console.WriteLine($@"inv(U) * M = {(mvU.Inverse() * mvM).TermsToLaTeX()}");
            Console.WriteLine();
        }
    }
}