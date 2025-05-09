using System.Collections;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Core.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Core.Polynomials.Float64.BSplineCurveBasis;

/// <summary>
/// This class represents a knot vector for a spline curve
/// </summary>
public sealed class BSplineKnotVector :
    IReadOnlyList<double>
{
    /// <summary>
    /// Create an empty knot vector to be filled later
    /// </summary>
    /// <returns></returns>
    public static BSplineKnotVector Create()
    {
        var knotVector = new BSplineKnotVector();

        return knotVector;
    }

    /// <summary>
    /// Create a knot vector and fill its knots automatically.
    /// The knot values uniformly span the range [t0, t1] with count equal to knotCount
    /// The outer knots with values t0, t1 will have multiplicity equal to outerMultiplicity
    /// The inner knots with values between t0, t1 exclusive will have multiplicity equal to innerMultiplicity
    /// </summary>
    /// <param name="t0"></param>
    /// <param name="t1"></param>
    /// <param name="knotCount"></param>
    /// <param name="outerMultiplicity"></param>
    /// <param name="innerMultiplicity"></param>
    /// <returns></returns>
    public static BSplineKnotVector Create(double t0, double t1, int knotCount, int outerMultiplicity, int innerMultiplicity)
    {
        var knotVector = new BSplineKnotVector();

        var tList = t0.GetLinearRange(t1, knotCount).ToArray();

        var i = 0;
        var n = tList.Length - 1;
        foreach (var t in tList)
        {
            var multiplicity =
                i == 0 || i == n
                    ? outerMultiplicity
                    : innerMultiplicity;

            knotVector.AppendKnot(t, multiplicity);

            i++;
        }

        return knotVector;
    }

    /// <summary>
    /// Create a knot vector and fill its knots automatically.
    /// The knot values uniformly span the range [t0, t1] with count equal to knotCount
    /// All multiplicities are equal to 1
    /// </summary>
    /// <param name="t0"></param>
    /// <param name="t1"></param>
    /// <param name="knotCount"></param>
    /// <returns></returns>
    public static BSplineKnotVector CreateUniform(double t0, double t1, int knotCount)
    {
        var knotVector = new BSplineKnotVector();

        var tList = t0.GetLinearRange(t1, knotCount).ToArray();

        foreach (var t in tList)
            knotVector.AppendKnot(t);

        return knotVector;
    }

    /// <summary>
    /// Create a knot vector and fill its knots automatically.
    /// The knot values uniformly span the range [t0, t1] with count equal to knotCount
    /// The outer knots with values t0, t1 will have multiplicity equal to degree + 1
    /// The inner knots with values between t0, t1 exclusive will have multiplicity equal to 1
    /// </summary>
    /// <param name="t0"></param>
    /// <param name="t1"></param>
    /// <param name="knotCount"></param>
    /// <param name="degree"></param>
    /// <returns></returns>
    public static BSplineKnotVector CreateSimpleClamped(double t0, double t1, int knotCount, int degree)
    {
        var knotVector = new BSplineKnotVector();

        knotVector.AppendKnot(t0, degree + 1);

        var tList = t0.GetLinearRange(t1, knotCount).ToArray();

        for (var i = 1; i < tList.Length - 1; i++)
            knotVector.AppendKnot(tList[i]);

        knotVector.AppendKnot(t1, degree + 1);

        return knotVector;
    }


    private readonly List<BSplineKnot> _knotList
        = new List<BSplineKnot>();

    /// <summary>
    /// The number of values in this vector included repeated knot values
    /// </summary>
    public int Count
        => _knotList.Count == 0
            ? 0 : _knotList[^1].Index2 + 1;

    /// <summary>
    /// The number of knots in this vector
    /// </summary>
    public int KnotCount
        => _knotList.Count;

    /// <summary>
    /// The knots in this vector
    /// </summary>
    public IEnumerable<BSplineKnot> Knots
        => _knotList;

    /// <summary>
    /// The max allowable b-spline degree based on the number of values in this vector
    /// </summary>
    public int MaxDegree
        => Count - 1 >> 1;

    /// <summary>
    /// Get a value of this vector given its index
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public double this[int index]
        => _knotList.First(knot => knot.ContainsIndex(index)).Value;

    /// <summary>
    /// The multiplicity of the first knot in this vector
    /// </summary>
    public int FirstMultiplicity
        => _knotList[0].Multiplicity;

    /// <summary>
    /// The value of the first knot in this vector
    /// </summary>
    public double FirstValue
        => _knotList[0].Value;

    /// <summary>
    /// The multiplicity of the last knot in this vector
    /// </summary>
    public int LastMultiplicity
        => _knotList[^1].Multiplicity;

    /// <summary>
    /// The value of the last knot in this vector
    /// </summary>
    public double LastValue
        => _knotList[^1].Value;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private BSplineKnotVector()
    {
    }


    /// <summary>
    /// The number of basis spline functions allowed on this vector
    /// </summary>
    /// <param name="degree"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int GetBasisCount(int degree)
    {
        return degree >= 0 && degree <= MaxDegree
            ? Count - degree - 1
            : 0;
    }

    /// <summary>
    /// Get a knot in this vector given its index
    /// </summary>
    /// <param name="knotIndex"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public BSplineKnot GetKnot(int knotIndex)
    {
        return _knotList[knotIndex];
    }

    /// <summary>
    /// Set a knot in this vector given its index
    /// </summary>
    /// <param name="knotIndex"></param>
    /// <param name="knot"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void SetKnot(int knotIndex, BSplineKnot knot)
    {
        _knotList[knotIndex] = knot
                               ?? throw new ArgumentNullException(nameof(knot));
    }

    /// <summary>
    /// Get the sequence of (knot value, knot multiplicity) in this vector
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<Tuple<double, int>> GetKnotValueMultiplicityList()
    {
        return _knotList.Select(k => new Tuple<double, int>(k.Value, k.Multiplicity));
    }

    /// <summary>
    /// Append a new knot value at the end of this vector
    /// If the value is less than the last knot value, it's an error
    /// If the value is equal to the last value, the multiplicity of the last knot is increased
    /// </summary>
    /// <param name="value"></param>
    /// <param name="multiplicity"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public BSplineKnotVector AppendKnot(double value, int multiplicity = 1)
    {
        if (multiplicity < 1)
            throw new ArgumentOutOfRangeException(nameof(multiplicity));

        if (_knotList.Count > 0)
        {
            var lastValue = LastValue;

            if (value < lastValue)
                throw new InvalidOperationException();

            if (value == lastValue)
            {
                var lastKnot = _knotList[^1];
                _knotList[^1] = new BSplineKnot(
                    lastKnot.Index1,
                    value,
                    lastKnot.Multiplicity + multiplicity
                );

                return this;
            }
        }

        _knotList.Add(
            new BSplineKnot(Count, value, multiplicity)
        );

        return this;
    }

    /// <summary>
    /// Get the knot values at index1 and index2
    /// </summary>
    /// <param name="index1"></param>
    /// <param name="index2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Pair<double> GetKnotValueRange(int index1, int index2)
    {
        return new Pair<double>(this[index1], this[index2]);
    }

    /// <summary>
    /// Get the difference between knot values at index1 and index2
    /// </summary>
    /// <param name="index1"></param>
    /// <param name="index2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetKnotValueDifference(int index1, int index2)
    {
        return this[index2] - this[index1];
    }

    /// <summary>
    /// Compute a basis spline of degree 0 of given index (the Boxcar function of given index)
    /// at the given value
    /// https://en.wikipedia.org/wiki/Boxcar_function
    /// </summary>
    /// <param name="index"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double Boxcar(int index, double value)
    {
        if (index < 0 || index >= Count - 1)
            //return 0d;
            throw new IndexOutOfRangeException(nameof(index));

        for (var knotIndex = 0; knotIndex < _knotList.Count; knotIndex++)
        {
            var knot = _knotList[knotIndex];
            var n = knot.Multiplicity;

            if (index >= n)
            {
                index -= n;
                continue;
            }

            if (index < n - 1 || knotIndex == _knotList.Count - 1)
                return 0d;

            var value1 = knot.Value;
            var value2 = _knotList[knotIndex + 1].Value;

            return value < value1 || value >= value2 ? 0d : 1d;
        }

        return 0d;
    }

    /// <summary>
    /// Multiply the multiplicities of the knot vectors by given scaling factor
    /// and return the result as a new knot vector
    /// </summary>
    /// <param name="scalingFactor"></param>
    /// <returns></returns>
    public BSplineKnotVector ScaleMultiplicity(int scalingFactor)
    {
        if (scalingFactor < 0)
            throw new ArgumentOutOfRangeException(nameof(scalingFactor));

        var knotVector = new BSplineKnotVector();

        foreach (var knot in _knotList)
            knotVector.AppendKnot(
                knot.Value,
                knot.Multiplicity * scalingFactor
            );

        return knotVector;
    }

    /// <summary>
    /// Set the multiplicities of all knots to one and return the result as a new knot vector
    /// </summary>
    /// <returns></returns>
    public BSplineKnotVector RemoveMultiplicity()
    {
        var knotVector = new BSplineKnotVector();

        foreach (var knot in _knotList)
            knotVector.AppendKnot(knot.Value);

        return knotVector;
    }

    /// <summary>
    /// Create a B-spline basis set using this knot vector
    /// </summary>
    /// <param name="degree"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public BSplineBasisSet CreateBSplineBasisSet(int degree)
    {
        if (degree < 0 || degree > MaxDegree)
            throw new ArgumentOutOfRangeException(nameof(degree));

        return BSplineBasisSet.Create(this, degree);
    }


    public IEnumerator<double> GetEnumerator()
    {
        foreach (var knot in _knotList)
        {
            var n = knot.Multiplicity;

            while (n > 0)
            {
                yield return knot.Value;

                n--;
            }
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}