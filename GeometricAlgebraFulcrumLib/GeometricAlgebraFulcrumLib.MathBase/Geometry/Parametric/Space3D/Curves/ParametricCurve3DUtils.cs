using System.Diagnostics;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Maps.Space3D;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Borders;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Parametric.Space3D.Curves.Adaptive;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Parametric.Space3D.Curves.Mapped;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Parametric.Space3D.Curves.Samplers;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Parametric.Space3D.Frames;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Frames.Space3D;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.MathBase.Geometry.Parametric.Space3D.Curves
{
    public static class ParametricCurve3DUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double GetLength(this IArcLengthCurve3D curve, double parameterValue1, double parameterValue2)
        {
            var length1 = curve.ParameterToLength(parameterValue1);
            var length2 = curve.ParameterToLength(parameterValue2);

            return Math.Abs(length2 - length1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double GetLength(this IArcLengthCurve3D curve, IPair<double> parameterValuePair)
        {
            var length1 = curve.ParameterToLength(parameterValuePair.Item1);
            var length2 = curve.ParameterToLength(parameterValuePair.Item2);

            return Math.Abs(length2 - length1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double GetSignedLength(this IArcLengthCurve3D curve, double parameterValue1, double parameterValue2)
        {
            var length1 = curve.ParameterToLength(parameterValue1);
            var length2 = curve.ParameterToLength(parameterValue2);

            return length2 - length1;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double GetSignedLength(this IArcLengthCurve3D curve, IPair<double> parameterValuePair)
        {
            var length1 = curve.ParameterToLength(parameterValuePair.Item1);
            var length2 = curve.ParameterToLength(parameterValuePair.Item2);

            return length2 - length1;
        }

        
        public static Float64Quaternion CreateFrameToFrameRotationQuaternion(this ParametricCurveLocalFrame3D frame1, ParametricCurveLocalFrame3D frame2)
        {
            var q1 =
                frame1
                    .Tangent
                    .ToUnitVector()
                    .CreateVectorToVectorRotationQuaternion(frame2.Tangent.ToUnitVector());

            Debug.Assert(
                (q1.RotateVector(frame1.Tangent) - frame2.Tangent).ENormSquared().IsNearZero(1e-7)
            );

            var f1 =
                frame1.GetRotatedFrameUsingQuaternion(q1);

            var dot12 =
                q1
                    .RotateVector(frame1.Normal1)
                    .ToUnitVector()
                    .ESp(frame2.Normal1.ToUnitVector());

            var q2 =
                q1
                    .RotateVector(frame1.Normal1)
                    .ToUnitVector()
                    .CreateVectorToVectorRotationQuaternion(frame2.Normal1.ToUnitVector(), frame2.Tangent);

            var quaternion = q1.Concatenate(q2);

            var f2 =
                f1.GetRotatedFrameUsingQuaternion(q2);

            Debug.Assert(
                (quaternion.RotateVector(frame1.Tangent) - frame2.Tangent).ENormSquared().IsNearZero(1e-7)
            );

            Debug.Assert(
                (quaternion.RotateVector(frame1.Normal1) - frame2.Normal1).ENormSquared().IsNearZero(1e-7)
            );

            Debug.Assert(
                (quaternion.RotateVector(frame1.Normal2) - frame2.Normal2).ENormSquared().IsNearZero(1e-7)
            );

            return quaternion;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GrRouletteMappedFiniteParametricCurve3D GetRouletteMappedCurve(this IArcLengthCurve3D baseCurve, RouletteMap3D rouletteMap)
        {
            return new GrRouletteMappedFiniteParametricCurve3D(
                baseCurve,
                rouletteMap
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GrParametricRotatedNormalsCurve3D GetRotatedNormalsCurve(this IParametricCurve3D baseCurve, Func<double, Float64PlanarAngle> angleFunction)
        {
            return new GrParametricRotatedNormalsCurve3D(
                baseCurve,
                angleFunction
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GrC1ArcLengthRotatedNormalsCurve3D GetRotatedNormalsCurve(this IArcLengthCurve3D baseCurve, Func<double, Float64PlanarAngle> angleFunction)
        {
            return new GrC1ArcLengthRotatedNormalsCurve3D(
                baseCurve,
                angleFunction
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GrC1MappedParameterFiniteCurve3D GetMappedParameterCurve(this IParametricCurve3D baseCurve, Func<double, double> parameterMapping)
        {
            return new GrC1MappedParameterFiniteCurve3D(baseCurve, parameterMapping);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GrC1MappedParameterFiniteCurve3D GetMappedParameterCurveCosWave(this IParametricCurve3D baseCurve, int cycleCount = 1)
        {
            return new GrC1MappedParameterFiniteCurve3D(
                baseCurve, t =>
                    t.CosWave(
                        baseCurve.ParameterRange,
                        cycleCount
                    )
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GrC1MappedParameterFiniteCurve3D GetMappedParameterCurveTriangleWave(this IParametricCurve3D baseCurve, int cycleCount = 1)
        {
            return new GrC1MappedParameterFiniteCurve3D(
                baseCurve, t =>
                    t.TriangleWave(
                        baseCurve.ParameterRange,
                        cycleCount
                    )
            );
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector3D GetTangent(this IParametricCurve3D curve, double parameterValue)
        {
            return curve.GetDerivative1Point(parameterValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector3D GetUnitTangent(this IParametricCurve3D curve, double parameterValue)
        {
            return curve.GetDerivative1Point(parameterValue).ToUnitVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ParametricCurveLocalFrame3D> GetTangentData(this IParametricCurve3D curve, IEnumerable<double> parameterValueList)
        {
            return parameterValueList.Select(
                parameterValue =>
                    ParametricCurveLocalFrame3D.Create(
                        parameterValue,
                        curve.GetPoint(parameterValue),
                        curve.GetDerivative1Point(parameterValue)
                    )
            );
        }

        public static double ComputeCurveLength(this IEnumerable<ParametricCurveLocalFrame3D> framesList)
        {
            var arcLength = 0d;

            ParametricCurveLocalFrame3D frame1 = null;
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
        
        //public static Tuple2D[] ToPolyline(this ICurve2D curve, Float64Range1D curveParamRange, int pointsCount, double lengthError = 0.95)
        //{
        //    pointsCount = (Math.Max(pointsCount, 2) - 2).UpperPower2Limit();


        //}

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<double> GetParameterValues(this AdaptiveCurve3D curve)
        {
            return curve.Select(f => f.ParameterValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Float64Vector3D> GetPoints(this AdaptiveCurve3D curve)
        {
            return curve.Select(f => f.Point);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Float64Vector3D> GetPoints(this IParametricCurve3D curve, IEnumerable<double> tList)
        {
            return tList.Select(curve.GetPoint);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector3D[] GetPoints(this IParametricCurve3D curve, params double[] tList)
        {
            return tList.Select(curve.GetPoint).ToArray();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Float64Vector3D> GetTangents(this AdaptiveCurve3D curve)
        {
            return curve.Select(f => f.Tangent);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AffineFrame3D ToAffineFrame(this IParametricCurveLocalFrame3D frame)
        {
            return AffineFrame3D.Create(
                frame.Point,
                frame.Tangent,
                frame.Normal1,
                frame.Normal2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ParametricCurveLocalFrame3D ToLocalCurveFrame(this AffineFrame3D frame, double parameterValue)
        {
            return ParametricCurveLocalFrame3D.Create(
                parameterValue,
                frame.Origin,
                frame.Direction1,
                frame.Direction2,
                frame.Direction3
            );
        }

        /// <summary>
        /// https://en.wikipedia.org/wiki/Frenet%E2%80%93Serret_formulas
        /// </summary>
        /// <param name="curve"></param>
        /// <param name="parameterValue"></param>
        /// <returns></returns>
        public static ParametricCurveLocalFrame3D GetFrenetSerretFrame(this IParametricC2Curve3D curve, double parameterValue)
        {
            var origin = curve.GetPoint(parameterValue);

            var vDt1 = curve.GetDerivative1Point(parameterValue);
            var vDt2 = curve.GetDerivative2Point(parameterValue);
            //var vDt3 = GetSignalVectorDerivative3(t);

            var sDt1 = vDt1.ENorm();
            var sDt2 = vDt1.ESp(vDt2) / sDt1;
            //var sDt3 = (vDt2.VectorDot(vDt2) + vDt1.VectorDot(vDt3) - sDt2.Square()) / sDt1;

            var vDs1 = vDt1 / sDt1;
            var vDs2 = (sDt1 * vDt2 - sDt2 * vDt1) / sDt1.Cube();
            //var vDs3 = (sDt1.Square() * vDt3 - 3 * sDt1 * sDt2 * vDt2 + (3 * sDt2.Square() - sDt1 * sDt3) * vDt1) / sDt1.Power(5);

            var e1 = vDs1;
            var e2 = (vDs2 - vDs2.ProjectOnUnitVector(vDs1)).ToUnitVector();
            var e3 = e1.VectorUnitCross(e2);

            //var vDsMatrix = Matrix.Build.DenseOfArray(
            //    new [,]
            //    {
            //        { vDs1.X, vDs2.X, vDs3.X }, 
            //        { vDs1.Y, vDs2.Y, vDs3.Y }, 
            //        { vDs1.Z, vDs2.Z, vDs3.Z }
            //    }
            //);

            //var gramSchmidt = vDsMatrix.GramSchmidt();
            //var eMatrix = gramSchmidt.Q;
            ////var qDet = eMatrix.Determinant();

            //var e1 = new Tuple3D(eMatrix[0, 0], eMatrix[1, 0], eMatrix[2, 0]);
            //var e2 = new Tuple3D(eMatrix[0, 1], eMatrix[1, 1], eMatrix[2, 1]);
            //var e3 = e1.VectorUnitCross(e2);

            //if (eMatrix[2, 2].IsNearZero())
            //    throw new InvalidOperationException();

            ////Console.WriteLine(vDsMatrix);
            ////Console.WriteLine(gramSchmidt.Q);
            ////Console.WriteLine(qDet);
            ////Console.WriteLine(gramSchmidt.R);
            ////Console.WriteLine();

            return ParametricCurveLocalFrame3D.Create(
                parameterValue,
                origin,
                e1,
                e2,
                e3
            );
        }

        /// <summary>
        /// https://en.wikipedia.org/wiki/Frenet%E2%80%93Serret_formulas
        /// </summary>
        /// <param name="curve"></param>
        /// <param name="parameterValue"></param>
        /// <returns></returns>
        public static AffineFrame3D GetFrenetSerretAffineFrame(this IParametricC2Curve3D curve, double parameterValue)
        {
            var origin = curve.GetPoint(parameterValue);

            var vDt1 = curve.GetDerivative1Point(parameterValue);
            var vDt2 = curve.GetDerivative2Point(parameterValue);
            //var vDt3 = GetSignalVectorDerivative3(t);

            var sDt1 = vDt1.ENorm();
            var sDt2 = vDt1.ESp(vDt2) / sDt1;
            //var sDt3 = (vDt2.VectorDot(vDt2) + vDt1.VectorDot(vDt3) - sDt2.Square()) / sDt1;

            var vDs1 = vDt1 / sDt1;
            var vDs2 = (sDt1 * vDt2 - sDt2 * vDt1) / sDt1.Cube();
            //var vDs3 = (sDt1.Square() * vDt3 - 3 * sDt1 * sDt2 * vDt2 + (3 * sDt2.Square() - sDt1 * sDt3) * vDt1) / sDt1.Power(5);

            var e1 = vDs1;
            var e2 = (vDs2 - vDs2.ProjectOnUnitVector(vDs1)).ToUnitVector();
            var e3 = e1.VectorUnitCross(e2);

            //var vDsMatrix = Matrix.Build.DenseOfArray(
            //    new [,]
            //    {
            //        { vDs1.X, vDs2.X, vDs3.X }, 
            //        { vDs1.Y, vDs2.Y, vDs3.Y }, 
            //        { vDs1.Z, vDs2.Z, vDs3.Z }
            //    }
            //);

            //var gramSchmidt = vDsMatrix.GramSchmidt();
            //var eMatrix = gramSchmidt.Q;
            ////var qDet = eMatrix.Determinant();

            //var e1 = new Tuple3D(eMatrix[0, 0], eMatrix[1, 0], eMatrix[2, 0]);
            //var e2 = new Tuple3D(eMatrix[0, 1], eMatrix[1, 1], eMatrix[2, 1]);
            //var e3 = e1.VectorUnitCross(e2);

            //if (eMatrix[2, 2].IsNearZero())
            //    throw new InvalidOperationException();

            ////Console.WriteLine(vDsMatrix);
            ////Console.WriteLine(gramSchmidt.Q);
            ////Console.WriteLine(qDet);
            ////Console.WriteLine(gramSchmidt.R);
            ////Console.WriteLine();

            return AffineFrame3D.Create(origin, e1, e2, e3);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UniformParameterCurveSampler3D GetUniformParameterSampler(this IParametricCurve3D curve, Float64Range1D parameterRange, int sampleCount, bool isPeriodic)
        {
            return new UniformParameterCurveSampler3D(
                curve,
                parameterRange,
                sampleCount,
                isPeriodic
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AdaptiveCurveSampler3D GetAdaptiveSampler(this IParametricCurve3D curve, Float64Range1D parameterRange, AdaptiveCurveSamplingOptions3D samplingOptions, bool isPeriodic)
        {
            return new AdaptiveCurveSampler3D(
                curve,
                parameterRange,
                samplingOptions,
                isPeriodic
            );
        }
    }
}
