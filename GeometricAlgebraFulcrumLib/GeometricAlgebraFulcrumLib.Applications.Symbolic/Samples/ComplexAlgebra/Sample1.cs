using GeometricAlgebraFulcrumLib.Algebra.ComplexAlgebra;
using GeometricAlgebraFulcrumLib.Mathematica.Algebra.ComplexAlgebra;
using GeometricAlgebraFulcrumLib.Mathematica.Algebra.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Mathematica.Algebra.Scalars;
using GeometricAlgebraFulcrumLib.Mathematica.Utilities.Structures;

namespace GeometricAlgebraFulcrumLib.Applications.Symbolic.Samples.ComplexAlgebra
{
    public static class Sample1
    {
        public static void Example1()
        {
            var processor = ScalarProcessorOfWolframExpr.Instance;

            var latexComposer = LaTeXComposerOfWolframExpr.DefaultComposer;

            var a = processor.CreateComplexNumber(
                "Ar".ToExpr(), 
                "Ai".ToExpr()
            );

            var b = processor.CreateComplexNumber(
                "Br".ToExpr(), 
                "Bi".ToExpr()
            );

            Console.WriteLine($@"$a = {latexComposer.GetNumberText(a)}$");
            Console.WriteLine($@"$b = {latexComposer.GetNumberText(b)}$");
            Console.WriteLine();
            
            Console.WriteLine($@"$\left| a \right| = {latexComposer.GetScalarText(a.Magnitude)}$");
            Console.WriteLine($@"$\angle a = {latexComposer.GetAngleText(a.Phase)}$");
            Console.WriteLine($@"$-a = {latexComposer.GetNumberText(-a)}$");
            Console.WriteLine($@"$a{{\prime}} = {latexComposer.GetNumberText(a.Conjugate())}$");
            Console.WriteLine($@"$a^2 = {latexComposer.GetNumberText(a.Square())}$");
            Console.WriteLine($@"$1/a = {latexComposer.GetNumberText(a.Inverse())}$");
            Console.WriteLine($@"$a+b = {latexComposer.GetNumberText(a + b)}$");
            Console.WriteLine($@"$a-b = {latexComposer.GetNumberText(a - b)}$");
            Console.WriteLine($@"$a b = {latexComposer.GetNumberText(a * b)}$");
            Console.WriteLine($@"$a / b = {latexComposer.GetNumberText(a / b)}$");
        }

        public static void Example2()
        {
            var processor = ScalarProcessorOfWolframExpr.Instance;

            var latexComposer = LaTeXComposerOfWolframExpr.DefaultComposer;

            var a1 = processor.CreateComplexNumber(
                "A1r".ToExpr(), 
                "A1i".ToExpr()
            );

            var b1 = processor.CreateComplexNumber(
                "B1r".ToExpr(), 
                "B1i".ToExpr()
            );
            
            var c1 = processor.CreateComplexNumber(
                "C1r".ToExpr(), 
                "C1i".ToExpr()
            );
            
            var a2 = processor.CreateComplexNumber(
                "A2r".ToExpr(), 
                "A2i".ToExpr()
            );

            var b2 = processor.CreateComplexNumber(
                "B2r".ToExpr(), 
                "B2i".ToExpr()
            );
            
            var c2 = processor.CreateComplexNumber(
                "C2r".ToExpr(), 
                "C2i".ToExpr()
            );

            var (x1, x2) = processor.SolveLinear2D(
                a1, b1, c1, 
                a2, b2, c2
            );

            var d1 = (a1 * x1 + b1 * x2 - c1).FullSimplifyScalars();
            var d2 = (a2 * x1 + b2 * x2 - c2).FullSimplifyScalars();

            Console.WriteLine($@"$a1 = {latexComposer.GetNumberText(a1)}$");
            Console.WriteLine($@"$b1 = {latexComposer.GetNumberText(b1)}$");
            Console.WriteLine($@"$c1 = {latexComposer.GetNumberText(c1)}$");
            Console.WriteLine();
            
            Console.WriteLine($@"$a2 = {latexComposer.GetNumberText(a2)}$");
            Console.WriteLine($@"$b2 = {latexComposer.GetNumberText(b2)}$");
            Console.WriteLine($@"$c2 = {latexComposer.GetNumberText(c2)}$");
            Console.WriteLine();
            
            Console.WriteLine($@"$x1 = {latexComposer.GetNumberText(x1)}$");
            Console.WriteLine($@"$x2 = {latexComposer.GetNumberText(x2)}$");
            Console.WriteLine();
            
            Console.WriteLine($@"$d1 = {latexComposer.GetNumberText(d1)}$");
            Console.WriteLine($@"$d2 = {latexComposer.GetNumberText(d2)}$");
            Console.WriteLine();
        }
    }
}
