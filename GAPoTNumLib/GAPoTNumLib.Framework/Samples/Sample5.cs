using System;
using System.Globalization;
using GAPoTNumLib.GAPoT;

namespace GAPoTNumLib.Framework.Samples
{
    public static class Sample5
    {
        public static void Execute()
        {
            CultureInfo.CurrentCulture = new CultureInfo("en-US");

            var u1 = 200 / Math.Sqrt(3);
            var u34 = 100 / Math.Sqrt(3);
            var ua34 = 60 * Math.PI / 180;

            var mvU = $"{u1}<1>, p({u34}, {ua34}) <3,4>"
                .GaPoTNumParseVector();

            var i12 = 193.18;
            var ia12 = -45 * Math.PI / 180;
            var i34 = 100 * Math.Sqrt(2);
            var ia34 = -165 * Math.PI / 180;

            var mvI = $"p({i12}, {ia12}) <1,2>, p({i34}, {ia34})<3,4>"
                .GaPoTNumParseVector();

            var mvM = mvU * mvI;

            //This computes per-phase impedance biversors;
            //here we have two phases, each phase contains 2 terms.
            var mvUParts = mvU.GetParts(2, 2);
            var mvIParts = mvI.GetParts(2, 2);
            var mvZ = mvU.GetPartsImpedance(mvI, 2, 2);

            Console.WriteLine(@"Display multivectors terms in LaTeX form");
            Console.WriteLine($@"U = {mvU.TermsToLaTeX()}");
            Console.WriteLine($@"I = {mvI.TermsToLaTeX()}");
            Console.WriteLine();

            Console.WriteLine(@"Display multivectors in polar LaTeX form");
            Console.WriteLine($@"U = {mvU.PolarPhasorsToLaTeX()}");
            Console.WriteLine($@"I = {mvI.PolarPhasorsToLaTeX()}");
            Console.WriteLine($@"M = {mvM.TermsToText()}");
            Console.WriteLine();

            Console.WriteLine(@"Display parts of power biversor");
            Console.WriteLine($@"Active total = {mvM.GetActiveTotal():G}");
            Console.WriteLine($@"Active part = {mvM.GetActivePart().ToLaTeX()}");
            Console.WriteLine($@"Reactive total = {mvM.GetReactiveTotal():G}");
            Console.WriteLine($@"Reactive part = {mvM.GetReactivePart().ToLaTeX()}");
            Console.WriteLine($@"Non-active total = {mvM.GetNonActiveTotal():G}");
            Console.WriteLine($@"Non-active part = {mvM.GetNonActivePart().ToLaTeX()}");
            Console.WriteLine($@"Reactive fundamental total = {mvM.GetReactiveFundamentalTotal():G}");
            Console.WriteLine($@"Reactive fundamental part = {mvM.GetReactiveFundamentalPart().ToLaTeX()}");
            Console.WriteLine($@"Harm total = {mvM.GetHarmTotal():G}");
            Console.WriteLine($@"Harm part = {mvM.GetHarmPart().ToLaTeX()}");
            Console.WriteLine($@"Selected part value = {mvM.GetTermValue(2, 3):G}");
            Console.WriteLine($@"Selected part = {mvM.GetTerm(2, 3).ToLaTeX()}");
            Console.WriteLine();

            Console.WriteLine(@"Display voltage, current, and impedance of phases in LaTeX form");
            Console.WriteLine($@"Ua = {mvUParts[0].TermsToLaTeX()}");
            Console.WriteLine($@"Ub = {mvUParts[1].TermsToLaTeX()}");
            Console.WriteLine($@"Ia = {mvIParts[0].TermsToLaTeX()}");
            Console.WriteLine($@"Ib = {mvIParts[1].TermsToLaTeX()}");
            Console.WriteLine($@"Za = {mvZ[0].TermsToLaTeX()}");
            Console.WriteLine($@"Zb = {mvZ[1].TermsToLaTeX()}");
            Console.WriteLine();

            Console.WriteLine(@"Display norms of GAPoT multivectors");
            Console.WriteLine($@"norm(U) = {mvU.Norm():G}");
            Console.WriteLine($@"norm(I) = {mvI.Norm():G}");
            Console.WriteLine($@"norm(M) = {mvM.Norm():G}");
            Console.WriteLine($@"norm(U) * norm(I) = {(mvU.Norm() * mvI.Norm()):G}");
            Console.WriteLine($@"norm(U) * norm(I) - norm(M) = {(mvU.Norm() * mvI.Norm() - mvM.Norm()):G}");
            Console.WriteLine();
        }
    }
}