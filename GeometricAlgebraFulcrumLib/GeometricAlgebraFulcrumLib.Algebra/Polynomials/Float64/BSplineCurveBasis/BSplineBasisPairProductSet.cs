//using System.Runtime.CompilerServices;
//using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Numerical;
//using GeometricAlgebraFulcrumLib.Algebra.Polynomials.Float64.CurveBasis;
//using MathNet.Numerics.LinearAlgebra;
//using MathNet.Numerics.LinearAlgebra.Double;

//namespace GeometricAlgebraFulcrumLib.Algebra.Polynomials.Float64.BSplineCurveBasis;

///// <summary>
///// Efficient computation of products of pairs of B-spline basis functions.
///// </summary>
//public sealed class BSplineBasisPairProductSet : IPolynomialPairProductSet
//{
//    private readonly Vector<double>[,] _xVectorArray;

//    public BSplineBasisSet BasisSet { get; }

//    public BSplineBasisSet BasisSet2 { get; }

//    public int BasisCount 
//        => BasisSet2.BasisCount;

//    public BSplineKnotVector KnotVector 
//        => BasisSet2.KnotVector;

//    public int Degree 
//        => 2 * BasisSet.Degree;

//    #region ────────── Creation ──────────

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    internal static BSplineBasisPairProductSet Create(BSplineBasisSet bSplineBasisSet)
//        => new BSplineBasisPairProductSet(bSplineBasisSet);

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    internal static BSplineBasisPairProductSet Create(BSplineKnotVector knotVector, int degree)
//        => new BSplineBasisPairProductSet(BSplineBasisSet.Create(knotVector, degree));

//    private BSplineBasisPairProductSet(BSplineBasisSet bSplineBasisSet)
//    {
//        BasisSet = bSplineBasisSet;

//        var degree = bSplineBasisSet.Degree;
//        var knotVector2 = bSplineBasisSet.KnotVector.ScaleMultiplicity(degree + 1);
//        BasisSet2 = knotVector2.CreateBSplineBasisSet(2 * degree);

//        _xVectorArray = CreateVectorXArray();
//    }

//    #endregion

//    #region ────────── Matrix Assembly ──────────

//    /// <summary>
//    /// Builds the A matrix for basis pair products using adaptive integration.
//    /// </summary>
//    private double[,] CreateMatrixA()
//    {
//        var n = BasisSet.Degree;
//        var q = BasisSet2.BasisCount;

//        var matrixA = new double[q, q];

//        for (var h = 0; h < q; h++)
//        {
//            for (var k = 0; k <= h; k++)
//            {
//                if (Math.Abs(h - k) > 2 * n) continue;

//                var interval = BasisSet2.GetSupportInterval(h)
//                    .Intersect(BasisSet2.GetSupportInterval(k));

//                if (interval.IsEmptyInterval) continue;

//                var h1 = h;
//                var k1 = k;
//                Func<double, double> integrand = h == k
//                    ? t => Math.Pow(BasisSet2.GetValue(h1, t), 2)
//                    : t => BasisSet2.GetValue(h1, t) * BasisSet2.GetValue(k1, t);

//                var item = AdaptiveIntegrate(integrand, interval.MinValue, interval.MaxValue);
//                matrixA[h, k] = matrixA[k, h] = item;
//            }
//        }

//        return matrixA;
//    }

//    /// <summary>
//    /// Numerically stable computation of A⁻¹ via Cholesky decomposition.
//    /// </summary>
//    private Matrix<double> CreateMatrixAInverse()
//    {
//        var A = Matrix.Build.DenseOfArray(CreateMatrixA());
//        Matrix<double> Ainv;

//        try
//        {
//            // Use Cholesky for SPD systems
//            var chol = A.Cholesky();
//            Ainv = chol.Solve(Matrix.Build.DenseIdentity(A.RowCount));
//        }
//        catch
//        {
//            // Fallback to general inversion if not SPD
//            Ainv = A.Inverse();
//        }

//        return Ainv;
//    }

//    /// <summary>
//    /// Builds the B vector for a given pair (i,j).
//    /// </summary>
//    private double[] CreateVectorB(int i, int j)
//    {
//        var q = BasisSet2.BasisCount;
//        var vectorB = new double[q];

//        for (var k = 0; k < q; k++)
//        {
//            var interval = BasisSet.GetSupportInterval(i)
//                .Intersect(BasisSet.GetSupportInterval(j))
//                .Intersect(BasisSet2.GetSupportInterval(k));

