//using System.Runtime.CompilerServices;
//using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Tuples;

//namespace GeometricAlgebraFulcrumLib.Algebra.Polynomials.Float64.BSplineCurveBasis;

//public class BSplineBasisSet :
//    IBSplineBasisSet
//{
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    internal static BSplineBasisSet Create(BSplineKnotVector knotVector, int degree)
//    {
//        return new BSplineBasisSet(knotVector, degree);
//    }


//    public int Degree { get; }

//    public BSplineKnotVector KnotVector { get; }

//    public int BasisCount
//        => KnotVector.GetBasisCount(Degree);


//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    private BSplineBasisSet(BSplineKnotVector knotVector, int degree)
//    {
//        if (degree < 0 || degree > knotVector.MaxDegree)
//            throw new ArgumentOutOfRangeException(nameof(degree));

//        Degree = degree;
//        KnotVector = knotVector;
//    }


//    //private double GetValue(int degree, int index, double parameterValue)
//    //{
//    //    if (degree == 0)
//    //        return KnotVector.Boxcar(index, parameterValue);

//    //    var (ti, ti1) = KnotVector.GetKnotValueRange(index, index + 1);
//    //    var (tin, tin1) = KnotVector.GetKnotValueRange(index + degree, index + degree + 1);

//    //    var vi = GetValue(degree - 1, index, parameterValue);
//    //    var vi1 = GetValue(degree - 1, index + 1, parameterValue);

//    //    var a1 = vi * (parameterValue - ti);
//    //    var a2 = tin - ti;

//    //    var b1 = vi1 * (tin1 - parameterValue);
//    //    var b2 = tin1 - ti1;

//    //    var a = a1.IsZero() || a2.IsZero() ? 0d : a1 / a2;
//    //    var b = b1.IsZero() || b2.IsZero() ? 0d : b1 / b2;

//    //    return a + b;
//    //}

//    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
//    //public double GetValue(int index, double parameterValue)
//    //{
//    //    return GetValue(Degree, index, parameterValue);
//    //}
    
//    /// <summary>
//    /// de Boor iterative form, vastly faster for real-time evaluation.
//    /// </summary>
//    /// <param name="index"></param>
//    /// <param name="t"></param>
//    /// <returns></returns>
//    public double GetValue(int index, double t)
//    {
//        var N = new double[Degree + 1];

//        for (var j = 0; j <= Degree; j++)
//            N[j] = KnotVector.Boxcar(index + j, t);

//        for (var k = 1; k <= Degree; k++)
//        for (var j = 0; j <= Degree - k; j++)
//        {
//            var left = KnotVector[index + j + k] - KnotVector[index + j];
//            var right = KnotVector[index + j + k + 1] - KnotVector[index + j + 1];

//            var a = left == 0 ? 0 : (t - KnotVector[index + j]) / left;
//            var b = right == 0 ? 0 : (KnotVector[index + j + k + 1] - t) / right;

//            N[j] = a * N[j] + b * N[j + 1];
//        }

//        return N[0];
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public double GetValue(int index, double parameterValue, double termScalar)
//    {
//        return termScalar * GetValue(index, parameterValue);
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public double GetValue(double parameterValue, params double[] termScalarsList)
//    {
//        return termScalarsList.Select(
//            (scalar, index) => GetValue(index, parameterValue, scalar)
//        ).Sum();
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public IReadOnlyList<double> GetValues(double parameterValue)
//    {
//        return Enumerable
//            .Range(0, Degree + 1)
//            .Select(index => GetValue(index, parameterValue))
//            .ToArray();
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public Float64Interval GetSupportInterval(int index)
//    {
//        return Float64Interval.Create(
//            KnotVector[index],
//            KnotVector[index + Degree + 1],
//            false,
//            false
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public BSplineBasisPairProductSet CreatePairProductSet()
//    {
//        return BSplineBasisPairProductSet.Create(this);
//    }

//}