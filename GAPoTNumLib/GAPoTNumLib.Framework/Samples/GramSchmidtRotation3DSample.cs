using System;
using GAPoTNumLib.GAPoT;

namespace GAPoTNumLib.Framework.Samples
{
    public static class GramSchmidtRotation3DSample
    {
        public static void Execute()
        {
            var n = 3;

            var uFrame = GaPoTNumFrame.CreateBasisFrame(n);
            var cFrame = GaPoTNumFrame.CreateGramSchmidtFrame(n, n - 1, out var eFrame);

            var rotorsSequence = 
                GaPoTNumRotorsSequence.Create(
                    uFrame.GetRotorsToFrame(cFrame)
                ).Reverse();

            var finalRotor = 
                rotorsSequence.GetFinalRotor();

            var autoVector = GaPoTNumVector.CreateAutoVector(n);

            var rotatedAutoVector =
                rotorsSequence.Rotate(autoVector);


            //var v1 = Math.Sqrt(2);
            //var v2 = Math.Sqrt(2) * Math.Cos(-Math.PI / 3);
            //var v3 = Math.Sqrt(2) * Math.Cos(Math.PI / 3);

            var v1 = 1.0d;
            var v2 = Math.Cos(-2 * Math.PI / 3);
            var v3 = Math.Cos(2 * Math.PI / 3);

            var inputVector = new GaPoTNumVector()
                .AddTerm(1, v1)
                .AddTerm(2, v2)
                .AddTerm(3, v3);

            Console.WriteLine("Final Rotor:");
            Console.WriteLine($"{finalRotor.TermsToLaTeX()}");
            Console.WriteLine();

            Console.WriteLine("Rotated Auto Vector:");
            Console.WriteLine($"{rotatedAutoVector.TermsToLaTeX()}");
            Console.WriteLine();

            Console.WriteLine("Input Vector:");
            Console.WriteLine($"{inputVector.TermsToLaTeX()}");
            Console.WriteLine();

            var rotatedVector =
                rotorsSequence.Rotate(inputVector);

            Console.WriteLine("Rotated Vector:");
            Console.WriteLine($"{rotatedVector.TermsToLaTeX()}");
            Console.WriteLine();

            Console.WriteLine("Rotated Vector . Rotated Auto Vector:");
            Console.WriteLine($"{rotatedVector.DotProduct(rotatedAutoVector):G}");
            Console.WriteLine();

            var eI = eFrame.GetPseudoScalar();

            Console.WriteLine("Input Vector inside e-frame: ");
            Console.WriteLine($"{inputVector.Op(eI).TermsToLaTeX()}");
            Console.WriteLine();
            
            Console.WriteLine("Rotated Vector inside e-frame: ");
            Console.WriteLine($"{rotatedVector.Op(eI).TermsToLaTeX()}");
            Console.WriteLine();


            Console.WriteLine("Input Vector squared norm: ");
            Console.WriteLine($"{inputVector.Norm2()}");
            Console.WriteLine();

            Console.WriteLine("Rotated Vector squared norm: ");
            Console.WriteLine($"{rotatedVector.Norm2()}");
            Console.WriteLine();
        }
    }
}