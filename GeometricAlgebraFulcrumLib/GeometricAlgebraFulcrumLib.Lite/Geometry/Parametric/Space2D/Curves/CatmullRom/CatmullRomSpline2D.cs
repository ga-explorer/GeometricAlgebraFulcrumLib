using System.Diagnostics;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space1D.Scalars.CatmullRom;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using MathNet.Numerics;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space2D.Curves.CatmullRom
{
    /// <summary>
    /// Implementation of the Centripetal Catmull-Rom spline
    /// https://en.wikipedia.org/wiki/Centripetal_Catmull%E2%80%93Rom_spline
    /// </summary>
    public sealed class CatmullRomSpline2D :
        IParametricC2Curve2D
    {
        public sealed record SplineSegmentData(int KnotIndex1, int KnotIndex2, double ParameterValue);


        private readonly double[] _knotList;
        private readonly List<IFloat64Vector2D> _pointList;

        public CatmullRomSplineType SplineType { get; }

        public bool IsClosed { get; }

        public IEnumerable<IFloat64Vector2D> ControlPoints
            => _pointList;

        public int ControlPointCount
            => _pointList.Count;
        
        public Float64ScalarRange ParameterRange 
            => Float64ScalarRange.Infinite;


        internal CatmullRomSpline2D(IEnumerable<IFloat64Vector2D> inputPointList, CatmullRomSplineType curveType, bool isClosed)
        {
            SplineType = curveType;
            IsClosed = isClosed;
            _pointList = new List<IFloat64Vector2D>(inputPointList);

            IFloat64Vector2D endPoint1, endPoint2;

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
                endPoint1 = 2 * _pointList[0].ToVector2D() - _pointList[1];
                endPoint2 = 2 * _pointList[^1].ToVector2D() - _pointList[^2];
            }

            // Insert control points at both ends.
            _pointList.Insert(0, endPoint1);
            _pointList.Add(endPoint2);

            _knotList = new double[_pointList.Count];

            var total = 0d;
            for (var i = 1; i < _pointList.Count; i++)
            {
                var vector = _pointList[i].ToVector2D() - _pointList[i - 1];
                var ds = vector.ENormSquared();

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


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
                    tx.Lerp(_knotList[0], _knotList[1])
                );
            }

            if (index1 == _knotList.Length - 2 && index2 == _knotList.Length - 1)
            {
                var tx = (x - _pointList[^2].X) / (_pointList[^1].X - _pointList[^2].X);

                return new Tuple<bool, double>(
                    true,
                    tx.Lerp(_knotList[^2], _knotList[^1])
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

                return tx.Lerp(_pointList[0].Y, _pointList[1].Y);
            }

            if (index1 == _knotList.Length - 2 && index2 == _knotList.Length - 1)
            {
                var tx = (x - _pointList[^2].X) / (_pointList[^1].X - _pointList[^2].X);

                return tx.Lerp(_pointList[^2].Y, _pointList[^1].Y);
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
        public Float64Vector2D GetPointFromX(double x)
        {
            // Handle edge cases
            if (x <= _pointList[0].X)
                return _pointList[0].ToVector2D();

            if (x >= _pointList[^1].X)
                return _pointList[^1].ToVector2D();

            var (index1, index2) =
                GetKnotIndexContainingX(x, 0, _knotList.Length - 1);

            if (index1 == index2)
                return _pointList[index1].ToVector2D();

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

            return Float64Vector2D.Create((Float64Scalar)xh, (Float64Scalar)yh);
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

        public Float64Vector2D GetPoint(double parameterValue)
        {
            // Handle edge cases
            if (parameterValue <= _knotList[0])
                return _pointList[0].ToVector2D();

            if (parameterValue >= _knotList[^1])
                return _pointList[^1].ToVector2D();

            var (index1, index2) =
                GetKnotIndexContaining(parameterValue, 0, _knotList.Length - 1);

            if (index1 == index2)
                return _pointList[index1].ToVector2D();

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

            var x = parameterValue.GetCatmullRomValue(tQuad, xQuad);
            var y = parameterValue.GetCatmullRomValue(tQuad, yQuad);

            return Float64Vector2D.Create((Float64Scalar)x, (Float64Scalar)y);
        }
        
        public Float64Vector2D GetDerivative1Point(double parameterValue)
        {
            if (parameterValue is <= 0d or >= 1d)
                return Float64Vector2D.Create((Float64Scalar)Differentiate.FirstDerivative(GetPointX, parameterValue),
                    (Float64Scalar)Differentiate.FirstDerivative(GetPointY, parameterValue));

            var (index1, index2) =
                GetKnotIndexContaining(parameterValue, 0, _knotList.Length - 1);

            if (index1 == index2)
                return Float64Vector2D.Create((Float64Scalar)Differentiate.FirstDerivative(GetPointX, parameterValue),
                    (Float64Scalar)Differentiate.FirstDerivative(GetPointY, parameterValue));

            Debug.Assert(
                index2 == index1 + 1 &&
                index1 >= 1 &&
                index2 <= _knotList.Length - 2
            );

            var tQuad = _knotList.GetItemQuad(index1 - 1);
            var xQuad = _pointList.GetTupleXQuad(index1 - 1);
            var yQuad = _pointList.GetTupleYQuad(index1 - 1);

            var x = parameterValue.GetCatmullRomDerivativeValue(tQuad, xQuad);
            var y = parameterValue.GetCatmullRomDerivativeValue(tQuad, yQuad);

            return Float64Vector2D.Create((Float64Scalar)x, (Float64Scalar)y);
        }

        public Float64Vector2D GetDerivative2Point(double parameterValue)
        {
            if (parameterValue is <= 0d or >= 1d)
                return Float64Vector2D.Create((Float64Scalar)Differentiate.SecondDerivative(GetPointX, parameterValue),
                    (Float64Scalar)Differentiate.SecondDerivative(GetPointY, parameterValue));

            var (index1, index2) =
                GetKnotIndexContaining(parameterValue, 0, _knotList.Length - 1);

            if (index1 == index2)
                return Float64Vector2D.Create((Float64Scalar)Differentiate.SecondDerivative(GetPointX, parameterValue),
                    (Float64Scalar)Differentiate.SecondDerivative(GetPointY, parameterValue));

            Debug.Assert(
                index2 == index1 + 1 &&
                index1 >= 1 &&
                index2 <= _knotList.Length - 2
            );

            var tQuad = _knotList.GetItemQuad(index1 - 1);
            var xQuad = _pointList.GetTupleXQuad(index1 - 1);
            var yQuad = _pointList.GetTupleYQuad(index1 - 1);

            var x = parameterValue.GetCatmullRomDerivative2Value(tQuad, xQuad);
            var y = parameterValue.GetCatmullRomDerivative2Value(tQuad, yQuad);

            return Float64Vector2D.Create((Float64Scalar)x, (Float64Scalar)y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector2D GetUnitTangent(double parameterValue)
        {
            return GetDerivative1Point(parameterValue).ToUnitVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ParametricCurveLocalFrame2D GetFrame(double parameterValue)
        {
            return ParametricCurveLocalFrame2D.Create(
                parameterValue,
                GetPoint(parameterValue),
                GetDerivative1Point(parameterValue)
            );
        }
    }
}