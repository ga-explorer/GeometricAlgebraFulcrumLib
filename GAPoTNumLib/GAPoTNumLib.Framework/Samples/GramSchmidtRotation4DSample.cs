using System;
using GAPoTNumLib.GAPoT;
using GAPoTNumLib.Text.LaTeX;

namespace GAPoTNumLib.Framework.Samples
{
    public static class GramSchmidtRotation4DSample
    {
        public static void ComputePower()
        {
            var n = 4;

            var uFrame = GaPoTNumFrame.CreateBasisFrame(n);
            var cFrame = GaPoTNumFrame.CreateGramSchmidtFrame(n, n - 1);

            var rotorsSequence = 
                GaPoTNumRotorsSequence.Create(
                    cFrame.GetRotorsToFrame(uFrame)
                );

            var v = "-4<1>, 8<2>, -3<3>, -1<4>".GaPoTNumParseVector();
            var i = "-9<1>, 2<2>, -5<3>, 12<4>".GaPoTNumParseVector();

            var m = v.Gp(i);

            var vr = rotorsSequence.Rotate(v);
            var ir = rotorsSequence.Rotate(i);

            var mr = vr.Gp(ir);

            Console.WriteLine($"m  = {m.TermsToLaTeX().GetLaTeXDisplayEquation()}");
            Console.WriteLine($"mr = {mr.TermsToLaTeX().GetLaTeXDisplayEquation()}");
            Console.WriteLine();
        }

        public static void Execute()
        {
            var n = 4;

            var uFrame = GaPoTNumFrame.CreateBasisFrame(n);
            var cFrame = GaPoTNumFrame.CreateGramSchmidtFrame(n, n - 1, out var eFrame);

            var eFrameEquation = eFrame.ToLaTeXEquationsArray(
                "e", 
                @"\mu"
            );

            Console.WriteLine("e-frame:");
            Console.WriteLine(eFrameEquation);
            Console.WriteLine();

            var cFrameEquation = 
                cFrame.ToLaTeXEquationsArray(
                    "c", 
                    @"\mu"
                );

            Console.WriteLine("c-frame:");
            Console.WriteLine(cFrameEquation);
            Console.WriteLine();

            var cFrameMatrix = 
                cFrame.GetMatrix().GetLaTeXArray();

            Console.WriteLine("c-frame matrix:");
            Console.WriteLine(cFrameMatrix);
            Console.WriteLine();

            var rotorsSequence = 
                GaPoTNumRotorsSequence.Create(
                    cFrame.GetRotorsToFrame(uFrame)
                );

            var finalRotor = 
                rotorsSequence.GetFinalRotor();

            var R1 = rotorsSequence[1].Gp(rotorsSequence[0]);
            var R2 = rotorsSequence[2];

            var autoVector = GaPoTNumVector.CreateAutoVector(n);

            var rotatedAutoVector =
                rotorsSequence.Rotate(autoVector);

            var v1 = Math.Sqrt(2) * Math.Cos(0);
            var v2 = Math.Sqrt(2) * Math.Cos(0 - 1 * 2 * Math.PI / 4);
            var v3 = Math.Sqrt(2) * Math.Cos(0 - 2 * 2 * Math.PI / 4);
            var v4 = -(v1 + v2 + v3);

            var inputVector = new GaPoTNumVector()
                .AddTerm(1, v1)
                .AddTerm(2, v2)
                .AddTerm(3, v3)
                .AddTerm(4, v4);

            Console.WriteLine("Final Rotor:");
            Console.WriteLine($"{finalRotor.TermsToLaTeX().GetLaTeXDisplayEquation()}");
            Console.WriteLine($"{finalRotor.TermsToText()}");
            Console.WriteLine();

            Console.WriteLine("R1:");
            Console.WriteLine($"{R1.TermsToLaTeX().GetLaTeXDisplayEquation()}");
            Console.WriteLine();

            Console.WriteLine("R2:");
            Console.WriteLine($"{R2.TermsToLaTeX().GetLaTeXDisplayEquation()}");
            Console.WriteLine();

            Console.WriteLine("R2 R1 - R:");
            Console.WriteLine($"{(R2.Gp(R1) - finalRotor).TermsToText()}");
            Console.WriteLine($"{(R1.Gp(R2) - finalRotor).TermsToText()}");
            Console.WriteLine();

            Console.WriteLine("Rotated Auto Vector:");
            Console.WriteLine($"{rotatedAutoVector.TermsToLaTeX().GetLaTeXDisplayEquation()}");
            Console.WriteLine();

            Console.WriteLine("Input Vector:");
            Console.WriteLine($"{inputVector.TermsToLaTeX().GetLaTeXDisplayEquation()}");
            Console.WriteLine();

            var rotatedVector =
                rotorsSequence.Rotate(inputVector);

            Console.WriteLine("Rotated Vector:");
            Console.WriteLine($"{rotatedVector.TermsToLaTeX().GetLaTeXDisplayEquation()}");
            Console.WriteLine();

            Console.WriteLine("Rotated Vector . Rotated Auto Vector:");
            Console.WriteLine($"{rotatedVector.DotProduct(rotatedAutoVector):G}");
            Console.WriteLine();

            var eI = eFrame.GetPseudoScalar();

            Console.WriteLine("Input Vector inside e-frame: ");
            Console.WriteLine($"{inputVector.Op(eI).TermsToLaTeX().GetLaTeXDisplayEquation()}");
            Console.WriteLine();
            
            Console.WriteLine("Rotated Vector inside e-frame: ");
            Console.WriteLine($"{rotatedVector.Op(eI).TermsToLaTeX().GetLaTeXDisplayEquation()}");
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