//            if (interval.IsEmptyInterval) continue;

//            vectorB[k] = AdaptiveIntegrate(
//                t => BasisSet.GetValue(i, t) *
//                     BasisSet.GetValue(j, t) *
//                     BasisSet2.GetValue(k, t),
//                interval.MinValue,
//                interval.MaxValue
//            );
//        }

//        return vectorB;
//    }

//    #endregion

//    #region ────────── Numerical Integration ──────────

//    /// <summary>
//    /// Adaptive Newton-Cotes integration (degree 4) until convergence.
//    /// </summary>
//    private static double AdaptiveIntegrate(Func<double, double> func, double a, double b, double tol = 1e-10)
//    {
//        var oldValue = 0d;
//        for (var m = 4; m <= 128; m *= 2)
//        {
//            var newValue = NewtonCotes(func, a, b, 4, m);
//            if (Math.Abs(newValue - oldValue) < tol && m > 4)
//                return newValue;
//            oldValue = newValue;
//        }
//        return oldValue;
//    }

//    private static double NewtonCotes(Func<double, double> f, double a, double b, int n, int m)
//    {
//        var h = (b - a) / m;
//        var sum = 0d;
//        for (var k = 0; k <= m; k++)
//        {
//            var xk = a + k * h;
//            var coeff = (k == 0 || k == m) ? 0.5 : 1.0;
//            sum += coeff * f(xk);
//        }
//        return h * sum;
//    }

//    #endregion

//    #region ────────── X Vector Computation ──────────

//    private Vector<double> CreateVectorXFromMatrixAInv(Matrix<double> matrixAInv, int i, int j)
//    {
//        var vectorB = Vector.Build.DenseOfArray(CreateVectorB(i, j));
//        return matrixAInv * vectorB;
//    }

//    private Vector<double>[,] CreateVectorXArray()
//    {
//        var p = BasisSet.BasisCount;
//        var xVectorArray = new Vector<double>[p, p];

//        var matrixAInv = CreateMatrixAInverse();

//#if DEBUG
//        Console.WriteLine("Computed A⁻¹ successfully.");
//#endif

//        for (var i = 0; i < p; i++)
//        {
//            for (var j = 0; j <= i; j++)
//            {
//                var vectorX = CreateVectorXFromMatrixAInv(matrixAInv, i, j);
//                vectorX.RemoveSmallScalars();

//                xVectorArray[i, j] = vectorX;
//                xVectorArray[j, i] = vectorX;

//#if DEBUG
//                if (vectorX.Any(v => v != 0d))
//                    Console.WriteLine($"X_{{{i},{j}}} = {vectorX}");
//#endif
//            }
//        }

//        return xVectorArray;
//    }

//    #endregion

//    #region ────────── Value Evaluation ──────────

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public double GetValue(int index1, int index2, double parameterValue)
//    {
//        var xijVector = _xVectorArray[index1, index2];
//        var q = BasisSet2.BasisCount;

//        var value = 0d;
//        for (var k = 0; k < q; k++)
//            value += xijVector[k] * BasisSet2.GetValue(k, parameterValue);

//        return value;
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public double GetValue(int index1, int index2, double parameterValue, double termScalar)
//        => termScalar * GetValue(index1, index2, parameterValue);

//    public double GetValue(double parameterValue, double[,] termScalarsList)
//    {
//        var n = BasisSet.BasisCount;
//        var total = 0d;

//        for (var i = 0; i < n; i++)
//            for (var j = 0; j < n; j++)
//                total += termScalarsList[i, j] * GetValue(i, j, parameterValue);

//        return total;
//    }

//    public double[,] GetValues(double parameterValue)
//    {
//        var n = BasisSet.BasisCount;
//        var values = new double[n, n];

//        for (var i = 0; i < n; i++)
//            for (var j = 0; j < n; j++)
//                values[i, j] = GetValue(i, j, parameterValue);

//        return values;
//    }

//    #endregion
//}


////using System.Runtime.CompilerServices;
////using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Numerical;
////using GeometricAlgebraFulcrumLib.Algebra.Polynomials.Float64.CurveBasis;
////using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
////using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Markdown;
////using MathNet.Numerics.LinearAlgebra;
////using MathNet.Numerics.LinearAlgebra.Double;

