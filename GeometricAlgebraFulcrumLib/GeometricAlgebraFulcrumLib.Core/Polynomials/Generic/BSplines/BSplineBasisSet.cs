using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Polynomials.Generic.Basis;
using GeometricAlgebraFulcrumLib.Core.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.Core.Polynomials.Generic.BSplines;

public class BSplineBasisSet<T> :
    IPolynomialBasisSet<T>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static BSplineBasisSet<T> Create(BSplineKnotVector<T> knotVector, int degree)
    {
        return new BSplineBasisSet<T>(knotVector, degree);
    }


    public IScalarProcessor<T> ScalarProcessor 
        => KnotVector.ScalarProcessor;

    public int Degree { get; }

    public BSplineKnotVector<T> KnotVector { get; }

    public int ControlPointsCount 
        => KnotVector.Size - Degree - 1;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal BSplineBasisSet(BSplineKnotVector<T> knotVector, int degree)
    {
        if (degree < 1 || knotVector.Size - degree - 1 < 4)
            throw new ArgumentOutOfRangeException(nameof(degree));

        Degree = degree;
        KnotVector = knotVector;
    }


    private Scalar<T> GetValue(int degree, int index, T parameterValue)
    {
        if (degree == 0)
            return KnotVector.BoxCar(index, parameterValue);

        var ti = KnotVector.GetKnotValue(index);
        var ti1 = KnotVector.GetKnotValue(index + 1);

        var tin = KnotVector.GetKnotValue(index + degree);
        var tin1 = KnotVector.GetKnotValue(index + degree + 1);

        var vi = GetValue(degree - 1, index, parameterValue);
        var vi1 = GetValue(degree - 1, index + 1, parameterValue);

        var a1 = vi * (parameterValue - ti);
        var a2 = tin - ti;

        var b1 = vi1 * (tin1 - parameterValue);
        var b2 = tin1 - ti1;

        var a = a1.IsZero() || a2.IsZero()
            ? ScalarProcessor.Zero
            : a1 / a2;
            
        var b = b1.IsZero() || b2.IsZero()
            ? ScalarProcessor.Zero
            : b1 / b2;

        return a + b;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> GetValue(int index, T parameterValue)
    {
        return GetValue(Degree, index, parameterValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> GetValue(int index, T parameterValue, T termScalar)
    {
        return termScalar * GetValue(index, parameterValue);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> GetValue(T parameterValue, params T[] termScalarsList)
    {
        return ScalarProcessor.Add(
            termScalarsList.Select(
                (scalar, index) => GetValue(index, parameterValue, scalar).ScalarValue
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<T> GetValues(T parameterValue)
    {
        return Enumerable
            .Range(0, Degree + 1)
            .Select(index => GetValue(index, parameterValue).ScalarValue)
            .ToArray();
    }
}