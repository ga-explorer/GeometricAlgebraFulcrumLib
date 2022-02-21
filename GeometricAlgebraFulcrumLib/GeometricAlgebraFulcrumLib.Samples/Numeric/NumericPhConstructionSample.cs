using System;
using GeometricAlgebraFulcrumLib.Algebra.PolynomialAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Text;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Samples.Numeric
{
    public static class NumericPhConstructionSample
    {
        // This is a pre-defined scalar processor for numeric scalars
        public static ScalarAlgebraFloat64Processor ScalarProcessor { get; }
            = ScalarAlgebraFloat64Processor.DefaultProcessor;
            
        // Create a 3-dimensional Euclidean geometric algebra processor based on the
        // selected scalar processor
        public static GeometricAlgebraEuclideanProcessor<double> GeometricProcessor { get; } 
            = ScalarProcessor.CreateGeometricAlgebraEuclideanProcessor(3);

        // This is a pre-defined text generator for displaying multivectors
        public static TextFloat64Composer TextComposer { get; }
            = TextFloat64Composer.DefaultComposer;

        // This is a pre-defined LaTeX generator for displaying multivectors
        public static LaTeXFloat64Composer LaTeXComposer { get; }
            = LaTeXFloat64Composer.DefaultComposer;


        /// <summary>
        /// Compute and Display Bernstein Basis polynomial
        /// sets of various degrees in symbolic form
        /// </summary>
        public static void Example1()
        {
            var parameterValue = 0.5d;
            
            for (var degree = 1; degree <= 7; degree++)
            {
                Console.WriteLine($"Degree: {degree}");
                Console.WriteLine();
                
                var basis = BernsteinBasisSet<double>.Create(
                    ScalarProcessor, 
                    degree
                );
                

                var valueSum1 = ScalarProcessor.CreateScalarZero();
                for (var index = 0; index <= degree; index++)
                {
                    var value = basis.GetValue(index, parameterValue);
                    var value0 = basis.GetValue(index, 0);
                    var value1 = basis.GetValue(index, 1);

                    valueSum1 += value;

                    Console.WriteLine($@"$B_{{{index},{degree}}}\left(t\right) = {LaTeXComposer.GetScalarText(value)}$");
                    Console.WriteLine($@"$B_{{{index},{degree}}}\left(0\right) = {LaTeXComposer.GetScalarText(value0)}$");
                    Console.WriteLine($@"$B_{{{index},{degree}}}\left(1\right) = {LaTeXComposer.GetScalarText(value1)}$");
                    Console.WriteLine();
                }

                Console.WriteLine();
                Console.WriteLine($@"$\sum_{{i=0}}^{{{degree}}}B_{{i,{degree}}}\left(t\right) = {LaTeXComposer.GetScalarText(valueSum1)}$");
                Console.WriteLine();
            }
        }
        
        /// <summary>
        /// Compute and Display Generalized Blended Trigonometric Bernstein Basis polynomial
        /// sets of various degrees in symbolic form
        /// </summary>
        public static void Example3()
        {
            var parameterValue = 0.5d;
            
            for (var degree = 2; degree <= 7; degree++)
            {
                Console.WriteLine($"Degree: {degree}");
                Console.WriteLine();

                var basis = new GbtBernsteinBasisSet<double>(ScalarProcessor, degree)
                {
                    Alpha = 0,
                    Beta = 0
                };

                var valueSum1 = ScalarProcessor.CreateScalarZero();
                for (var index = 0; index <= degree; index++)
                {
                    var value = basis.GetValue(index, parameterValue);
                    var value0 = basis.GetValue(index, 0);
                    var value1 = basis.GetValue(index, 1);

                    valueSum1 += value;

                    Console.WriteLine($@"$B_{{{index},{degree}}}\left(t\right) = {LaTeXComposer.GetScalarText(value)}$");
                    Console.WriteLine($@"$B_{{{index},{degree}}}\left(0\right) = {LaTeXComposer.GetScalarText(value0)}$");
                    Console.WriteLine($@"$B_{{{index},{degree}}}\left(1\right) = {LaTeXComposer.GetScalarText(value1)}$");
                    Console.WriteLine();
                }

                Console.WriteLine();
                Console.WriteLine($@"$\sum_{{i=0}}^{{{degree}}}B_{{i,{degree}}}\left(t\right) = {LaTeXComposer.GetScalarText(valueSum1)}$");
                Console.WriteLine();
            }
        }
        
        /// <summary>
        /// Define a canonical 3D cubic PH curve using 1st degree Bernstein Basis polynomials
        /// </summary>
        public static void Example5()
        {
            const int degree = 1;

            var t = 0.5d;
            
            var basisSet = BernsteinBasisSet<double>.Create(ScalarProcessor, degree);

            var e1 = 
                GeometricProcessor.CreateVectorBasis(0);

            var p1 =
                GeometricProcessor.CreateVector(1, 1, 1);

            var d1 = p1 - e1;
            var d1Norm = d1.ENorm();
            var d1Unit = d1 / d1Norm;

            var scaledRotor0 = 
                GeometricProcessor.CreateGivensRotor(
                    1, 
                    2, 
                    0
                );

            var a0 = 
                scaledRotor0.Multivector;

            var scaledRotor1 = 
                GeometricProcessor.CreateScaledParametricPureRotor3D(
                    e1, 
                    d1Unit,
                    0,
                    d1Norm.Sqrt().ScalarValue
                );

            var a1 = 
                scaledRotor1.Multivector;

            var e1by2 = scaledRotor0.OmMap(e1);
            Console.WriteLine($@"$e1/2 = {LaTeXComposer.GetMultivectorText(e1by2)}$");
            Console.WriteLine();

            var d1by2 = scaledRotor1.OmMap(e1);
            Console.WriteLine($@"$d1/2 = {LaTeXComposer.GetMultivectorText(d1by2)}$");
            Console.WriteLine();

            var a00 = 2 * a0.Gp(e1).Gp(a0.Reverse());
            var a11 = 2 * a1.Gp(e1).Gp(a1.Reverse());
            var a01 = a0.Gp(e1).Gp(a1.Reverse()) + a1.Gp(e1).Gp(a0);

            var b0 = basisSet.GetValue(0, t);
            var b1 = basisSet.GetValue(1, t);

            var a = a0 * b0 + a1 * b1;

            var cda = a.Gp(e1).Gp(a.Reverse());

            var cd = 
                a00 * b0 * b0 + 
                a01 * b0 * b1 +
                a11 * b1 * b1;

            //var cd0 = cda.GetMultivectorStorage().MapScalars(s => s.ReplaceAll(t, double.INT_ZERO)).FullSimplifyScalars();
            //var cd1 = cda.GetMultivectorStorage().MapScalars(s => s.ReplaceAll(t, double.INT_ONE)).FullSimplifyScalars();

            Console.WriteLine($@"$c^{{{{\prime}}}} \left( t \right) = {LaTeXComposer.GetMultivectorText(cda)}$");
            Console.WriteLine($@"$c^{{{{\prime}}}} \left( t \right) = {LaTeXComposer.GetMultivectorText(cd)}$");
            //Console.WriteLine($@"$c^{{{{\prime}}}} \left( 0 \right) = {LaTeXComposer.GetMultivectorText(cd0)}$");
            //Console.WriteLine($@"$c^{{{{\prime}}}} \left( 1 \right) = {LaTeXComposer.GetMultivectorText(cd1)}$");
            Console.WriteLine();
        }
    }
}