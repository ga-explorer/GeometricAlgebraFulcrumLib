using System;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Processors;

namespace GeometricAlgebraFulcrumLib.Matlab.Samples.GeometricAlgebra
{
    public static class Samples1
    {
        public static XGaFloat64EuclideanProcessor VGa 
            => XGaFloat64EuclideanProcessor.Instance;


        public static void Example1()
        {
            var a = 
                VGa.Parse("-1.2 + 3.2<0> + 1.5<1> - 0.2<2> - 2.1<0,1> - 3.7<0,2> + 4.7<1,2> - 5.6<0,1,2>");
            
            var b = 
                VGa.Parse("2.5 + 3<0> - 2.6<1> - 2.2<2> + 3.2<0,1> - 1.1<0,2> + 2<1,2> + 3.6<0,1,2>");

            Console.WriteLine(a.ToString());
            Console.WriteLine(b.ToString());
            Console.WriteLine();

            Console.WriteLine("Geometric Product: ");
            Console.WriteLine(a.Gp(b).ToString());
            Console.WriteLine();

            Console.WriteLine("Outer Product: ");
            Console.WriteLine(a.Op(b).ToString());
            Console.WriteLine();

            Console.WriteLine("Scalar Product: ");
            Console.WriteLine(a.Sp(b).ToString());
            Console.WriteLine();

            Console.WriteLine("Left-Contraction Product: ");
            Console.WriteLine(a.Lcp(b).ToString());
            Console.WriteLine();

            Console.WriteLine("Right-Contraction Product: ");
            Console.WriteLine(a.Rcp(b).ToString());
            Console.WriteLine();

            Console.WriteLine("Dot Product: ");
            Console.WriteLine(a.Fdp(b).ToString());
            Console.WriteLine();

            Console.WriteLine("Inner Product: ");
            Console.WriteLine(a.Hip(b).ToString());
            Console.WriteLine();

            Console.WriteLine("Commutator Product: ");
            Console.WriteLine(a.Cp(b).ToString());
            Console.WriteLine();

            Console.WriteLine("Anti-Commutator Product: ");
            Console.WriteLine(a.Acp(b).ToString());
            Console.WriteLine();

            Console.WriteLine("Addition: ");
            Console.WriteLine((a + b).ToString());
            Console.WriteLine();

            Console.WriteLine("Subtraction: ");
            Console.WriteLine((a - b).ToString());
            Console.WriteLine();

            Console.WriteLine("Times scalar: ");
            Console.WriteLine((4 * a).ToString());
            Console.WriteLine();

            Console.WriteLine("Divide by scalar: ");
            Console.WriteLine((a / 4).ToString());
            Console.WriteLine();

            Console.WriteLine("Grade Involution: ");
            Console.WriteLine(a.GradeInvolution().ToString());
            Console.WriteLine();

            Console.WriteLine("Reverse: ");
            Console.WriteLine(a.Reverse().ToString());
            Console.WriteLine();

            Console.WriteLine("Clifford Conjugate: ");
            Console.WriteLine(a.CliffordConjugate().ToString());
            Console.WriteLine();

            //var composer = VGa.CreateKVectorComposer(5);

            //composer.SetTerm([1, 4, 2], 1.2);
        }
    }
}
