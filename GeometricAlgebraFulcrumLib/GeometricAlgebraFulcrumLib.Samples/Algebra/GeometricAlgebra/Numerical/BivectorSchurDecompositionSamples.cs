using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Processors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Numerical;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Numerical.BivectorSchurDecomposer;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Numerical;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Numerical.Orthogonal;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace GeometricAlgebraFulcrumLib.Samples.Algebra.GeometricAlgebra.Numerical
{
    public static class BivectorSchurDecompositionSamples
    {
        public static void BivectorToMatrixExample()
        {
            const int n = 6;
            var ga = XGaFloat64EuclideanProcessor.Instance;
            var random = new Random(10);

            // Create random bivector with full structure
            var b = ga.Bivector(random.GetFloat64Array1D(n * (n - 1) / 2));
            var m = b.ToSkewSymmetricMatrix();

            Console.WriteLine($"Bivector: {b}");
            Console.WriteLine($"Matrix: {m}");
            Console.WriteLine();

            var m2 = DenseMatrix.OfColumnVectors(
                n.GetRange(i => 
                    ga.ERcp(b, i).VectorToSparseMathNetVector(n)
                )
            );
            
            Console.WriteLine($"ERcp Matrix: {m2}");
            Console.WriteLine();
        }

        public static void BivectorToTridiagonalExample()
        {
            const int n = 6;
            var ga = XGaFloat64EuclideanProcessor.Instance;
            var random = new Random(10);

            // Create random bivector with full structure
            var b = ga.Bivector(random.GetFloat64Array1D(n * (n - 1) / 2));

            Console.WriteLine($"Bivector: {b}");
            Console.WriteLine($"Matrix: {b.ToSkewSymmetricMatrix()}");
            Console.WriteLine();

            // Make bivector tridiagonal
            var (b1, hvList) = b.ToTridiagonal();
            
            Console.WriteLine($"Bivector: {b1}");
            Console.WriteLine($"Matrix: {b1.ToSkewSymmetricMatrix()}");
            Console.WriteLine();

            Console.WriteLine("Householder vectors:");
            foreach (var hv in hvList)
                Console.WriteLine("   " + hv);
            Console.WriteLine();
        }
        
        public static void TridiagonalBivectorEigenDecompositionExample1()
        {
            const int n = 32;
            var random = new Random(10);
            var sw = new Stopwatch();

            // Create random bivector with full structure
            var b = random.GetXGaBivector(n);

            //Console.WriteLine($"Bivector: {b}");
            //Console.WriteLine($"Matrix: {b.ToSkewSymmetricMatrix()}");
            //Console.WriteLine();

            sw.Start();
            // Make bivector tridiagonal
            var (bTri, hvList) = b.ToTridiagonalViaMatrix();
            sw.Stop();

            Console.WriteLine($"Tridiagonalization completed in {sw.Elapsed} seconds.");
            Console.WriteLine();

            var mTri = bTri.ToSkewSymmetricMatrix();
            var mTri2 = mTri.MatrixSquare();

            //Console.WriteLine($"Bivector: {bTri}");
            //Console.WriteLine($"Matrix: {mTri}");
            //Console.WriteLine();

            //Console.WriteLine("Householder vectors:");
            //foreach (var hv in hvList)
            //    Console.WriteLine("   " + hv);
            //Console.WriteLine();

            var k = 0;
            var m3 = mTri2.ToSparse();
            while (k < 2000 && !m3.IsNearDiagonal())
            {
                if (k % 100 == 0)
                {
                    Console.WriteLine($"G M_{k} = " + m3);
                    Console.WriteLine();
                }

                // Find strongest cross-coupling
                var (i, j) = m3.GetMaxMagnitudeItemPosition();

                m3.ZeroItemByRotation(i, j);

                m3.RemoveSmallScalars();
                
                //Debug.Assert(m3.IsNearSymmetric());

                k++;
            }

            Console.WriteLine($"G M_{k} = " + m3);
            Console.WriteLine();
        }

        public static void TridiagonalBivectorEigenDecompositionExample2()
        {
            const int n = 6;
            var ga = XGaFloat64EuclideanProcessor.Instance;
            var random = new Random(10);

            // Create random bivector with full structure
            var b = ga.Bivector(random.GetFloat64Array1D(n * (n - 1) / 2));

            Console.WriteLine($"Bivector: {b}");
            Console.WriteLine($"Matrix: {b.ToSkewSymmetricMatrix()}");
            Console.WriteLine();

            // Make bivector tridiagonal
            var (bTri, hvList) = b.ToTridiagonal();

            var mTri = bTri.ToSkewSymmetricMatrix();
            var mTri2 = mTri.MatrixSquare();

            Console.WriteLine($"Bivector: {bTri}");
            Console.WriteLine($"Matrix: {mTri}");
            Console.WriteLine();

            Console.WriteLine("Householder vectors:");
            foreach (var hv in hvList)
                Console.WriteLine("   " + hv);
            Console.WriteLine();

            // Create random vector
            var a0 = 
                ga.Vector(random.GetFloat64Array1D(n)).DivideByENorm();

            var qPrev = ga.VectorZero;
            var qCurr = a0;

            var betaPrev = 0d;

            var qList = new List<XGaFloat64Vector> { qCurr };
            var alphaList = new List<double>();
            var betaList = new List<double>();

            for (var j = 1; j <= n; j++)
            {
                // Step 1: Apply M^2 via double contraction
                var z = bTri.ERcp(bTri.ERcp(qCurr));

                // Step 2: alpha = q_curr ⋅ z
                var alpha = qCurr.ESp(z);

                // Step 3: w = z - alpha*q_curr - beta_prev*q_prev
                var w = z - alpha * qCurr;
                if (j > 1) w -= betaPrev * qPrev;

                // Step 4: beta = ||w||
                var beta = w.ENorm();

                // Step 5: Store alpha, beta
                alphaList.Add(alpha);
                betaList.Add(beta);

                // Step 6: q_next = w / beta (if beta > 0)
                if (beta < 1e-14) break;

                // Update for next iteration
                qPrev = qCurr;
                qCurr = w / beta;
                betaPrev = beta;

                qList.Add(qCurr);
            }

            var k = qList.Count;
            
            var matrixQ = qList.ColumnsToMathNetDenseMatrix(n);
            
            var matrixT = DenseMatrix.Create(k, k, 0);
            for (var i = 0; i < k; i++)
            {
                matrixT[i, i] = alphaList[i];

                if (i == k - 1) continue;

                matrixT[i, i + 1] = betaList[i];
                matrixT[i + 1, i] = betaList[i];
            }

            var reconError = (
                matrixQ.Transpose() * 
                mTri2 * 
                matrixQ - matrixT
            ).FrobeniusNorm();

            Console.WriteLine("Q = " + matrixQ);
            Console.WriteLine();

            Console.WriteLine("T = " + matrixT);
            Console.WriteLine();
            
            Console.WriteLine("Reconstruction error = " + reconError);
            Console.WriteLine();

            var evd = matrixT.Evd(Symmetricity.Symmetric);

            var eigenValues1 = evd.EigenValues.Real();
            var eigenVectors1 = matrixQ * evd.EigenVectors;

            var eigenValues = DenseVector.Create(n, 0);
            var eigenVectors = DenseMatrix.Create(n, n, 0);

            for (var i = 0; i < k; i++)
            {
                var i1 = 2 * i; 
                var i2 = 2 * i + 1;

                eigenValues[i1] = eigenValues1[i];
                eigenValues[i2] = eigenValues1[i];

                var v = eigenVectors1.Column(i);
                eigenVectors.SetColumn(i1, v);
                eigenVectors.SetColumn(i2, bTri.ERcp(v.ToXGaVector(ga)).DivideByENorm().VectorToDenseMathNetVector(n));
            }

            Console.WriteLine("Eigen values = " + eigenValues);
            Console.WriteLine();

            Console.WriteLine("Eigen vectors = " + eigenVectors);
            Console.WriteLine();
            
            Debug.Assert(eigenVectors.IsOrthogonal());

            var evdTri2 = mTri2.Evd(Symmetricity.Symmetric);
            
            Console.WriteLine("Eigen values = " + evdTri2.EigenValues.Real());
            Console.WriteLine();

            Console.WriteLine("Eigen vectors = " + evdTri2.EigenVectors);
            Console.WriteLine();

            Debug.Assert(evdTri2.EigenVectors.IsOrthogonal());

            var e = (mTri2 - eigenVectors * DenseMatrix.OfDiagonalVector(eigenValues) * eigenVectors.Transpose()).FrobeniusNorm();

            Console.WriteLine("Reconstruction error = " + e);
            Console.WriteLine();
        }
        

        //public static double[] ComputeDiagonalOfTransposeTQT(Matrix<double> T, Matrix<double> Q)
        //{
        //    var n = T.RowCount;
        //    var m = T.ColumnCount;

        //    // Output: diagonal elements
        //    var result = new double[m];

        //    // Reuse memory for column vector
        //    var tCol = Vector<double>.Build.Dense(n);

        //    for (var j = 0; j < m; j++)
        //    {
        //        // Extract j-th column of T into tCol
        //        T.Column(j, tCol);

        //        // Compute tCol' * Q * tCol
        //        result[j] = tCol.DotProduct(Q * tCol);
        //    }

        //    return result;
        //}

        public static void TridiagonalBivectorEigenDecompositionExample3()
        {
            const int n = 6;
            var random = new Random(10);
            var sw = new Stopwatch();

            // Create random bivector with full structure
            var b = random.GetXGaBivector(n);

            //Console.WriteLine($"Bivector: {b}");
            //Console.WriteLine($"Matrix: {b.ToSkewSymmetricMatrix()}");
            //Console.WriteLine();

            sw.Start();
            // Make bivector tridiagonal (TODO: GA tridiagonalization is too slow)
            var (bTri, hvList) = b.ToTridiagonalViaMatrix();
            sw.Stop();

            Console.WriteLine($"Tridiagonalization completed in {sw.Elapsed} seconds.");
            Console.WriteLine();

            var mTri = bTri.ToSkewSymmetricMatrix();
            var mTri2 = mTri.MatrixSquare();

            //Console.WriteLine($"Bivector: {bTri}");
            //Console.WriteLine($"Matrix: {mTri}");
            //Console.WriteLine();

            //Console.WriteLine("Householder vectors:");
            //foreach (var hv in hvList)
            //    Console.WriteLine("   " + hv);
            //Console.WriteLine();

            IReadOnlyList<Vector<double>> basisList = 
                DenseMatrix.CreateIdentity(n).Columns().ToArray();
                //random.GetOrthonormalVectors(n, n);

            var eigenValues = 
                basisList.Select(
                    v => v.DotProduct(mTri2 * v)
                ).ToMathNetDenseVector();
            
            var k = 0;
            while (k < 1000)
            {
                basisList = basisList
                    .Select(v => mTri2 * v)
                    .ToArray()
                    .Orthonormalize()
                    .OrthonormalBasis;
                
                k++;

                var newEigenValues = 
                    basisList.Select(
                        v => v.DotProduct(mTri2 * v)
                    ).ToMathNetDenseVector();

                if ((newEigenValues - eigenValues).L2Norm() < 1e-14) break;

                eigenValues = newEigenValues;
            }

            var eigenVectors = 
                DenseMatrix.OfColumnVectors(basisList);
                
            Console.WriteLine($"Steps = {k}");
            Console.WriteLine($"Eigen values = {eigenValues}");
            Console.WriteLine();
            Console.WriteLine($"Eigen vectors = {eigenVectors}");
            Console.WriteLine();

            var ev = 
                mTri2.Evd(Symmetricity.Symmetric).EigenValues.Real();

            Console.WriteLine($"Eigen values error: {ev - eigenValues}");
            Console.WriteLine();

            var m = eigenVectors.Transpose() * mTri * eigenVectors;
            m.RemoveSmallScalars();

            Console.WriteLine($"Mapped bivector matrix: {m}");
            Console.WriteLine();



        }
        
        public static void TridiagonalBivectorEigenDecompositionExample4()
        {
            const int n = 128;
            var random = new Random(10);
            var sw = new Stopwatch();

            // Create random bivector with full structure
            var b = random.GetXGaBivector(n);
            var bTri = b.ToTridiagonalViaMatrix().tridiagonalBivector;
            
            sw.Start();

            var mb = bTri.ToSkewSymmetricMatrix(n);

            IReadOnlyList<Vector<double>> basisList = 
                DenseMatrix.CreateIdentity(n).Columns().ToArray();
                //random.GetOrthonormalVectors(n, n);

            var matrix = mb;

            var k = 0;
            var error = double.PositiveInfinity;
            while (k < 1000)
            {
                basisList = basisList
                    .Select(v => mb * v)
                    .ToArray()
                    .Orthonormalize()
                    .OrthonormalBasis;
                
                k++;

                var ev =
                    DenseMatrix.OfColumnVectors(basisList);

                var newMatrix = ev.Transpose() * mb * ev;
                error = (newMatrix - matrix).L2Norm();
                matrix = newMatrix;

                if (error < Float64Utils.DefaultTolerance) break;
            }
            
            var eigenVectors = 
                DenseMatrix.OfColumnVectors(basisList);

            eigenVectors.RemoveSmallScalars();
            sw.Stop();

            Console.WriteLine($"{k} steps in {sw.Elapsed} seconds, error = {error}");
            Console.WriteLine($"Eigen vectors = {eigenVectors}");
            Console.WriteLine();

            var m = eigenVectors.Transpose() * mb * eigenVectors;
            m.RemoveSmallScalars();

            Console.WriteLine($"Mapped bivector matrix: {m}");
            Console.WriteLine();
        }

        public static void Example1()
        {
            try
            {
                // Run basic BivectorSchur tests
                Console.WriteLine("Running BivectorSchur Decomposition Tests...\n");
                var tests = new BivectorSchurTests();
                tests.RunAllTests();
                
                Console.WriteLine("\n" + new string('=', 60));
                Console.WriteLine("Running BivectorSchur Decomposition Examples...\n");
                var examples = new SchurDecompositionExample();
                examples.RunAllExamples();
                
                Console.WriteLine("\n" + new string('=', 80));
                Console.WriteLine("Running High-Dimensional Validation Tests...\n");
                
                // Run comprehensive high-dimensional validation tests
                try
                {
                    var validation = new HighDimensionalValidation();
                    validation.RunAllValidationTests();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"High-dimensional validation failed: {ex.Message}");
                    Console.WriteLine("Continuing with basic framework tests...");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
            }
        }
    }
}
