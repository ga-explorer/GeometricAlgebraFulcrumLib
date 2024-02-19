using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space1D.Scalars.CatmullRom;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.SpaceND.Curves.CatmullRom;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Matrices;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using MathNet.Numerics;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric;

/// <summary>
/// Implementation of the Centripetal Catmull-Rom spline on arbitrary dimensions
/// https://en.wikipedia.org/wiki/Centripetal_Catmull%E2%80%93Rom_spline
/// </summary>
public sealed class CatmullRomSplineSparse
{
    public sealed record SplineSegmentData(int KnotIndex1, int KnotIndex2, double ParameterValue);


    private readonly double[] _knotList;
    private readonly List<Float64Vector> _pointList;

    public CatmullRomSplineType CurveType { get; }

    public bool IsClosed { get; }

    public IEnumerable<Float64Vector> ControlPoints
        => _pointList;

    public int ControlPointCount
        => _pointList.Count;


    internal CatmullRomSplineSparse(IEnumerable<Float64Vector> inputPointList, CatmullRomSplineType curveType, bool isClosed)
    {
        CurveType = curveType;
        IsClosed = isClosed;
        _pointList = new List<Float64Vector>(inputPointList);

        Float64Vector endPoint1, endPoint2;

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
            endPoint1 = 2 * _pointList[0] - _pointList[1];
            endPoint2 = 2 * _pointList[^1] - _pointList[^2];
        }

        // Insert control points at both ends.
        _pointList.Insert(0, endPoint1);
        _pointList.Add(endPoint2);

        _knotList = new double[_pointList.Count];