////namespace GeometricAlgebraFulcrumLib.Algebra.Polynomials.Float64.BSplineCurveBasis;

////public sealed class BSplineBasisPairProductSet :
////    IPolynomialPairProductSet
////{
////    [MethodImpl(MethodImplOptions.AggressiveInlining)]
////    internal static BSplineBasisPairProductSet Create(BSplineBasisSet bSplineBasisSet)
////    {
////        return new BSplineBasisPairProductSet(bSplineBasisSet);
////    }

////    [MethodImpl(MethodImplOptions.AggressiveInlining)]
////    internal static BSplineBasisPairProductSet Create(BSplineKnotVector knotVector, int degree)
////    {
////        var bSplineBasisSet = BSplineBasisSet.Create(knotVector, degree);
////        var basisSet = new BSplineBasisPairProductSet(bSplineBasisSet);

////        return basisSet;
////    }


////    private readonly Vector<double>[,] _xVectorArray;


////    public BSplineBasisSet BasisSet { get; }

////    public BSplineBasisSet BasisSet2 { get; }

////    public int BasisCount
////        => BasisSet2.BasisCount;

////    public BSplineKnotVector KnotVector
////        => BasisSet2.KnotVector;

////    public int Degree
////        => 2 * BasisSet.Degree;


////    private BSplineBasisPairProductSet(BSplineBasisSet bSplineBasisSet)
////    {
////        BasisSet = bSplineBasisSet;

////        var degree = bSplineBasisSet.Degree;
////        var knotVector2 = bSplineBasisSet.KnotVector.ScaleMultiplicity(degree + 1);
////        BasisSet2 = knotVector2.CreateBSplineBasisSet(2 * degree);

////        _xVectorArray = CreateVectorXArray();
////    }


////    private double[,] CreateMatrixA()
////    {
////        var n = BasisSet.Degree;
////        var q = BasisSet2.BasisCount;
////        var a = BasisSet2.KnotVector.FirstValue;
////        var b = BasisSet2.KnotVector.LastValue;

////        var matrixA = new double[q, q];
////        for (var h = 0; h < q; h++)
////        {
////            for (var k = 0; k <= h; k++)
////            {
////                if (Math.Abs(h - k) > 2 * n)
////                    continue;

////                var supportInterval =
////                    h == k
////                        ? BasisSet2.GetSupportInterval(h)
////                        : BasisSet2.GetSupportInterval(h).Intersect(BasisSet2.GetSupportInterval(k));

////                if (supportInterval.IsEmptyInterval)
////                    continue;

////                var h1 = h;
////                var k1 = k;
////                var item =
////                    h1 == k
////                        ? PolynomialUtils.NewtonCotes(
////                            t => BasisSet2.GetValue(h1, t).Square(),
////                            a, b, 4, 10
////                        )
////                        : PolynomialUtils.NewtonCotes(
////                            t => BasisSet2.GetValue(h1, t) * BasisSet2.GetValue(k1, t),
////                            a, b, 4, 10
////                        );

////                matrixA[h, k] = item;
////                if (k != h) matrixA[k, h] = item;
////            }
////        }

////        return matrixA;
////    }

////    private Matrix<double> CreateMatrixAInverse()
////    {
////        var matrixA = CreateMatrixA();
////        var p = matrixA.GetLength(0);

////        var indexList = new List<int>(p);
////        for (var i = 0; i < p; i++)
////            if (matrixA[i, i] != 0d)
////                indexList.Add(i);

////        var q = indexList.Count;
////        var a = new double[q, q];

////        for (var i = 0; i < q; i++)
////        {
////            var i1 = indexList[i];

////            for (var j = 0; j < q; j++)
////            {
////                var j1 = indexList[j];

////                a[i, j] = matrixA[i1, j1];
////            }
////        }

////        var aInv = Matrix.Build.DenseOfArray(a).Inverse();

////        for (var i = 0; i < q; i++)
////        {
////            var i1 = indexList[i];

////            for (var j = 0; j < q; j++)
////            {
////                var j1 = indexList[j];

////                matrixA[i1, j1] = aInv[i, j];
////            }
////        }

////        return Matrix.Build.DenseOfArray(matrixA);
////    }

////    private double[] CreateVectorB(int i, int j)
////    {
////        var q = BasisSet2.BasisCount;
////        var a = BasisSet2.KnotVector.FirstValue;
////        var b = BasisSet2.KnotVector.LastValue;

