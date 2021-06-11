using System;
using GAPoTNumLib.GAPoT;
using GAPoTNumLib.Text.LaTeX;

namespace GAPoTNumLib.Framework.Samples
{
    public static class HyperVectorsFrameSample
    {
        public static GaPoTNumLaTeXComposer LaTeXComposer { get; }
            = new GaPoTNumLaTeXComposer();


        public static void DisplayFrame(GaPoTNumFrame frame)
        {
            var frameMatrix =
                LaTeXComposer.GetDisplayEquationCode(frame.GetMatrix());

            Console.WriteLine("Frame Matrix:");
            Console.WriteLine(frameMatrix);
            Console.WriteLine();

            var innerProductsMatrixText = 
                LaTeXComposer.GetDisplayEquationCode(frame.GetInnerProductsMatrix());

            Console.WriteLine("Frame Inner Products Matrix:");
            Console.WriteLine(innerProductsMatrixText);
            Console.WriteLine();

            //Console.WriteLine("Frame Inner Angles Matrix:");
            //Console.WriteLine(frame.GetInnerAnglesMatrix().GetLaTeXDisplayEquation());
            //Console.WriteLine();

            //Console.WriteLine("Auto Vector Projection on Frame:");
            //Console.WriteLine("$" + GaPoTNumVector.CreateAutoVector(n).GetProjectionOnFrame(frame).TermsToLaTeX() + "$");
            //Console.WriteLine();
        }

        public static void Execute1()
        {
            var refVectorIndex = 0;

            for (var n = 2; n <= 10; n++)
            {
                Console.Write(@"\section*{" + n + "-Dimensions}");
                Console.WriteLine();

                var uFrame = 
                    GaPoTNumFrame.CreateBasisFrame(n);

                var uPseudoScalar = 
                    GaPoTNumMultivector
                        .CreateZero()
                        .SetTerm((1 << n) - 1, 1);

                var eFrame = 
                    GaPoTNumFrame.CreateKirchhoffFrame(n, refVectorIndex);

                //var pFrame = 
                //    uFrame.GetProjectionOnFrame(eFrame);

                var hFrame = GaPoTNumFrame.CreateHyperVectorsFrame(n);

                var cFrame = GaPoTNumFrame.CreateGramSchmidtFrame(n, refVectorIndex);

                var rotorsSequence = GaPoTNumRotorsSequence.CreateFromFrames(
                    n, 
                    cFrame,
                    uFrame
                );

                var cFrameRotationMatrix = 
                    rotorsSequence.GetFinalMatrix(n);

                var hFrameMatrixText = 
                    LaTeXComposer.GetDisplayEquationCode(hFrame.GetMatrix());

                Console.WriteLine("FBD Matrix:");
                Console.WriteLine(hFrameMatrixText);
                Console.WriteLine();

                var cFrameRotationMatrixText =
                    LaTeXComposer.GetDisplayEquationCode(cFrameRotationMatrix);

                Console.WriteLine(@"Gram Schmidt Frame Rotation Matrix:");
                Console.WriteLine(cFrameRotationMatrixText);
                Console.WriteLine();
            }
        }

