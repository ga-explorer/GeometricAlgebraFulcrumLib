using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using MathNet.Numerics;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64.Basic;

/// <summary>
/// Implementation of the Centripetal Catmull-Rom spline
/// https://en.wikipedia.org/wiki/Centripetal_Catmull%E2%80%93Rom_spline
/// </summary>
public sealed class Float64CatmullRomSplinePath2D :
    Float64Path2D
{
    public sealed record SplineSegmentData(int KnotIndex1, int KnotIndex2, double ParameterValue);


    private readonly double[] _knotList;
    private readonly List<ILinFloat64Vector2D> _pointList;

    public CatmullRomSplineType SplineType { get; }

    public bool IsClosed { get; }

    public IEnumerable<ILinFloat64Vector2D> ControlPoints
        => _pointList;

    public int ControlPointCount
        => _pointList.Count;


    internal Float64CatmullRomSplinePath2D(bool isPeriodic, IEnumerable<ILinFloat64Vector2D> inputPointList, CatmullRomSplineType curveType, bool isClosed)
        : base(Float64ScalarRange.ZeroToOne, isPeriodic)
    {
        SplineType = curveType;
        IsClosed = isClosed;
        _pointList = new List<ILinFloat64Vector2D>(inputPointList);

        ILinFloat64Vector2D endPoint1, endPoint2;

        if (isClosed)
        {
            // Make sure the first and last points are the same.
            if (_pointList[0].GetDistanceSquaredToPoint(_pointList[^1]).IsNearZero())
                _pointList.RemoveAt(_pointList.Count);

            _pointList.Add(_pointList[0]);

            // Use the second and second from last points as control points.
            endPoint1 = _pointList[^2];
            endPoint2 = _pointList[1];
        }
        else
        {
            // Extend the curve by two control points
            endPoint1 = 2 * _pointList[0].ToLinVector2D() - _pointList[1];
            endPoint2 = 2 * _pointList[^1].ToLinVector2D() - _pointList[^2];
        }

        // Insert control points at both ends.
        _pointList.Insert(0, endPoint1);
        _pointList.Add(endPoint2);

        _knotList = new double[_pointList.Count];

        var total = 0d;
        for (var i = 1; i < _pointList.Count; i++)
        {
            var vector = _pointList[i].ToLinVector2D() - _pointList[i - 1];
            var ds = vector.VectorENormSquared();

            var power =
                curveType == CatmullRomSplineType.Centripetal
                    ? 0.25d : 0.5d;

            total += Math.Pow(ds, power);

            _knotList[i] = total;
        }

        var tMin = _knotList[1];
        var tMax = _knotList[^2];
        var tRangeInv = 1d / (tMax - tMin);

        for (var i = 0; i < _knotList.Length; i++)
            _knotList[i] = (_knotList[i] - tMin) * tRangeInv;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsValid()
    {
        return _pointList.All(p => p.IsValid());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Pair<int> GetKnotIndexContaining(double t)
    {
        return GetKnotIndexContaining(t, 0, _knotList.Length - 1);
    }

    private Pair<int> GetKnotIndexContaining(double t, int index1, int index2)
    {
        while (true)
        {
            if (index2 == index1 || index2 == index1 + 1)
                return new Pair<int>(index1, index2);

            var indexMid = (index1 + index2) / 2;
            var tMid = _knotList[indexMid];

            if (t < tMid)
            {
                index2 = indexMid;
                continue;
            }

            if (t > tMid)
            {
                index1 = indexMid;
                continue;
            }

            return new Pair<int>(indexMid, indexMid);
        }
    }

    /// <summary>
    /// This assumes the x-values are strictly monotonically increasing with t.
    /// Typically used for approximation of functions in 1 parameter y(x)
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Pair<int> GetKnotIndexContainingX(double x)
    {
        return GetKnotIndexContainingX(x, 0, _knotList.Length - 1);
    }

    /// <summary>
    /// This assumes the x-values are strictly monotonically increasing with t.
    /// Typically used for approximation of functions in 1 parameter y(x)
    /// </summary>
    /// <param name="x"></param>
    /// <param name="index1"></param>
    /// <param name="index2"></param>
    /// <returns></returns>
    private Pair<int> GetKnotIndexContainingX(double x, int index1, int index2)
    {
        while (true)
        {
            if (index2 == index1 || index2 == index1 + 1)
                return new Pair<int>(index1, index2);

            var indexMid = (index1 + index2) / 2;
            var xMid = _pointList[indexMid].X;

            if (x < xMid)
            {
                index2 = indexMid;
                continue;
            }

            if (x > xMid)
            {
                index1 = indexMid;
                continue;
            }

            return new Pair<int>(indexMid, indexMid);
        }
    }

    /// <summary>
    /// This assumes the x-values are strictly monotonically increasing with t.
    /// Typically used for approximation of functions in 1 parameter y(x)
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    public Tuple<bool, double> TryGetParameterValueFromX(double x)
    {
        // Handle edge cases
        if (x <= _pointList[0].X)
            return new Tuple<bool, double>(true, _knotList[0]);

        if (x >= _pointList[^1].X)
            return new Tuple<bool, double>(true, _knotList[^1]);

        var (index1, index2) =
            GetKnotIndexContainingX(x, 0, _knotList.Length - 1);

        if (index1 == index2)
            return new Tuple<bool, double>(true, _knotList[index1]);

        if (index1 == 0 && index2 == 1)
        {
            var tx = (x - _pointList[0].X) / (_pointList[1].X - _pointList[0].X);

            return new Tuple<bool, double>(
                true,
                Float64Utils.Lerp(tx, _knotList[0], _knotList[1])
            );
        }

        if (index1 == _knotList.Length - 2 && index2 == _knotList.Length - 1)
        {
            var tx = (x - _pointList[^2].X) / (_pointList[^1].X - _pointList[^2].X);

            return new Tuple<bool, double>(
                true,
                Float64Utils.Lerp(tx, _knotList[^2], _knotList[^1])
            );
        }

        // General Case
        Debug.Assert(
            index2 == index1 + 1 &&
            index1 >= 1 &&
            index2 <= _knotList.Length - 2
        );

        var tQuad = _knotList.GetItemQuad(index1 - 1);
        var xQuad = _pointList.GetTupleXQuad(index1 - 1);

        return x.TryGetCatmullRomParameterValue(tQuad, xQuad);
    }

    /// <summary>
    /// This assumes the x-values are strictly monotonically increasing with t.
    /// Typically used for approximation of functions in 1 parameter y(x)
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    /// <exception cref="KeyNotFoundException"></exception>
    public double GetPointYFromX(double x)
    {
        // Handle edge cases
        if (x <= _pointList[0].X)
            return _pointList[0].Y;

        if (x >= _pointList[^1].X)
            return _pointList[^1].Y;

        var (index1, index2) =
            GetKnotIndexContainingX(x, 0, _knotList.Length - 1);

        if (index1 == index2)
            return _pointList[index1].Y;

        if (index1 == 0 && index2 == 1)
        {
            var tx = (x - _pointList[0].X) / (_pointList[1].X - _pointList[0].X);

            return Float64Utils.Lerp(tx, _pointList[0].Y, _pointList[1].Y);
        }

        if (index1 == _knotList.Length - 2 && index2 == _knotList.Length - 1)
        {
            var tx = (x - _pointList[^2].X) / (_pointList[^1].X - _pointList[^2].X);

            return Float64Utils.Lerp(tx, _pointList[^2].Y, _pointList[^1].Y);
        }

        // General case
        Debug.Assert(
            index2 == index1 + 1 &&
            index1 >= 1 &&
            index2 <= _knotList.Length - 2
        );

        var tQuad = _knotList.GetItemQuad(index1 - 1);
        var xQuad = _pointList.GetTupleXQuad(index1 - 1);
        var yQuad = _pointList.GetTupleYQuad(index1 - 1);

        var (tFound, t) =
            x.TryGetCatmullRomParameterValue(tQuad, xQuad);

        if (!tFound)
            throw new KeyNotFoundException();

        return t.GetCatmullRomValue(tQuad, yQuad);
    }

    /// <summary>
    /// This assumes the x-values are strictly monotonically increasing with t.
    /// Typically used for approximation of functions in 1 parameter y(x)
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    /// <exception cref="KeyNotFoundException"></exception>
    public LinFloat64Vector2D GetPointFromX(double x)
    {
        // Handle edge cases
        if (x <= _pointList[0].X)
            return _pointList[0].ToLinVector2D();

        if (x >= _pointList[^1].X)
            return _pointList[^1].ToLinVector2D();

        var (index1, index2) =
            GetKnotIndexContainingX(x, 0, _knotList.Length - 1);

        if (index1 == index2)
            return _pointList[index1].ToLinVector2D();

        if (index1 == 0 && index2 == 1)
        {
            var tx = (x - _pointList[0].X) / (_pointList[1].X - _pointList[0].X);

            return tx.Lerp(_pointList[0], _pointList[1]);
        }

        if (index1 == _knotList.Length - 2 && index2 == _knotList.Length - 1)
        {
            var tx = (x - _pointList[^2].X) / (_pointList[^1].X - _pointList[^2].X);

            return tx.Lerp(_pointList[^2], _pointList[^1]);
        }

        // General case
        Debug.Assert(
            index2 == index1 + 1 &&
            index1 >= 1 &&
            index2 <= _knotList.Length - 2
        );

        var tQuad = _knotList.GetItemQuad(index1 - 1);
        var xQuad = _pointList.GetTupleXQuad(index1 - 1);
        var yQuad = _pointList.GetTupleYQuad(index1 - 1);

        var (tFound, t) =
            x.TryGetCatmullRomParameterValue(tQuad, xQuad);

        if (!tFound)
            throw new KeyNotFoundException();

        var xh = t.GetCatmullRomValue(tQuad, xQuad);
        var yh = t.GetCatmullRomValue(tQuad, yQuad);

        return LinFloat64Vector2D.Create((Float64Scalar)xh, (Float64Scalar)yh);
    }

    public double GetPointX(double t)
    {
        // Handle edge cases
        if (t <= _knotList[0])
            return _pointList[0].X;

        if (t >= _knotList[^1])
            return _pointList[^1].X;

        var (index1, index2) =
            GetKnotIndexContaining(t, 0, _knotList.Length - 1);

        if (index1 == index2)
            return _pointList[index1].X;

        if (index1 == 0 && index2 == 1)
        {
            var t1 = (t - _knotList[0]) / (_knotList[1] - _knotList[0]);

            return t1.Lerp(_pointList[0].X, _pointList[1].X);
        }

        if (index1 == _knotList.Length - 2 && index2 == _knotList.Length - 1)
        {
            var t1 = (t - _knotList[^2]) / (_knotList[^1] - _knotList[^2]);

            return t1.Lerp(_pointList[^2].X, _pointList[^1].X);
        }

        // General case
        Debug.Assert(
            index2 == index1 + 1 &&
            index1 >= 1 &&
            index2 <= _knotList.Length - 2
        );

        var tQuad = _knotList.GetItemQuad(index1 - 1);
        var xQuad = _pointList.GetTupleXQuad(index1 - 1);

        return t.GetCatmullRomValue(tQuad, xQuad);
    }

    public double GetPointY(double t)
    {
        // Handle edge cases
        if (t <= _knotList[0])
            return _pointList[0].Y;

        if (t >= _knotList[^1])
            return _pointList[^1].Y;

        var (index1, index2) =
            GetKnotIndexContaining(t, 0, _knotList.Length - 1);

        if (index1 == index2)
            return _pointList[index1].Y;

        if (index1 == 0 && index2 == 1)
        {
            var t1 = (t - _knotList[0]) / (_knotList[1] - _knotList[0]);

            return t1.Lerp(_pointList[0].Y, _pointList[1].Y);
        }

        if (index1 == _knotList.Length - 2 && index2 == _knotList.Length - 1)
        {
            var t1 = (t - _knotList[^2]) / (_knotList[^1] - _knotList[^2]);

            return t1.Lerp(_pointList[^2].Y, _pointList[^1].Y);
        }

        // General case
        Debug.Assert(
            index2 == index1 + 1 &&
            index1 >= 1 &&
            index2 <= _knotList.Length - 2
        );

        var tQuad = _knotList.GetItemQuad(index1 - 1);
        var yQuad = _pointList.GetTupleYQuad(index1 - 1);

        return t.GetCatmullRomValue(tQuad, yQuad);
    }

    public override LinFloat64Vector2D GetValue(double t)
    {
        // Handle edge cases
        if (t <= _knotList[0])
            return _pointList[0].ToLinVector2D();

        if (t >= _knotList[^1])
            return _pointList[^1].ToLinVector2D();

        var (index1, index2) =
            GetKnotIndexContaining(t, 0, _knotList.Length - 1);

        if (index1 == index2)
            return _pointList[index1].ToLinVector2D();

        if (index1 == 0 && index2 == 1)
        {
            var t1 = (t - _knotList[0]) / (_knotList[1] - _knotList[0]);

            return t1.Lerp(_pointList[0], _pointList[1]);
        }

        if (index1 == _knotList.Length - 2 && index2 == _knotList.Length - 1)
        {
            var t1 = (t - _knotList[^2]) / (_knotList[^1] - _knotList[^2]);

            return t1.Lerp(_pointList[^2], _pointList[^1]);
        }

        // General case
        Debug.Assert(
            index2 == index1 + 1 &&
            index1 >= 1 &&
            index2 <= _knotList.Length - 2
        );

        var tQuad = _knotList.GetItemQuad(index1 - 1);
        var xQuad = _pointList.GetTupleXQuad(index1 - 1);
        var yQuad = _pointList.GetTupleYQuad(index1 - 1);

        var x = t.GetCatmullRomValue(tQuad, xQuad);
        var y = t.GetCatmullRomValue(tQuad, yQuad);

        return LinFloat64Vector2D.Create((Float64Scalar)x, (Float64Scalar)y);
    }

    public override Float64Path2D ToFinitePath()
    {
        throw new NotImplementedException();
    }

    public override Float64Path2D ToPeriodicPath()
    {
        throw new NotImplementedException();
    }

    public override LinFloat64Vector2D GetDerivative1Value(double t)
    {
        if (t is <= 0d or >= 1d)
            return LinFloat64Vector2D.Create((Float64Scalar)Differentiate.FirstDerivative(GetPointX, t),
                (Float64Scalar)Differentiate.FirstDerivative(GetPointY, t));

        var (index1, index2) =
            GetKnotIndexContaining(t, 0, _knotList.Length - 1);

        if (index1 == index2)
            return LinFloat64Vector2D.Create((Float64Scalar)Differentiate.FirstDerivative(GetPointX, t),
                (Float64Scalar)Differentiate.FirstDerivative(GetPointY, t));

        Debug.Assert(
            index2 == index1 + 1 &&
            index1 >= 1 &&
            index2 <= _knotList.Length - 2
        );

        var tQuad = _knotList.GetItemQuad(index1 - 1);
        var xQuad = _pointList.GetTupleXQuad(index1 - 1);
        var yQuad = _pointList.GetTupleYQuad(index1 - 1);

        var x = t.GetCatmullRomDerivativeValue(tQuad, xQuad);
        var y = t.GetCatmullRomDerivativeValue(tQuad, yQuad);

        return LinFloat64Vector2D.Create((Float64Scalar)x, (Float64Scalar)y);
    }

    public override LinFloat64Vector2D GetDerivative2Value(double t)
    {
        if (t is <= 0d or >= 1d)
            return LinFloat64Vector2D.Create((Float64Scalar)Differentiate.SecondDerivative(GetPointX, t),
                (Float64Scalar)Differentiate.SecondDerivative(GetPointY, t));

        var (index1, index2) =
            GetKnotIndexContaining(t, 0, _knotList.Length - 1);

        if (index1 == index2)
            return LinFloat64Vector2D.Create((Float64Scalar)Differentiate.SecondDerivative(GetPointX, t),
                (Float64Scalar)Differentiate.SecondDerivative(GetPointY, t));

        Debug.Assert(
            index2 == index1 + 1 &&
            index1 >= 1 &&
            index2 <= _knotList.Length - 2
        );

        var tQuad = _knotList.GetItemQuad(index1 - 1);
        var xQuad = _pointList.GetTupleXQuad(index1 - 1);
        var yQuad = _pointList.GetTupleYQuad(index1 - 1);

        var x = t.GetCatmullRomDerivative2Value(tQuad, xQuad);
        var y = t.GetCatmullRomDerivative2Value(tQuad, yQuad);

        return LinFloat64Vector2D.Create((Float64Scalar)x, (Float64Scalar)y);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector2D GetUnitTangent(double t)
    {
        return GetDerivative1Value(t).ToUnitLinVector2D();
    }

}