////        var vectorB = new double[q];

////        for (var k = 0; k < q; k++)
////        {
////            var supportInterval =
////                BasisSet
////                    .GetSupportInterval(i)
////                    .Intersect(BasisSet.GetSupportInterval(j))
////                    .Intersect(BasisSet2.GetSupportInterval(k));

////            if (supportInterval.IsEmptyInterval)
////                continue;

////            var k1 = k;
////            vectorB[k] = PolynomialUtils.NewtonCotes(
////                t =>
////                    BasisSet.GetValue(i, t) *
////                    BasisSet.GetValue(j, t) *
////                    BasisSet2.GetValue(k1, t),
////                a, b, 4, 10
////            );
////        }

////        return vectorB;
////    }

////    private double[,] CreateMatrixL()
////    {
////        var matrixA1 = CreateMatrixA();

////        var p = matrixA1.GetLength(0);

////        var indexList = new List<int>(p);
////        for (var i = 0; i < p; i++)
////            if (matrixA1[i, i] != 0d)
////                indexList.Add(i);

////        var q = indexList.Count;
////        var matrixA = new double[q, q];

////        for (var i = 0; i < q; i++)
////        {
////            var i1 = indexList[i];

////            for (var j = 0; j < q; j++)
////            {
////                var j1 = indexList[j];

////                matrixA[i, j] = matrixA1[i1, j1];
////            }
////        }

////        var n = BasisSet.Degree;
////        var matrixL = new double[q, q];

////        for (var h = 0; h < q; h++)
////        {
////            // k < h
////            var kMin = Math.Max(h - 2 * n, 0);
////            for (var k = kMin; k < h; k++)
////            {
////                var item = matrixA[h, k];

////                for (var s = kMin; s < h; s++)
////                    item -= matrixL[h, s] * matrixL[k, s];

////                matrixL[h, k] = item / matrixL[k, k];
////            }

////            // k = h
////            {
////                var item = matrixA[h, h];

////                for (var s = kMin; s < h; s++)
////                    item -= matrixL[h, s].Square();

////                matrixL[h, h] = item.Sqrt();
////            }
////        }

////        var matrixL1 = new double[p, p];

////        for (var i = 0; i < q; i++)
////        {
////            var i1 = indexList[i];

////            for (var j = 0; j < q; j++)
////            {
////                var j1 = indexList[j];

////                matrixL1[i1, j1] = matrixL[i, j];
////            }
////        }

////        return matrixL1;
////    }

////    private Vector<double> CreateVectorXFromMatrixAInv(Matrix<double> matrixAInv, int i, int j)
////    {
////        var vectorB = Vector.Build.DenseOfArray(CreateVectorB(i, j));

////        return matrixAInv * vectorB;
////    }

////    /// <summary>
////    ///  This procedure is more efficient but needs debugging
////    /// </summary>
////    /// <param name="matrixL"></param>
////    /// <param name="i"></param>
////    /// <param name="j"></param>
////    /// <returns></returns>
////    private Vector<double> CreateVectorXFromMatrixL(double[,] matrixL, int i, int j)
////    {
////        var q = BasisSet2.BasisCount;
////        var n = BasisSet.Degree;

////        var arrayB = CreateVectorB(i, j);
////        var arrayY = new double[q];
////        for (var h = 0; h < q; h++)
////        {
////            var kMin = Math.Max(h - 2 * n, 0);
////            var item = arrayB[h];

////            for (var k = kMin; k < h; k++)
////                item -= matrixL[h, k] * arrayY[k];

////            arrayY[h] = item / matrixL[h, h];
////        }

////        var arrayX = new double[q];
////        for (var h = 0; h < q; h++)
////        {
////            var kMax = Math.Min(h + 2 * n, q - 1);
////            var item = arrayY[h];

////            for (var k = h + 1; k <= kMax; k++)
////                item -= matrixL[k, h] * arrayX[k];

////            arrayX[h] = item / matrixL[h, h];
////        }

////        //for (var h = 0; h < matrixL.RowCount; h++)
////        //    if (double.IsNaN(arrayX[h]))
////        //        arrayX[h] = 0;

////        //for (var h = 0; h < q; h++)
////        //    if (arrayX[h].IsNearZero())
////        //        arrayX[h] = 0;

////        var vectorX1 = Vector.Build.DenseOfArray(arrayX);

