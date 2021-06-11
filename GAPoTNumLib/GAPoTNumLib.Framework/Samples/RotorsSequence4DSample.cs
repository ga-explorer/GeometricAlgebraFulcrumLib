using System;
using System.Diagnostics;
using System.Linq;
using GAPoTNumLib.GAPoT;
using GAPoTNumLib.Text.LaTeX;

namespace GAPoTNumLib.Framework.Samples
{
    public static class RotorsSequence4DSample
    {
        public static GaPoTNumLaTeXComposer LaTeXComposer { get; }
            = new GaPoTNumLaTeXComposer();


        private static void DisplayFrames(GaPoTNumFrame sourceFrame, GaPoTNumFrame kirchhoffFrame, GaPoTNumFrame targetFrame)
        {
            var n = sourceFrame.Count;
            var matrix = targetFrame.GetMatrix(n);
            
            var kirchhoffFrameEquation = kirchhoffFrame.ToLaTeXEquationsArray(
                "e", 
                @"\mu"
            );

            Console.WriteLine("Kirchhoff Frame:");
            Console.WriteLine(kirchhoffFrameEquation);
            Console.WriteLine();

            
            var projectedFrameMatrix = sourceFrame
                .GetProjectionOnFrame(kirchhoffFrame)
                .GetMatrix(n)
                .GetLaTeXArray();

            Console.WriteLine("Projected Frame Matrix:");
            Console.WriteLine(projectedFrameMatrix);
            Console.WriteLine();

            
            var orthoFrameEquation = targetFrame.ToLaTeXEquationsArray(
                "c", 
                @"\mu"
            );

            Console.WriteLine("Orthonormal Frame:");
            Console.WriteLine(orthoFrameEquation);
            Console.WriteLine();


            Console.WriteLine("Rotation Matrix:");
            Console.WriteLine($"{matrix.GetLaTeXArray()}");
            Console.WriteLine();

            var anglesTextList = sourceFrame
                .GetAnglesToFrame(targetFrame)
                .Select(a => "$" + a.GetLaTeXAngleInDegrees() + "$")
                .ToArray();

            Console.WriteLine("Angles between frames vectors:");
            foreach (var angleText in anglesTextList)
            {
                Console.WriteLine($"{angleText}");
                Console.WriteLine();
            }

            var rotorsSequence = 
                GaPoTNumRotorsSequence.Create(
                    sourceFrame.GetRotorsToFrame(targetFrame)
                );

            //if (!rotorsSequence.ValidateRotation(sourceFrame, targetFrame))
            //    throw new InvalidOperationException("Error in rotation sequence");

            //for (var i = 0; i < sourceFrame.Count - 1; i++)
            //{
            //    var f1 = sourceFrame.GetSubFrame(0, i + 1);
            //    var f2 = targetFrame.GetSubFrame(0, i + 1);
            //    var rs = rotorsSequence.GetSubSequence(0, i + 1);

            //    if (!rs.ValidateRotation(f1, f2))
            //        throw new InvalidOperationException("Error in rotation sequence");
            //}

            Console.WriteLine("Rotors Sequence:");
            Console.WriteLine();

            for (var i = 0; i < rotorsSequence.Count; i++)
            {
                var rotorEquation = rotorsSequence[i].ToLaTeXEquationsArray(
                    $"R_{i + 1}",
                    @"\mu"
                );

                Console.WriteLine(rotorEquation);
                Console.WriteLine();
            }

            //var rotor12Equation = rotorsSequence[1].Gp(rotorsSequence[0]).ToLaTeXEquationsArray(
            //    $"R_{{12}}",
            //    @"\mu"
            //);

            //Console.WriteLine(rotor12Equation);
            //Console.WriteLine();

            //var rotor23Equation = rotorsSequence[2].Gp(rotorsSequence[1]).ToLaTeXEquationsArray(
            //    $"R_{{23}}",
            //    @"\mu"
            //);

            //Console.WriteLine(rotor23Equation);
            //Console.WriteLine();

            Console.WriteLine("Final Rotor:");
            Console.WriteLine();

            var finalRotorEquation = rotorsSequence.GetFinalRotor().ToLaTeXEquationsArray(
                "R", 
                @"\mu"
            );

            Console.WriteLine(finalRotorEquation);
            Console.WriteLine();
        }

        public static void Execute()
        {
            var n = 3;

            var fbdFrame = GaPoTNumFrame.CreateHyperVectorsFrame(n);

            var m1 = fbdFrame.GetMatrix(n - 1).GetLaTeXArray();
            var m2 = fbdFrame.GetInnerProductsMatrix().GetLaTeXArray();
            var m3 = fbdFrame.GetInnerAnglesInDegreesMatrix().GetLaTeXArray();

            Console.WriteLine("FBD Frame Matrix:");
            Console.WriteLine(m1);
            Console.WriteLine();

            Console.WriteLine("FBD Frame Inner Products Matrix:");
            Console.WriteLine(m2);
            Console.WriteLine();

            Console.WriteLine("FBD Frame Inner Angles Matrix:");
            Console.WriteLine(m3);
            Console.WriteLine();

            //return;
            
            var sourceFrame = GaPoTNumFrame.CreateBasisFrame(n);

            var uPseudoScalar = GaPoTNumMultivector
                .CreateZero()
                .SetTerm((1 << n) - 1, 1.0d);

            for (var refVectorIndex = 0; refVectorIndex < n; refVectorIndex++)
            {
                Console.Write(@"\section{Reference vector: $\mu_" + (refVectorIndex + 1) + "$}");
                Console.WriteLine();
            
                var kirchhoffFrameBase = GaPoTNumFrame.CreateEmptyFrame();

                var refVector = sourceFrame[refVectorIndex];
                for (var i = 0; i < n; i++)
                {
                    if (i == refVectorIndex)
                        continue;

                    kirchhoffFrameBase.AppendVector(sourceFrame[i] - refVector);
                }

                var kirchhoffFramesList = 
                    kirchhoffFrameBase.GetFramePermutations();

                var j = 1;
                foreach (var kirchhoffFrame in kirchhoffFramesList)
                {
                    Console.Write(@"\subsection{Kirchhoff Frame Permutation " + j + "}");
                    Console.WriteLine();
            
                    var targetFrame = kirchhoffFrame.GetOrthogonalFrame(true);
            
                    targetFrame.AppendVector(
                        -GaPoTNumUtils
                            .OuterProduct(targetFrame)
                            .Gp(uPseudoScalar.CliffordConjugate())
                            .GetVectorPart()
                    );

                    Debug.Assert(
                        targetFrame.IsOrthonormal()
                    );

                    Debug.Assert(
                        sourceFrame.HasSameHandedness(targetFrame)
                    );

                    DisplayFrames(
                        sourceFrame,
                        kirchhoffFrame,
                        targetFrame
                    );

                    j++;
                }
            }
        }
    }
}