        public static void Execute2()
        {
            var refVectorIndex = 0;

            for (var n = 3; n <= 5; n++)
            {
                Console.Write(@"\section*{" + n + "-Dimensions}");
                Console.WriteLine();

                var uFrame = 
                    GaPoTNumFrame.CreateBasisFrame(n);

                var uPseudoScalar = 
                    GaPoTNumMultivector
                        .CreateZero()
                        .SetTerm((1 << n) - 1, 1);

                var eFrame = 
                    GaPoTNumFrame.CreateKirchhoffFrame(n, refVectorIndex);

                var pFrame = 
                    uFrame.GetProjectionOnFrame(eFrame);

                var hFrame = GaPoTNumFrame.CreateHyperVectorsFrame(n);

                var cFrame = GaPoTNumFrame.CreateGramSchmidtFrame(n, refVectorIndex);

                var phRotorsSequence = GaPoTNumRotorsSequence.CreateFromFrames(
                    n, 
                    pFrame,
                    hFrame
                );

                var cuRotorsSequence = GaPoTNumRotorsSequence.CreateFromOrthonormalFrames(
                    cFrame,
                    uFrame
                );

                var phRotationMatrix = phRotorsSequence.GetFinalMatrix(n);
                var cuRotationMatrix = cuRotorsSequence.GetFinalMatrix(n);

                Console.WriteLine("Hyper-vectors Frame Matrix:");
                Console.WriteLine(LaTeXComposer.GetDisplayEquationCode(hFrame.GetMatrix()));
                Console.WriteLine();

                Console.WriteLine(@"p-h Rotation Matrix:");
                Console.WriteLine(LaTeXComposer.GetDisplayEquationCode(phRotationMatrix));
                Console.WriteLine();

                Console.WriteLine(@"c-u Rotation Matrix:");
                Console.WriteLine(LaTeXComposer.GetDisplayEquationCode(cuRotationMatrix));
                Console.WriteLine();
            }
        }

        public static void Execute3()
        {
            var refVectorIndex = 0;

            for (var n = 3; n <= 3; n++)
            {
                Console.Write(@"\section*{" + n + "-Dimensions}");
                Console.WriteLine();

                var uFrame =
                    GaPoTNumFrame.CreateBasisFrame(n);

                var uPseudoScalar =
                    GaPoTNumMultivector
                        .CreateZero()
                        .SetTerm((1 << n) - 1, 1);

                var eFrame =
                    GaPoTNumFrame.CreateKirchhoffFrame(n, refVectorIndex);

                var pFrame =
                    uFrame.GetProjectionOnFrame(eFrame);

                var hFrame = GaPoTNumFrame.CreateHyperVectorsFrame(n);

                var cFrame = GaPoTNumFrame.CreateGramSchmidtFrame(n, refVectorIndex);
                //var cFrame = GaPoTNumFrame.CreateClarkeFrame(n);

                var kirchhoffVector = GaPoTNumVector.CreateUnitAutoVector(n);

                var phRotor0 = GaPoTNumMultivector.CreateSimpleRotor(
                    n,
                    pFrame[0],
                    kirchhoffVector,
                    hFrame[0],
                    uFrame[n - 1]
                );

                //Apply this rotor to the pFrame and cFrame
                var pFrame1 = pFrame.ApplyRotor(phRotor0);
                

                //Console.WriteLine("pFrame Matrix:");

                //Find rotor sequence to align pFrame to hFrame
                var phRotorsSequence = 
                    GaPoTNumRotorsSequence.CreateFromFrames(n, pFrame1, hFrame);

                phRotorsSequence[0] = phRotor0;

                Console.WriteLine("pFrame to hFrame Rotors Sequence:");
                Console.WriteLine(phRotorsSequence.ToLaTeXEquationsArrays("R^{ph}", @"\mu"));
                Console.WriteLine();
                
                if (n < 4)
                {
                    var phRotor = phRotorsSequence.GetFinalRotor();
                    var (phAngle, phBlade) = phRotor.GetSimpleRotorAngleBlade();

                    var phNormal = phBlade.OrthogonalComplement(uPseudoScalar);

                    Console.WriteLine("pFrame to hFrame Final Rotor:");
                    
                    Console.WriteLine("Angle:");
                    Console.WriteLine(phAngle.GetLaTeXAngleInDegrees().GetLaTeXInlineEquation());
                    Console.WriteLine();

                    Console.WriteLine("Blade:");
                    Console.WriteLine(phBlade.ToLaTeXEquationsArray("B^{cu}",@"\mu"));
                    Console.WriteLine();

                    Console.WriteLine("Normal:");
                    Console.WriteLine(phNormal.ToLaTeXEquationsArray("n^{cu}",@"\mu"));
                    Console.WriteLine();
                }


                var cuRotor0 = GaPoTNumMultivector.CreateSimpleRotor(
                    n,
                    cFrame[0],
                    kirchhoffVector,
                    uFrame[0],
                    uFrame[n - 1]
                );

                var cFrame1 = cFrame.ApplyRotor(cuRotor0);

                var cuRotorsSequence = 
                    GaPoTNumRotorsSequence.CreateFromFrames(n,cFrame1, uFrame);

                cuRotorsSequence[0] = cuRotor0;

                Console.WriteLine("cFrame to uFrame Rotors Sequence:");
                Console.WriteLine(cuRotorsSequence.ToLaTeXEquationsArrays("R^{cu}", @"\mu"));
                Console.WriteLine();

                if (n < 4)
                {
                    var cuRotor = cuRotorsSequence.GetFinalRotor();
                    var (cuAngle, cuBlade) = cuRotor.GetSimpleRotorAngleBlade();

                    var cuNormal = cuBlade.OrthogonalComplement(uPseudoScalar);

                    Console.WriteLine("cFrame to uFrame Final Rotor:");
                    Console.WriteLine();
                    
                    Console.WriteLine("Angle:");
                    Console.WriteLine(cuAngle.GetLaTeXAngleInDegrees().GetLaTeXInlineEquation());
                    Console.WriteLine();

                    Console.WriteLine("Blade:");
                    Console.WriteLine(cuBlade.ToLaTeXEquationsArray("B^{cu}",@"\mu"));
                    Console.WriteLine();

                    Console.WriteLine("Normal:");
                    Console.WriteLine(cuNormal.ToLaTeXEquationsArray("n^{cu}",@"\mu"));
                    Console.WriteLine();
                }
            }
        }