////        //var matrixA = Matrix.Build.DenseOfArray(CreateMatrixA());
////        //var matrixAInv = matrixA.Inverse();

////        //var vectorB = Vector.Build.DenseOfArray(arrayB);

////        //var vectorX2 = matrixAInv * vectorB;

////        //for (var h = 0; h < q; h++)
////        //    if (vectorX2[h].IsNearZero())
////        //        vectorX2[h] = 0;

////        //var vectorXDiff = vectorX2 - vectorX1;

////        //var vectorB1 = matrixA * vectorX1;
////        //var vectorB2 = matrixA * vectorX2;

////        //Console.WriteLine("X1 =");
////        //Console.WriteLine(vectorX1);
////        //Console.WriteLine();

////        //Console.WriteLine("X2 =");
////        //Console.WriteLine(vectorX2);
////        //Console.WriteLine();

////        //Console.WriteLine("X1 - X2 =");
////        //Console.WriteLine(vectorXDiff);
////        //Console.WriteLine();

////        //Console.WriteLine($"B_{{{i},{j}}} =");
////        //Console.WriteLine(vectorB);
////        //Console.WriteLine();

////        //Console.WriteLine($"A X1_{{{i},{j}}} =");
////        //Console.WriteLine(vectorB1);
////        //Console.WriteLine();

////        //Console.WriteLine($"A X2_{{{i},{j}}} =");
////        //Console.WriteLine(vectorB2);
////        //Console.WriteLine();

////        return vectorX1;
////    }

////    private Vector<double>[,] CreateVectorXArray()
////    {
////        var p = BasisSet.BasisCount;

////        var xVectorArray = new Vector<double>[p, p];

////        var matrixAInv = CreateMatrixAInverse();
////        //var lMatrix = CreateMatrixL();

////        Console.WriteLine("A^{{-1}} =");
////        Console.WriteLine(matrixAInv.ToArray().ToMarkdownTable());
////        Console.WriteLine();

////        //Console.WriteLine("L =");
////        //Console.WriteLine(lMatrix.ToMarkdownTable());
////        //Console.WriteLine();

////        for (var i = 0; i < p; i++)
////            for (var j = 0; j <= i; j++)
////            {
////                //var vectorX = CreateVectorXFromMatrixL(lMatrix, i, j);
////                var vectorX = CreateVectorXFromMatrixAInv(matrixAInv, i, j);

////                vectorX.RemoveSmallScalars();

////                xVectorArray[i, j] = vectorX;
////                xVectorArray[j, i] = vectorX;

////                if (vectorX.Any(v => v != 0d))
////                {
////                    Console.WriteLine($"X_{{{i},{j}}} = {vectorX}");
////                    Console.WriteLine();
////                }
////            }

////        return xVectorArray;
////    }


////    [MethodImpl(MethodImplOptions.AggressiveInlining)]
////    public double GetValue(int index1, int index2, double parameterValue)
////    {
////        var q = BasisSet2.BasisCount;
////        var xijVector = _xVectorArray[index1, index2];

////        var value = 0d;
////        for (var k = 0; k < q; k++)
////            value += xijVector[k] * BasisSet2.GetValue(k, parameterValue);

////        return value;
////    }

////    [MethodImpl(MethodImplOptions.AggressiveInlining)]
////    public double GetValue(int index1, int index2, double parameterValue, double termScalar)
////    {
////        return termScalar * GetValue(index1, index2, parameterValue);
////    }

////    public double GetValue(double parameterValue, double[,] termScalarsList)
////    {
////        var basisCount = BasisSet.BasisCount;
////        var value = 0d;

////        for (var i = 0; i < basisCount; i++)
////            for (var j = 0; j < basisCount; j++)
////            {
////                value += GetValue(i, j, parameterValue, termScalarsList[i, j]);
////            }

////        return value;
////    }

////    public double[,] GetValues(double parameterValue)
////    {
////        var basisCount = BasisSet.BasisCount;
////        var valueArray = new double[basisCount, basisCount];

////        for (var i = 0; i < basisCount; i++)
////            for (var j = 0; j < basisCount; j++)
////            {
////                valueArray[i, j] = GetValue(i, j, parameterValue);
////            }

////        return valueArray;
////    }


////    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
////    //public BSplineBasisPairProductIntegralSet CreateIntegralSet()
////    //{
////    //    return BSplineBasisPairProductIntegralSet.Create(this);
////    //}



////}