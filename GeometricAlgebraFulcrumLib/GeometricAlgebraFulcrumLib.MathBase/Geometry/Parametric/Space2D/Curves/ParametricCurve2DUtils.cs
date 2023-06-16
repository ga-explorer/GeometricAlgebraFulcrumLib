using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Maps.Space2D;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Parametric.Space2D.Curves.Adaptive;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Parametric.Space2D.Curves.Mapped;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Parametric.Space2D.Frames;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Frames.Space2D;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.MathBase.Geometry.Parametric.Space2D.Curves
{
    public static class ParametricCurve2DUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GrRouletteMappedFiniteParametricCurve2D GetRouletteMappedCurve(this IArcLengthCurve2D baseCurve, RouletteMap2D rouletteMap)
        {
            return new GrRouletteMappedFiniteParametricCurve2D(
                baseCurve,
                rouletteMap
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GrC1MappedParameterFiniteCurve2D GetMappedParameterCurve(this IParametricCurve2D baseCurve, Func<double, double> parameterMapping)
        {
            return new GrC1MappedParameterFiniteCurve2D(baseCurve, parameterMapping);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GrC1MappedParameterFiniteCurve2D GetMappedParameterCurveCosWave(this IParametricCurve2D baseCurve, int cycleCount = 1)
        {
            return new GrC1MappedParameterFiniteCurve2D(
                baseCurve, t =>
                    t.CosWave(
                        baseCurve.ParameterRange,
                        cycleCount
                    )
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GrC1MappedParameterFiniteCurve2D GetMappedParameterCurveTriangleWave(this IParametricCurve2D baseCurve, int cycleCount = 1)
        {
            return new GrC1MappedParameterFiniteCurve2D(
                baseCurve, t =>
                    t.TriangleWave(
                        baseCurve.ParameterRange,
                        cycleCount
                    )
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector2D GetTangent(this IParametricCurve2D curve, double parameterValue)
        {
            return curve.GetDerivative1Point(parameterValue);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ParametricCurveLocalFrame2D> GetTangentData(this IParametricCurve2D curve, IEnumerable<double> parameterValueList)
        {
            return parameterValueList.Select(
                parameterValue =>
                    ParametricCurveLocalFrame2D.Create(
                        parameterValue,
                        curve.GetPoint(parameterValue),
                        curve.GetDerivative1Point(parameterValue)
                    )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector2D GetUnitTangent(this IParametricCurve2D curve, double parameterValue)
        {
            return curve.GetDerivative1Point(parameterValue).ToUnitVector();
        }
        
        public static double ComputeCurveLength(this IEnumerable<ParametricCurveLocalFrame2D> framesList)
        {
            var arcLength = 0d;

            ParametricCurveLocalFrame2D frame1 = null;
            var firstFrame = true;
            foreach (var frame2 in framesList)
            {
                if (firstFrame)
                {
                    frame1 = frame2;
                    firstFrame = false;
                    continue;
                }

                arcLength += frame2.Point.GetDistanceToPoint(frame1.Point);

                frame1 = frame2;
            }

            return arcLength;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Float64Vector2D> GetPointsAt(this IParametricCurve2D curve, IEnumerable<double> tList)
        {
            return tList.Select(curve.GetPoint);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector2D[] GetPointsAt(this IParametricCurve2D curve, params double[] tList)
        {
            return tList.Select(curve.GetPoint).ToArray();
        }

        //public static Tuple2D[] ToPolyline(this ICurve2D curve, Float64Range1D curveParamRange, int pointsCount, double lengthError = 0.95)
        //{
        //    pointsCount = (Math.Max(pointsCount, 2) - 2).UpperPower2Limit();


        //}

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<double> GetParameterValues(this AdaptiveCurve2D curve)
        {
            return curve.Select(f => f.ParameterValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Float64Vector2D> GetPoints(this AdaptiveCurve2D curve)
        {
            return curve.Select(f => f.Point);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Float64Vector2D> GetPoints(this IParametricCurve2D curve, IEnumerable<double> tList)
        {
            return tList.Select(curve.GetPoint);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector2D[] GetPoints(this IParametricCurve2D curve, params double[] tList)
        {
            return tList.Select(curve.GetPoint).ToArray();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Float64Vector2D> GetTangents(this AdaptiveCurve2D curve)
        {
            return curve.Select(f => f.Tangent);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AffineFrame2D ToAffineFrame(this IParametricCurveLocalFrame2D frame)
        {
            return AffineFrame2D.Create(
                frame.Point,
                frame.Tangent,
                frame.Normal
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ParametricCurveLocalFrame2D ToLocalCurveFrame(this AffineFrame2D frame, double parameterValue)
        {
            return ParametricCurveLocalFrame2D.Create(
                parameterValue,
                frame.Origin,
                frame.UDirection
            );
        }

        /// <summary>
        /// https://en.wikipedia.org/wiki/Frenet%E2%80%93Serret_formulas
        /// </summary>
        /// <param name="curve"></param>
        /// <param name="parameterValue"></param>
        /// <returns></returns>
        public static ParametricCurveLocalFrame2D GetFrenetSerretFrame(this IParametricC2Curve2D curve, double parameterValue)
        {
            var origin = curve.GetPoint(parameterValue);

            var vDt1 = curve.GetDerivative1Point(parameterValue);
            var sDt1 = vDt1.ENorm();
            var vDs1 = vDt1 / sDt1;
            
            return ParametricCurveLocalFrame2D.Create(
                parameterValue,
                origin,
                vDs1
            );
        }

        /// <summary>
        /// https://en.wikipedia.org/wiki/Frenet%E2%80%93Serret_formulas
        /// </summary>
        /// <param name="curve"></param>
        /// <param name="parameterValue"></param>
        /// <returns></returns>
        public static AffineFrame2D GetFrenetSerretAffineFrame(this IParametricC2Curve2D curve, double parameterValue)
        {
            var origin = curve.GetPoint(parameterValue);

            var vDt1 = curve.GetDerivative1Point(parameterValue);
            var vDt2 = curve.GetDerivative2Point(parameterValue);

            var sDt1 = vDt1.ENorm();
            var sDt2 = vDt1.ESp(vDt2) / sDt1;

            var vDs1 = vDt1 / sDt1;
            var vDs2 = (sDt1 * vDt2 - sDt2 * vDt1) / sDt1.Cube();

            var e1 = vDs1;
            var e2 = (vDs2 - vDs2.ProjectOnUnitVector(vDs1)).ToUnitVector();
            
            return AffineFrame2D.Create(origin, e1, e2);
        }
    }
}
