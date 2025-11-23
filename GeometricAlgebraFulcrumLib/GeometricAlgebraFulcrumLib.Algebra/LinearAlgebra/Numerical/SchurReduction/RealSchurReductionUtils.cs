using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Numerical.Matrices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using MathNet.Numerics.LinearAlgebra;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Numerical.SchurReduction
{
    public static class RealSchurReductionUtils
    {
        /// <summary>
        /// Compute the real Schur decomposition of a real square matrix
        /// https://en.wikipedia.org/wiki/Schur_decomposition
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="tolerance"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RealSchurReductionResult GetRealSchurReduction(this Matrix<double> matrix, double tolerance = Float64Utils.DefaultTolerance)
        {
            var n = matrix.Size();
            var quasiTriangularArray = matrix.ToArray();
            var schurVectorsArray = new double[n, n];

            // https://www.alglib.net/translator/man/manual.cpp.html#unit_schur
            var success = alglib.schur.rmatrixschur(ref quasiTriangularArray, n, ref schurVectorsArray, alglib.parallel);

            var schurVectors = schurVectorsArray.ToMathNetDenseMatrix();
            var quasiTriangularMatrix = quasiTriangularArray.ToMathNetDenseMatrix();

            quasiTriangularMatrix.RemoveSmallScalars(tolerance);

            return new RealSchurReductionResult(
                matrix, 
                schurVectors, 
                quasiTriangularMatrix, 
                success, 
                tolerance
            );
        }


        
        public static void SkewSymmetricTriDiagonalExample1()
        {
            var matrix = SkewSymmetricTridiagonalMatrix.DenseRandom(
                8, new Random(10)
            ).SquareToDenseMatrix();

            Console.WriteLine(matrix.ToMatlabText());
        }

        public static void SkewSymmetricTriDiagonalExample2()
        {
            var decomp = new RealSkewSymmetricTriDiagonalSchur();

            //var matrix = SkewSymmetricTridiagonalMatrix.SparseRandom(
            //    512, 100, new Random(10)
            //);
        
            var matrix = SkewSymmetricTridiagonalMatrix.DenseRandom(
                512, new Random(10)
            );

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var result = decomp.GetRealSchurReduction(matrix);

            result.T.RemoveSmallScalars();
            stopwatch.Stop();

            Console.WriteLine(result.Q);
            Console.WriteLine(result.Q.IsOrthogonal());
            Console.WriteLine(result.T);
            Console.WriteLine(matrix.ToDenseMatrix().ComputeResidualNorm(result.Q, result.T));
            Console.WriteLine(stopwatch.Elapsed);
        }
    }
}