        public static void Execute()
        {
            var refVectorIndex = 0;

            for (var n = 3; n <= 4; n++)
            {
                Console.Write(@"\section{Dimensions: " + n + "}");
                Console.WriteLine();

                var uFrame = 
                    GaPoTNumFrame.CreateBasisFrame(n);

                var uPseudoScalar = 
                    GaPoTNumMultivector
                        .CreateZero()
                        .SetTerm((1 << n) - 1, 1);

                var eFrame = 
                    GaPoTNumFrame.CreateKirchhoffFrame(n, refVectorIndex);

                var pFrame = 
                    uFrame.GetProjectionOnFrame(eFrame);

                var fbdFrame = GaPoTNumFrame.CreateHyperVectorsFrame(n);

                
                Console.Write(@"\subsection{FBD Frame:}");
                Console.WriteLine();

                DisplayFrame(fbdFrame);

                Console.Write(@"\subsection{Projected Frame:}");
                Console.WriteLine();

                DisplayFrame(pFrame);

                //var pFrame1 = pFrame
                //    .GetSubFrame(0, n - 1)
                //    .PrependVector(GaPoTNumVector.CreateAutoVector(n))
                //    .GetOrthogonalFrame(true);

                //var fbdFrame1 = fbdFrame
                //    .GetSubFrame(0, n - 1)
                //    .PrependVector(GaPoTNumVector.CreateAutoVector(n))
                //    .GetOrthogonalFrame(true);

                //var rs = GaPoTNumRotorsSequence.Create(
                //    pFrame1.GetRotorsToFrame(fbdFrame1)
                //);


                var rotorsSequence = GaPoTNumRotorsSequence.CreateFromFrames(
                    n, 
                    pFrame,
                    fbdFrame
                );

                var pFrame2 = rotorsSequence.Rotate(pFrame);

                Console.Write(@"\subsection{Rotated Projected Frame:}");
                Console.WriteLine();

                DisplayFrame(pFrame2);

                Console.WriteLine("Rotors Sequence:");

                for (var i = 0; i < rotorsSequence.Count; i++)
                {
                    var rotorEquation = 
                        rotorsSequence[i].ToLaTeXEquationsArray(
                            $"R_{{{i + 1}}}", 
                            @"\mu"
                        );

                    Console.WriteLine(rotorEquation);
                    Console.WriteLine();
                }

                Console.WriteLine("Rotation Matrix:");
                Console.WriteLine(LaTeXComposer.GetDisplayEquationCode(rotorsSequence.GetFinalMatrix(n)));
                Console.WriteLine();
            }
        }
    }
}