using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space1D.Scalars.CatmullRom;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;
using MathNet.Numerics;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space3D.Curves.CatmullRom;

/// <summary>
/// Implementation of the Centripetal Catmull-Rom spline
/// https://en.wikipedia.org/wiki/Centripetal_Catmull%E2%80%93Rom_spline
/// </summary>
public sealed class CatmullRomSpline3D :
    IParametricC2Curve3D
{
    public sealed record SplineSegmentData(int KnotIndex1, int KnotIndex2, double ParameterValue);


    private readonly double[] _knotList;
    private readonly List<ILinFloat64Vector3D> _pointList;

    public CatmullRomSplineType CurveType { get; }

    public bool IsClosed { get; }

    public IEnumerable<ILinFloat64Vector3D> ControlPoints
        => _pointList;

    public int ControlPointCount
        => _pointList.Count;
        
    public Float64ScalarRange ParameterRange 
        => Float64ScalarRange.Infinite;


    internal CatmullRomSpline3D(IEnumerable<ILinFloat64Vector3D> inputPointList, CatmullRomSplineType curveType, bool isClosed)
    {
        CurveType = curveType;
        IsClosed = isClosed;
        _pointList = new List<ILinFloat64Vector3D>(inputPointList);

        ILinFloat64Vector3D endPoint1, endPoint2;

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
            endPoint1 = 2 * _pointList[0].ToLinVector3D() - _pointList[1];
            endPoint2 = 2 * _pointList[^1].ToLinVector3D() - _pointList[^2];
        }

        // Insert control points at both ends.
        _pointList.Insert(0, endPoint1);
        _pointList.Add(endPoint2);

        _knotList = new double[_pointList.Count];

        var total = 0d;
        for (var i = 1; i < _pointList.Count; i++)
        {
            var vector = _pointList[i].ToLinVector3D() - _pointList[i - 1];
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
    public bool IsValid()
    {
        return _pointList.All(p => p.IsValid());
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

    public double GetPointX(double parameterValue)
    {
        // Handle edge cases
        if (parameterValue <= _knotList[0])
            return _pointList[0].X;

        if (parameterValue >= _knotList[^1])
            return _pointList[^1].X;

        var (index1, index2) =
            GetKnotIndexContaining(parameterValue, 0, _knotList.Length - 1);

        if (index1 == index2)
            return _pointList[index1].X;

        if (index1 == 0 && index2 == 1)
        {
            var t = (parameterValue - _knotList[0]) / (_knotList[1] - _knotList[0]);

            return t.Lerp(_pointList[0].X, _pointList[1].X);
        }

        if (index1 == _knotList.Length - 2 && index2 == _knotList.Length - 1)
        {
            var t = (parameterValue - _knotList[^2]) / (_knotList[^1] - _knotList[^2]);

            return t.Lerp(_pointList[^2].X, _pointList[^1].X);
        }

        // General case
        Debug.Assert(
            index2 == index1 + 1 &&
            index1 >= 1 &&
            index2 <= _knotList.Length - 2
        );

        var tQuad = _knotList.GetItemQuad(index1 - 1);
        var xQuad = _pointList.GetTupleXQuad(index1 - 1);

        return parameterValue.GetCatmullRomValue(tQuad, xQuad);
    }

    public double GetPointY(double parameterValue)
    {
        // Handle edge cases
        if (parameterValue <= _knotList[0])
            return _pointList[0].Y;

        if (parameterValue >= _knotList[^1])
            return _pointList[^1].Y;

        var (index1, index2) =
            GetKnotIndexContaining(parameterValue, 0, _knotList.Length - 1);

        if (index1 == index2)
            return _pointList[index1].Y;

        if (index1 == 0 && index2 == 1)
        {
            var t = (parameterValue - _knotList[0]) / (_knotList[1] - _knotList[0]);

            return t.Lerp(_pointList[0].Y, _pointList[1].Y);
        }

        if (index1 == _knotList.Length - 2 && index2 == _knotList.Length - 1)
        {
            var t = (parameterValue - _knotList[^2]) / (_knotList[^1] - _knotList[^2]);

            return t.Lerp(_pointList[^2].Y, _pointList[^1].Y);
        }

        // General case
        Debug.Assert(
            index2 == index1 + 1 &&
            index1 >= 1 &&
            index2 <= _knotList.Length - 2
        );

        var tQuad = _knotList.GetItemQuad(index1 - 1);
        var yQuad = _pointList.GetTupleYQuad(index1 - 1);

        return parameterValue.GetCatmullRomValue(tQuad, yQuad);
    }

    public double GetPointZ(double parameterValue)
    {
        // Handle edge cases
        if (parameterValue <= _knotList[0])
            return _pointList[0].Z;

        if (parameterValue >= _knotList[^1])
            return _pointList[^1].Z;

        var (index1, index2) =
            GetKnotIndexContaining(parameterValue, 0, _knotList.Length - 1);

        if (index1 == index2)
            return _pointList[index1].Z;

        if (index1 == 0 && index2 == 1)
        {
            var t = (parameterValue - _knotList[0]) / (_knotList[1] - _knotList[0]);

            return t.Lerp(_pointList[0].Z, _pointList[1].Z);
        }

        if (index1 == _knotList.Length - 2 && index2 == _knotList.Length - 1)
        {
            var t = (parameterValue - _knotList[^2]) / (_knotList[^1] - _knotList[^2]);

            return t.Lerp(_pointList[^2].Z, _pointList[^1].Z);
        }

        // General case
        Debug.Assert(
            index2 == index1 + 1 &&
            index1 >= 1 &&
            index2 <= _knotList.Length - 2
        );

        var tQuad = _knotList.GetItemQuad(index1 - 1);
        var zQuad = _pointList.GetTupleZQuad(index1 - 1);

        return parameterValue.GetCatmullRomValue(tQuad, zQuad);
    }

    public LinFloat64Vector3D GetPoint(double parameterValue)
    {
        // Handle edge cases
        if (parameterValue <= _knotList[0])
            return _pointList[0].ToLinVector3D();

        if (parameterValue >= _knotList[^1])
            return _pointList[^1].ToLinVector3D();

        var (index1, index2) =
            GetKnotIndexContaining(parameterValue, 0, _knotList.Length - 1);

        if (index1 == index2)
            return _pointList[index1].ToLinVector3D();

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
        var xQuad = _pointList.GetTupleXQuad(index1 - 1);
        var yQuad = _pointList.GetTupleYQuad(index1 - 1);
        var zQuad = _pointList.GetTupleZQuad(index1 - 1);

        var x = parameterValue.GetCatmullRomValue(tQuad, xQuad);
        var y = parameterValue.GetCatmullRomValue(tQuad, yQuad);
        var z = parameterValue.GetCatmullRomValue(tQuad, zQuad);

        return LinFloat64Vector3D.Create(x, y, z);
    }

    public LinFloat64Vector3D GetDerivative1Point(double parameterValue)
    {
        if (parameterValue is <= 0d or >= 1d)
            return LinFloat64Vector3D.Create(Differentiate.FirstDerivative(GetPointX, parameterValue),
                Differentiate.FirstDerivative(GetPointY, parameterValue),
                Differentiate.FirstDerivative(GetPointZ, parameterValue));

        var (index1, index2) =
            GetKnotIndexContaining(parameterValue, 0, _knotList.Length - 1);

        if (index1 == index2)
            return LinFloat64Vector3D.Create(Differentiate.FirstDerivative(GetPointX, parameterValue),
                Differentiate.FirstDerivative(GetPointY, parameterValue),
                Differentiate.FirstDerivative(GetPointZ, parameterValue));

        Debug.Assert(
            index2 == index1 + 1 &&
            index1 >= 1 &&
            index2 <= _knotList.Length - 2
        );

        var tQuad = _knotList.GetItemQuad(index1 - 1);
        var xQuad = _pointList.GetTupleXQuad(index1 - 1);
        var yQuad = _pointList.GetTupleYQuad(index1 - 1);
        var zQuad = _pointList.GetTupleZQuad(index1 - 1);

        var x = parameterValue.GetCatmullRomDerivativeValue(tQuad, xQuad);
        var y = parameterValue.GetCatmullRomDerivativeValue(tQuad, yQuad);
        var z = parameterValue.GetCatmullRomDerivativeValue(tQuad, zQuad);

        return LinFloat64Vector3D.Create(x, y, z);
    }

    public LinFloat64Vector3D GetDerivative2Point(double parameterValue)
    {
        if (parameterValue is <= 0d or >= 1d)
            return LinFloat64Vector3D.Create(Differentiate.SecondDerivative(GetPointX, parameterValue),
                Differentiate.SecondDerivative(GetPointY, parameterValue),
                Differentiate.SecondDerivative(GetPointZ, parameterValue));

        var (index1, index2) =
            GetKnotIndexContaining(parameterValue, 0, _knotList.Length - 1);

        if (index1 == index2)
            return LinFloat64Vector3D.Create(Differentiate.SecondDerivative(GetPointX, parameterValue),
                Differentiate.SecondDerivative(GetPointY, parameterValue),
                Differentiate.SecondDerivative(GetPointZ, parameterValue));

        Debug.Assert(
            index2 == index1 + 1 &&
            index1 >= 1 &&
            index2 <= _knotList.Length - 2
        );

        var tQuad = _knotList.GetItemQuad(index1 - 1);
        var xQuad = _pointList.GetTupleXQuad(index1 - 1);
        var yQuad = _pointList.GetTupleYQuad(index1 - 1);
        var zQuad = _pointList.GetTupleZQuad(index1 - 1);

        var x = parameterValue.GetCatmullRomDerivative2Value(tQuad, xQuad);
        var y = parameterValue.GetCatmullRomDerivative2Value(tQuad, yQuad);
        var z = parameterValue.GetCatmullRomDerivative2Value(tQuad, zQuad);

        return LinFloat64Vector3D.Create(x, y, z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ParametricCurveLocalFrame3D GetFrame(double parameterValue)
    {
        return ParametricCurveLocalFrame3D.Create(
            parameterValue,
            GetPoint(parameterValue),
            GetDerivative1Point(parameterValue)
        );
    }
}