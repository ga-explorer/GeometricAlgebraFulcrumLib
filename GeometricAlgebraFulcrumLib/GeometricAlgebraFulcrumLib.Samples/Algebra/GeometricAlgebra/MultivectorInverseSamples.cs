using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Processors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Numerical;

namespace GeometricAlgebraFulcrumLib.Samples.Algebra.GeometricAlgebra
{
    //public class Multivector
//{
//    public double[] Coefficients; // in basis blade order
//    public int N; // 2^n

//    public Multivector(int n)
//    {
//        N = 1 << n;
//        Coefficients = new double[N];
//    }

//    public Multivector(double[] coeffs)
//    {
//        Coefficients = (double[])coeffs.Clone();
//        N = Coefficients.Length;
//    }

//    // Overloaded: A + B
//    public static Multivector operator +(Multivector a, Multivector b)
//    {
//        var r = new Multivector(a.N);
//        for (int i = 0; i < a.N; i++)
//            r.Coefficients[i] = a.Coefficients[i] + b.Coefficients[i];
//        return r;
//    }

//    // Overloaded: c * A
//    public static Multivector operator *(double c, Multivector a)
//    {
//        var r = new Multivector(a.N);
//        for (int i = 0; i < a.N; i++)
//            r.Coefficients[i] = c * a.Coefficients[i];
//        return r;
//    }

//    // You must implement: A * B (geometric product)
//    // This depends on the signature (p,q)
//    // For now, assume it's provided
//    public static Multivector GeometricProduct(Multivector a, Multivector b, int[] basisSigns)
//    {
//        // basisSigns[i] = sign of e_I^2 for blade i
//        // Requires full multiplication table or on-the-fly computation
//        // This is complex — use a library or precomputed table
//        // Placeholder:
//        throw new NotImplementedException("Implement geometric product.");
//    }
//}

public static class MultivectorInverseSamples
    {
        public static void Example1()
        {
            var matrix = new[,]{
                { -2, -6,  4 },
                {  8,  9,  7 },
                { -3,  1,  5 }
            }.ToMathNetDenseMatrix();

            Console.WriteLine($"Determinant: {matrix.Determinant()}");
            Console.WriteLine();

            var matrixInv =
                matrix.Inverse();

            var matrixInvC1 = matrix.FirstColumnOfInverse();

            Console.WriteLine(matrixInv);
            Console.WriteLine();

            Console.WriteLine(matrixInvC1);
            Console.WriteLine();

            var b = matrix.Multiply(matrixInv);

            Console.WriteLine(b);
            Console.WriteLine();
        }

        public static void Example2()
        {
            const int n = 10;
            var randGen = new Random(10);
            

            var ga = XGaFloat64EuclideanProcessor.Instance;

            var mv = ga.Multivector(
                randGen.GetFloat64Array1D(1 << n, -1, 1)
            );

            //var v1 = mv.LeftGpMapArray(n).FirstColumnOfInverse();
            //var v2 = 
            //    Matrix.Build
            //        .DenseOfArray(mv.LeftGpMapArray(n))
            //        .Inverse()
            //        .Column(0)
            //        .ToArray();

            //Console.WriteLine(v1.GetArrayText());
            //Console.WriteLine(v2.GetArrayText());

            var t1 = DateTime.Now;
            var mvInv = ga.Multivector(
                mv.LeftGpMapMatrix(n).FirstColumnOfInverse().ToArray()
            );
            Console.WriteLine(DateTime.Now - t1);
            Console.WriteLine();

            //var mvInv = 
            //    ga.Multivector(
            //        Matrix.Build.DenseOfArray(mv.LeftGpMapArray(n))
            //            .Inverse()
            //            .Column(0)
            //            .ToArray()
            //    );

            var d = mvInv.Gp(mv).RemoveSmallTerms();

            Console.WriteLine(d);
        }

        public static void Example3()
        {
            const int n = 5;
            const double epsilon = 1e-16;
            var randGen = new Random(10);
            

            var ga = XGaFloat64EuclideanProcessor.Instance;

            var mv = ga.Multivector(
                randGen.GetFloat64Array1D(1 << n, -1, 1)
            ).DivideByNorm();

            var mvRev = mv.Reverse();

            var mvInv = mvRev / mvRev.Gp(mv).Scalar();
            var err = mv.Gp(mvInv) - 1;
            
            while (!err.IsNearZero(epsilon))
            {
                Console.WriteLine(err.ENorm());

                mvInv = 2 * mvInv - mvInv.Gp(mv).Gp(mvInv);
                err = mv.Gp(mvInv) - 1;
            }

            Console.WriteLine(err.ENorm());
            Console.WriteLine();

            var d = mvInv.Gp(mv).RemoveSmallTerms();

            Console.WriteLine(d);
        }

        public static void Example4()
        {
            const int n = 3;
            const double epsilon = 1e-16;
            var randGen = new Random(10);
            

            var ga = XGaFloat64EuclideanProcessor.Instance;

            var mv = ga.Multivector(
                randGen.GetFloat64Array1D(1 << n, -1, 1)
            );

            var (coeffs, degree) = mv.GetMinimalPolynomial();
            //var coeffs = mv.GetCharacteristicPolynomial();

            Console.WriteLine(degree);
            Console.WriteLine(coeffs);
            Console.WriteLine();

            var flag = mv.ValidateMinimalPolynomial(coeffs);
            Console.WriteLine(flag);
            Console.WriteLine();

        }
    }
}
