using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using GraphicsComposerLib.Geometry.ParametricShapes.Curves.Mapped;
using GraphicsComposerLib.Geometry.ParametricShapes.Curves.Sampled;
using GraphicsComposerLib.Geometry.Primitives;
using NumericalGeometryLib.BasicMath;
using NumericalGeometryLib.BasicMath.Frames.Space3D;
using NumericalGeometryLib.BasicMath.Maps.Space3D;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.Geometry.ParametricShapes.Curves
{
    public static class GrParametricCurveUtils
    {
        public static Tuple4D CreateFrameToFrameRotationQuaternion(this GrParametricCurveLocalFrame3D frame1, GrParametricCurveLocalFrame3D frame2)
        {
            var q1 =
                frame1
                    .Tangent
                    .ToUnitVector()
                    .CreateVectorToVectorRotationQuaternion(frame2.Tangent.ToUnitVector());
            
            Debug.Assert(
                (q1.QuaternionRotate(frame1.Tangent) - frame2.Tangent).GetLengthSquared().IsNearZero(1e-7)
            );

            var f1 = 
                frame1.GetRotatedFrameUsingQuaternion(q1);

            var dot12 =
                q1
                    .QuaternionRotate(frame1.Normal1)
                    .ToUnitVector()
                    .VectorDot(frame2.Normal1.ToUnitVector());

            var q2 =
                q1
                    .QuaternionRotate(frame1.Normal1)
                    .ToUnitVector()
                    .CreateVectorToVectorRotationQuaternion(frame2.Normal1.ToUnitVector(), frame2.Tangent);

            var quaternion = q1.QuaternionConcatenate(q2);

            var f2 = 
                f1.GetRotatedFrameUsingQuaternion(q2);

            Debug.Assert(
                (quaternion.QuaternionRotate(frame1.Tangent) - frame2.Tangent).GetLengthSquared().IsNearZero(1e-7)
            );

            Debug.Assert(
                (quaternion.QuaternionRotate(frame1.Normal1) - frame2.Normal1).GetLengthSquared().IsNearZero(1e-7)
            );

            Debug.Assert(
                (quaternion.QuaternionRotate(frame1.Normal2) - frame2.Normal2).GetLengthSquared().IsNearZero(1e-7)
            );

            return quaternion;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GrRouletteMappedFiniteParametricCurve3D GetRouletteMappedCurve(this IGraphicsC1ArcLengthCurve3D baseCurve, RouletteMap3D rouletteMap)
        {
            return new GrRouletteMappedFiniteParametricCurve3D(
                baseCurve,
                rouletteMap
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GrParametricRotatedNormalsCurve3D GetRotatedNormalsCurve(this IGraphicsC1ParametricCurve3D baseCurve, Func<double, PlanarAngle> angleFunction)
        {
            return new GrParametricRotatedNormalsCurve3D(
                baseCurve,
                angleFunction
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GrC1ArcLengthRotatedNormalsCurve3D GetRotatedNormalsCurve(this IGraphicsC1ArcLengthCurve3D baseCurve, Func<double, PlanarAngle> angleFunction)
        {
            return new GrC1ArcLengthRotatedNormalsCurve3D(
                baseCurve,
                angleFunction
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GrC1MappedParameterFiniteCurve3D GetMappedParameterCurve(this IGraphicsC1ParametricFiniteCurve3D baseCurve, Func<double, double> parameterMapping)
        {
            return new GrC1MappedParameterFiniteCurve3D(baseCurve, parameterMapping);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GrC1MappedParameterFiniteCurve3D GetMappedParameterCurveCosWave(this IGraphicsC1ParametricFiniteCurve3D baseCurve, int cycleCount = 1)
        {
            return new GrC1MappedParameterFiniteCurve3D(
                baseCurve, t=>
                    t.CosWave(
                        baseCurve.ParameterValueMin, 
                        baseCurve.ParameterValueMax, 
                        cycleCount
                    )
                );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GrC1MappedParameterFiniteCurve3D GetMappedParameterCurveTriangleWave(this IGraphicsC1ParametricFiniteCurve3D baseCurve, int cycleCount = 1)
        {
            return new GrC1MappedParameterFiniteCurve3D(
                baseCurve, t=>
                    t.TriangleWave(
                        baseCurve.ParameterValueMin, 
                        baseCurve.ParameterValueMax, 
                        cycleCount
                    )
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GrParametricCurveLocalFrame3D> GetTangentData(this IGraphicsC1ParametricCurve3D curve, IEnumerable<double> parameterValueList)
        {
            return parameterValueList.Select(
                parameterValue =>
                    GrParametricCurveLocalFrame3D.Create(
                        parameterValue,
                        curve.GetPoint(parameterValue),
                        curve.GetTangent(parameterValue)
                    )
            );
        }
        
        public static double ComputeCurveLength(this IEnumerable<GrParametricCurveLocalFrame3D> framesList)
        {
            var arcLength = 0d;
            
            GrParametricCurveLocalFrame3D frame1 = null;
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
        public static IEnumerable<Tuple2D> GetPointsAt(this IGraphicsParametricCurve2D curve, IEnumerable<double> tList)
        {
            return tList.Select(curve.GetPoint);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tuple2D[] GetPointsAt(this IGraphicsParametricCurve2D curve, params double[] tList)
        {
            return tList.Select(curve.GetPoint).ToArray();
        }

        //public static Tuple2D[] ToPolyline(this ICurve2D curve, IBoundingBox1D curveParamRange, int pointsCount, double lengthError = 0.95)
        //{
        //    pointsCount = (Math.Max(pointsCount, 2) - 2).UpperPower2Limit();


        //}

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Tuple3D> GetPoints(this GrParametricCurveTree3D curve)
        {
            return curve.Select(f => f.Point);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Tuple3D> GetPoints(this IGraphicsC1ParametricCurve3D curve, IEnumerable<double> tList)
        {
            return tList.Select(curve.GetPoint);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tuple3D[] GetPoints(this IGraphicsC1ParametricCurve3D curve, params double[] tList)
        {
            return tList.Select(curve.GetPoint).ToArray();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AffineFrame3D ToAffineFrame(this IGraphicsCurveLocalFrame3D frame)
        {
            return AffineFrame3D.Create(
                frame.Point, 
                frame.Tangent, 
                frame.Normal1, 
                frame.Normal2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GrParametricCurveLocalFrame3D ToLocalCurveFrame(this AffineFrame3D frame, double parameterValue)
        {
            return GrParametricCurveLocalFrame3D.Create(
                parameterValue,
                frame.Origin, 
                frame.Direction2, 
                frame.Direction3, 
                frame.Direction1
            );
        }

        /// <summary>
        /// https://en.wikipedia.org/wiki/Frenet%E2%80%93Serret_formulas
        /// </summary>
        /// <param name="curve"></param>
        /// <param name="parameterValue"></param>
        /// <returns></returns>
        public static GrParametricCurveLocalFrame3D GetFrenetSerretFrame(this IGraphicsC2ParametricCurve3D curve, double parameterValue)
        {
            var origin = curve.GetPoint(parameterValue);

            var vDt1 = curve.GetTangent(parameterValue);
            var vDt2 = curve.GetSecondDerivative(parameterValue);
            //var vDt3 = GetSignalVectorDerivative3(t);

            var sDt1 = vDt1.GetLength();
            var sDt2 = vDt1.VectorDot(vDt2) / sDt1;
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

            return GrParametricCurveLocalFrame3D.Create(
                parameterValue,
                origin, 
                e2, 
                e3, 
                e1
            );
        }

        /// <summary>
        /// https://en.wikipedia.org/wiki/Frenet%E2%80%93Serret_formulas
        /// </summary>
        /// <param name="curve"></param>
        /// <param name="parameterValue"></param>
        /// <returns></returns>
        public static AffineFrame3D GetFrenetSerretAffineFrame(this IGraphicsC2ParametricCurve3D curve, double parameterValue)
        {
            var origin = curve.GetPoint(parameterValue);

            var vDt1 = curve.GetTangent(parameterValue);
            var vDt2 = curve.GetSecondDerivative(parameterValue);
            //var vDt3 = GetSignalVectorDerivative3(t);

            var sDt1 = vDt1.GetLength();
            var sDt2 = vDt1.VectorDot(vDt2) / sDt1;
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
    }
}
