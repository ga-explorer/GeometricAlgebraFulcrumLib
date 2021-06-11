using System;
using System.Linq;
using System.Numerics;
using GAPoTNumLib.GAPoT;
using GAPoTNumLib.Text.LaTeX;
using MathNet.Numerics.LinearAlgebra.Complex;

namespace GAPoTNumLib.Framework.Samples
{
    public static class OrthogonalRotorsDecompositionSample
    {
        public static GaPoTNumMultivector ComplexEigenPairToBivector(Complex eigenValue, Vector eigenVector)
        {
            var realValue = eigenValue.Real;
            var imagValue = eigenValue.Imaginary;

            var realVector = new GaPoTNumVector(eigenVector.Select(c => c.Real));
            var imagVector = new GaPoTNumVector(eigenVector.Select(c => c.Imaginary));

            var scalar = realValue * realValue + imagValue * imagValue;

            var angle = Math.Atan2(imagValue, realValue);

            Console.WriteLine($"Eigen value real part: {realValue.ToString("G").GetLaTeXDisplayEquation()}");
            Console.WriteLine();

            Console.WriteLine($"Eigen value imag part: {imagValue.ToString("G").GetLaTeXDisplayEquation()}");
            Console.WriteLine();

            Console.WriteLine($"Eigen value length: {scalar.ToString("G").GetLaTeXDisplayEquation()}");
            Console.WriteLine();

            Console.WriteLine($"Eigen value angle: {angle.RadiansToDegrees().ToString("G").GetLaTeXDisplayEquation()}");
            Console.WriteLine();

            Console.WriteLine("Eigen vector real part:");
            Console.WriteLine(realVector.TermsToLaTeX().GetLaTeXDisplayEquation());
            Console.WriteLine();

            Console.WriteLine("Eigen vector imag part:");
            Console.WriteLine(imagVector.TermsToLaTeX().GetLaTeXDisplayEquation());
            Console.WriteLine();


            var blade = realVector.Op(imagVector);

            Console.WriteLine("Blade:");
            Console.WriteLine(blade.ToLaTeXEquationsArray("B", @"\mu"));
            Console.WriteLine();

            var rotor = GaPoTNumMultivector.CreateSimpleRotor(angle, blade);

            Console.WriteLine("Final rotor:");
            Console.WriteLine(rotor.ToLaTeXEquationsArray("R", @"\mu"));
            Console.WriteLine();

            Console.WriteLine($"Is simple rotor? {rotor.IsSimpleRotor()}");
            Console.WriteLine();

            Console.WriteLine();

            return rotor;
        }
        
        public static void Execute(int n)
        {
            var uFrame = 
                GaPoTNumFrame.CreateBasisFrame(n);

            var cFrame = 
                GaPoTNumFrame.CreateGramSchmidtFrame(n, 0);

            var rs1 = 
                GaPoTNumRotorsSequence.CreateFromOrthonormalFrames(cFrame, uFrame);

            var rotationMatrix = (Matrix)Matrix.Build.DenseOfArray(
                rs1.GetFinalMatrix(n).ToComplexArray()
            );

            var cFrameMatrix = (Matrix)Matrix.Build.DenseOfArray(
                cFrame.GetMatrix().ToComplexArray()
            );

            //Make sure the rotation matrix is correct
            var m = rotationMatrix.Multiply(cFrameMatrix);

            var finalRotor = rs1.GetFinalRotor();

            Console.WriteLine("Rotation matrix:");
            Console.WriteLine(rotationMatrix);
            Console.WriteLine();

            Console.WriteLine("Final rotor:");
            Console.WriteLine(finalRotor.ToLaTeXEquationsArray("R", @"\mu"));
            Console.WriteLine();

            Console.WriteLine("Rotated cFrame using matrix:");
            Console.WriteLine("MatrixForm[" + m + "]");
            Console.WriteLine();

            rotationMatrix.EigenDecomposition(out var eigenValuesExpr, out var eigenVectorsExpr);

            Console.WriteLine("Eigen values:");
            foreach (var value in eigenValuesExpr)
                Console.WriteLine(value);
            Console.WriteLine();

            Console.WriteLine("Eigen vectors:");
            foreach (var vector in eigenVectorsExpr)
                Console.WriteLine(vector);
            Console.WriteLine();


            var eigenRotorsArray = new GaPoTNumMultivector[n];

            for (var i = 0; i < n; i++)
            {
                eigenRotorsArray[i] = ComplexEigenPairToBivector(
                    eigenValuesExpr[i], 
                    eigenVectorsExpr[i]
                );
            }

            //Make sure the blades of these rotors are eigen blades of the rotation matrix
            for (var i = 0; i < n; i++)
            {
                var (angleExpr, blade) =
                    eigenRotorsArray[i].GetSimpleRotorAngleBlade();

                var bladeDiff = (rs1.Rotate(blade) - blade).Round(7);

                Console.WriteLine("Angle:");
                Console.WriteLine(angleExpr.ToString("G").GetLaTeXDisplayEquation());
                Console.WriteLine();

                Console.WriteLine("Blade:");
                Console.WriteLine(blade.ToLaTeXEquationsArray("b_1", @"\mu"));
                Console.WriteLine();

                Console.WriteLine("Rotated Blade - Blade:");
                Console.WriteLine(bladeDiff.ToLaTeXEquationsArray("b_1", @"\mu"));
                Console.WriteLine();
            }

            var diff = 
                (eigenRotorsArray[0].Gp(eigenRotorsArray[1]) -
                 eigenRotorsArray[1].Gp(eigenRotorsArray[0]))
                .Round(7);

            Console.WriteLine("Difference:");
            Console.WriteLine(diff.TermsToLaTeX().GetLaTeXDisplayEquation());
            Console.WriteLine();


            var rs = GaPoTNumRotorsSequence.Create(
                eigenRotorsArray[0],
                eigenRotorsArray[1]
            );

            //var rs = GaPoTNumRotorsSequence.Create(
            //    eigenRotorsArray[0]
            //);

            var uFrame1 = rs.Rotate(cFrame);

            Console.WriteLine("Rotated cFrame:");

            for (var i = 0; i < n; i++)
            {
                Console.WriteLine(uFrame1[i].Round(7).TermsToLaTeX().GetLaTeXDisplayEquation());
                Console.WriteLine();
            }
        }

        public static void Execute()
        {
            Execute(6);
        }
    }
}