using System;
using System.Globalization;
using GAPoTNumLib.GAPoT;

namespace GAPoTNumLib.Framework.Samples
{
    public static class Sample4
    {
        public static void Execute()
        {
            CultureInfo.CurrentCulture = new CultureInfo("en-US");

            var u11 = Math.Sqrt(3) * 277;
            var ua11 = -30 * Math.PI / 180;

            var mvU = $"p({u11}, {ua11}) <1,2>"
                .GaPoTNumParseVector();


            var i11 = 151.72;
            var ia11 = -48.43 * Math.PI / 180;
            var i12 = 151.72;
            var ia12 = 11.56 * Math.PI / 180;

            var mvI = $"p({i11}, {ia11}) <1,2>, p({i12}, {ia12}) <3,4>"
                .GaPoTNumParseVector();

            var mvM = mvU * mvI;

            Console.WriteLine(@"Display multivectors in LaTeX form");
            Console.WriteLine($@"U = {mvU.TermsToLaTeX()}");
            Console.WriteLine($@"I = {mvI.TermsToLaTeX()}");
            Console.WriteLine();

            Console.WriteLine(@"Display multivectors in LaTeX form");
            Console.WriteLine($@"U = {mvU.PolarPhasorsToLaTeX()}");
            Console.WriteLine($@"I = {mvI.PolarPhasorsToLaTeX()}");
            Console.WriteLine($@"M = {mvM.TermsToLaTeX()}");
            Console.WriteLine();

            Console.WriteLine(@"Display multivectors in text form");
            Console.WriteLine($@"norm2(U) = {mvU.Norm2():G}");
            Console.WriteLine($@"norm2(I) = {mvI.Norm2():G}");
            Console.WriteLine($@"norm2(M) = {mvM.Norm2():G}");
            Console.WriteLine($@"norm2(U) * norm2(I) = {(mvU.Norm2() * mvI.Norm2()):G}");
            Console.WriteLine();

            //Console.WriteLine(@"Compute and display the inverse");
            //Console.WriteLine($@"inv(U) = {mvU.Inverse()}");
            //Console.WriteLine($@"inv(I) = {mvI.Inverse()}");
            //Console.WriteLine();

            //Console.WriteLine(@"Compute and display geometric product of multivectors U * inv(U)");
            //Console.WriteLine($@"U * inv(U) = {mvU * mvU.Inverse()}");
            //Console.WriteLine($@"I * inv(I) = {mvI * mvI.Inverse()}");
            //Console.WriteLine();

            //Console.WriteLine(@"Compute and display geometric product of multivectors");
            //Console.WriteLine($@"U * I = {mvU * mvI}");
            //Console.WriteLine($@"U * I = {(mvU * mvI).ToLaTeX()}");
            //Console.WriteLine();
        }
    }
}