        var total = 0d;
        for (var i = 1; i < _pointList.Count; i++)
        {
            var vector = _pointList[i] - _pointList[i - 1];
            var ds = vector.GetVectorNormSquared();

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


    public Pair<int> GetKnotIndexContaining(double parameterValue)
    {
        return GetKnotIndexContaining(parameterValue, 0, _knotList.Length - 1);
    }

    private Pair<int> GetKnotIndexContaining(double parameterValue, int index1, int index2)
    {
        while (true)
        {
            if (index2 == index1 || index2 == index1 + 1)
                return new Pair<int>(index1, index2);

            var indexMid = (index1 + index2) / 2;
            var tMid = _knotList[indexMid];

            if (parameterValue < tMid)
            {
                index2 = indexMid;
                continue;
            }

            if (parameterValue > tMid)
            {
                index1 = indexMid;
                continue;
            }

            return new Pair<int>(indexMid, indexMid);
        }
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<double> GetPointItemList(int itemIndex)
    {
        return _pointList.Select(p => p[itemIndex]).ToImmutableArray();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<int> GetPointItemIndexList()
    {
        return _pointList
            .SelectMany(p => p.Keys)
            .Distinct()
            .OrderBy(k => k);
    }

    public double GetPointItem(double parameterValue, int itemIndex)
    {
        // Handle edge cases
        if (parameterValue <= _knotList[0])
            return _pointList[0][itemIndex];

        if (parameterValue >= _knotList[^1])
            return _pointList[^1][itemIndex];

        var (index1, index2) =
            GetKnotIndexContaining(parameterValue, 0, _knotList.Length - 1);

        if (index1 == index2)
            return _pointList[index1][itemIndex];

        if (index1 == 0 && index2 == 1)
        {
            var t = (parameterValue - _knotList[0]) / (_knotList[1] - _knotList[0]);

            return t.Lerp(_pointList[0][itemIndex], _pointList[1][itemIndex]);
        }

        if (index1 == _knotList.Length - 2 && index2 == _knotList.Length - 1)
        {
            var t = (parameterValue - _knotList[^2]) / (_knotList[^1] - _knotList[^2]);

            return t.Lerp(_pointList[^2][itemIndex], _pointList[^1][itemIndex]);
        }

        // General case
        Debug.Assert(
            index2 == index1 + 1 &&
            index1 >= 1 &&
            index2 <= _knotList.Length - 2
        );

        var tQuad = _knotList.GetItemQuad(index1 - 1);
        var pQuad = _pointList.GetTupleItemQuad(index1 - 1, itemIndex);

        return parameterValue.GetCatmullRomValue(tQuad, pQuad);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetPointX(double parameterValue)
    {
        return GetPointItem(parameterValue, 0);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetPointY(double parameterValue)
    {
        return GetPointItem(parameterValue, 1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetPointZ(double parameterValue)
    {
        return GetPointItem(parameterValue, 2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetPointW(double parameterValue)
    {
        return GetPointItem(parameterValue, 3);
    }

    public Float64Vector GetPoint(double parameterValue)
    {
        // Handle edge cases
        if (parameterValue <= _knotList[0])
            return _pointList[0];

        if (parameterValue >= _knotList[^1])
            return _pointList[^1];

        var (index1, index2) =
            GetKnotIndexContaining(parameterValue, 0, _knotList.Length - 1);

        if (index1 == index2)
            return _pointList[index1];

        if (index1 == 0 && index2 == 1)
        {
            var t = (parameterValue - _knotList[0]) / (_knotList[1] - _knotList[0]);

            return t.Lerp(_pointList[0], _pointList[1]);
        }

        if (index1 == _knotList.Length - 2 && index2 == _knotList.Length - 1)
        {
            var t = (parameterValue - _knotList[^2]) / (_knotList[^1] - _knotList[^2]);

            return t.Lerp(_pointList[^2], _pointList[^1]);
        }

        // General case
        Debug.Assert(
            index2 == index1 + 1 &&
            index1 >= 1 &&
            index2 <= _knotList.Length - 2
        );

        var tQuad = _knotList.GetItemQuad(index1 - 1);
        var pQuad = _pointList.GetItemQuad(index1 - 1);

        return parameterValue.GetCatmullRomValue(tQuad, pQuad);
    }

    public Float64Vector GetTangent(double parameterValue)
    {
        if (parameterValue is <= 0d or >= 1d)
            return Float64Vector.Create(
                GetPointItemIndexList()
                    .Select(i =>
                        Differentiate.FirstDerivative(
                            t => GetPointItem(t, i),
                            parameterValue
                        )
                    )
            );

        var (index1, index2) =
            GetKnotIndexContaining(parameterValue, 0, _knotList.Length - 1);

        if (index1 == index2)
            return Float64Vector.Create(
                GetPointItemIndexList()
                    .Select(i =>
                        Differentiate.FirstDerivative(
                            t => GetPointItem(t, i),
                            parameterValue
                        )
                    )
            );

        Debug.Assert(
            index2 == index1 + 1 &&
            index1 >= 1 &&
            index2 <= _knotList.Length - 2
        );

        var tQuad = _knotList.GetItemQuad(index1 - 1);
        var pQuad = _pointList.GetItemQuad(index1 - 1);

        return parameterValue.GetCatmullRomDerivativeValue(tQuad, pQuad);
    }

    public Float64Vector GetSecondDerivative(double parameterValue)
    {
        if (parameterValue is <= 0d or >= 1d)
            return Float64Vector.Create(
                GetPointItemIndexList()
                    .Select(i =>
                        Differentiate.SecondDerivative(
                            t => GetPointItem(t, i),
                            parameterValue
                        )
                    )
            );

        var (index1, index2) =
            GetKnotIndexContaining(parameterValue, 0, _knotList.Length - 1);

        if (index1 == index2)
            return Float64Vector.Create(
                GetPointItemIndexList()
                    .Select(i =>
                        Differentiate.SecondDerivative(
                            t => GetPointItem(t, i),
                            parameterValue
                        )
                    )
            );

        Debug.Assert(
            index2 == index1 + 1 &&
            index1 >= 1 &&
            index2 <= _knotList.Length - 2
        );

        var tQuad = _knotList.GetItemQuad(index1 - 1);
        var pQuad = _pointList.GetItemQuad(index1 - 1);

        return parameterValue.GetCatmullRomDerivative2Value(tQuad, pQuad);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Vector GetUnitTangent(double parameterValue)
    {
        return GetTangent(parameterValue).ToUnitVector();
    }
}