using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Frames.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.LinearMaps.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Calculus.Curves;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.AffineMaps.Space3D;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Quaternions.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Scalars.Float64.Angles;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors3D.Float64.Mapped;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors3D.Float64;

public static class Float64ArcLengthPath3DUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double GetLength(this Float64ArcLengthPath3D curve, double parameterValue1, double parameterValue2)
    {
        var length1 = curve.TimeToLength(parameterValue1);
        var length2 = curve.TimeToLength(parameterValue2);

        return Math.Abs(length2 - length1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double GetLength(this Float64ArcLengthPath3D curve, IPair<double> parameterValuePair)
    {
        var length1 = curve.TimeToLength(parameterValuePair.Item1);
        var length2 = curve.TimeToLength(parameterValuePair.Item2);

        return Math.Abs(length2 - length1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double GetSignedLength(this Float64ArcLengthPath3D curve, double parameterValue1, double parameterValue2)
    {
        var length1 = curve.TimeToLength(parameterValue1);
        var length2 = curve.TimeToLength(parameterValue2);

        return length2 - length1;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double GetSignedLength(this Float64ArcLengthPath3D curve, IPair<double> parameterValuePair)
    {
        var length1 = curve.TimeToLength(parameterValuePair.Item1);
        var length2 = curve.TimeToLength(parameterValuePair.Item2);

        return length2 - length1;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64RouletteMappedPath3D GetRouletteMappedCurve(this Float64ArcLengthPath3D baseCurve, Float64RouletteAffineMap3D rouletteMap)
    {
        return new Float64RouletteMappedPath3D(
            baseCurve,
            rouletteMap
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64RotatedNormalsArcLengthPath3D GetRotatedNormalsCurve(this Float64ArcLengthPath3D baseCurve, Func<double, LinFloat64Angle> angleFunction)
    {
        return new Float64RotatedNormalsArcLengthPath3D(
            baseCurve,
            Float64ScalarSignal.CreateComputed(
                baseCurve.TimeRange, 
                baseCurve.IsPeriodic,
                t => angleFunction(t).RadiansValue
            ).RadiansToPolarAngle()
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64RotatedNormalsArcLengthPath3D GetRotatedNormalsCurve(this Float64ArcLengthPath3D baseCurve, LinFloat64PolarAngle angle)
    {
        return new Float64RotatedNormalsArcLengthPath3D(
            baseCurve,
            LinFloat64PolarAngleTimeSignal.CreateConstant(
                baseCurve.TimeRange,
                baseCurve.IsPeriodic,
                angle
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64RotatedNormalsPath3D GetRotatedNormalsCurve(this Float64ArcLengthPath3D baseCurve, LinFloat64PolarAngleTimeSignal angleFunction)
    {
        return new Float64RotatedNormalsPath3D(
            baseCurve,
            angleFunction
        );
    }
    
    public static LinFloat64Vector3D GetArcLengthDerivative1Point(this Float64Path3D path, double t)
    {
        if (path is Float64DifferentialPath3D dCurve)
            return dCurve.GetArcLengthDerivative1Value(t);

        var vDt1 = path.GetDerivative1Value(t);

        var sDt1 = path.GetGetDerivative1NormValue(t);

        if (sDt1.IsNearZero())
            return LinFloat64Vector3D.Zero;

        var vDs1 = vDt1 / sDt1;

        return vDs1;
    }

    public static LinFloat64Vector3D GetArcLengthDerivative2Point(this Float64Path3D path, double t)
    {
        if (path is Float64DifferentialPath3D dCurve)
            return dCurve.GetArcLengthDerivative2Value(t);

        var vDt1 = path.GetDerivative1Value(t);
        var vDt2 = path.GetDerivative2Value(t);

        var sDt1 = path.GetGetDerivative1NormValue(t);

        if (sDt1.IsNearZero())
            return LinFloat64Vector3D.Zero;

        var sDt2 = vDt1.VectorESp(vDt2) / sDt1;

        var vDs1 = vDt1 / sDt1;
        var vDs2 = (sDt1 * vDt2 - sDt2 * vDt1) / sDt1.Cube();

        return vDs2 - vDs2.ProjectOnUnitVector(vDs1);
    }

    public static LinFloat64Vector3D GetArcLengthDerivative3Point(this Float64Path3D path, double t)
    {
        var vDt1 = path.GetDerivative1Value(t);
        var vDt2 = path.GetDerivative2Value(t);
        var vDt3 =
            (path.GetDerivative2Value(t + 1e-7) -
             path.GetDerivative2Value(t - 1e-7)) / (2 * 1e-7);

        var sDt1 = path.GetGetDerivative1NormValue(t);

        if (sDt1.IsNearZero())
            return LinFloat64Vector3D.Zero;

        var sDt2 = vDt1.VectorESp(vDt2) / sDt1;
        var sDt3 = (vDt2.VectorESp(vDt2) + vDt1.VectorESp(vDt3) - sDt2.Square()) / sDt1;

        var vDs1 = vDt1 / sDt1;
        var vDs2 = (sDt1 * vDt2 - sDt2 * vDt1) / sDt1.Cube();
        var vDs3 = (sDt1.Square() * vDt3 - 3 * sDt1 * sDt2 * vDt2 + (3 * sDt2.Square() - sDt1 * sDt3) * vDt1) / sDt1.Power(5);

        return vDs3 - vDs3.ProjectOnUnitVector(vDs2) - vDs3.ProjectOnUnitVector(vDs1);
    }

    public static DifferentialCurveFrame3D GetFrenetFrame2Vectors(this Float64Path3D path, double t, bool orthogonalFrame = false)
    {
        if (path is Float64DifferentialPath3D dCurve)
            return dCurve.GetFrenetFrame2Vectors(t, orthogonalFrame);

        var origin = path.GetValue(t);

        var vDt1 = path.GetDerivative1Value(t);
        var vDt2 = path.GetDerivative2Value(t);

        var sDt1 = path.GetGetDerivative1NormValue(t);

        if (sDt1.IsNearZero())
            return DifferentialCurveFrame3D.Create(
                t,
                origin,
                LinFloat64Vector3D.E1,
                LinFloat64Vector3D.E2,
                LinFloat64Vector3D.E3
            );

        var sDt2 = vDt1.VectorESp(vDt2) / sDt1;

        var vDs1 = vDt1 / sDt1;
        var vDs2 = (sDt1 * vDt2 - sDt2 * vDt1) / sDt1.Cube();

        if (vDs2.IsNearZero())
            return DifferentialCurveFrame3D.Create(
                t,
                origin,
                vDs1,
                vDs1.GetUnitNormalPair()
            );

        if (!orthogonalFrame)
            return DifferentialCurveFrame3D.Create(
                t,
                origin,
                vDs1,
                vDs2,
                vDs1.VectorCross(vDs2)
            );

        var u1 = vDs1;
        var u2 = vDs2 - vDs2.ProjectOnVector(u1);

        return DifferentialCurveFrame3D.Create(
            t,
            origin,
            u1,
            u2,
            u1.VectorCross(u2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricQuaternion GetFrenetFrameRotationQuaternionsCurve(this Float64Path3D path)
    {
        if (path is Float64DifferentialPath3D dCurve)
            return dCurve.GetFrenetFrameRotationQuaternionsCurve();

        return ComputedParametricQuaternion.Create(time =>
            {
                var frame = path.GetFrenetFrame2Vectors(time);

                return frame
                    .Direction1
                    .GetOrthogonalizingRotation(frame.Direction2)
                    .GetQuaternion();
            }
        );
    }

    /// <summary>
    /// https://en.wikipedia.org/wiki/Frenet%E2%80%93Serret_formulas
    /// </summary>
    /// <param name="path"></param>
    /// <param name="t"></param>
    /// <returns></returns>
    public static Float64Path3DLocalFrame GetFrenetSerretFrame(this Float64Path3D path, double t)
    {
        var origin = path.GetValue(t);

        var vDt1 = path.GetDerivative1Value(t);
        var vDt2 = path.GetDerivative2Value(t);

        var sDt1 = vDt1.VectorENorm();

        if (sDt1.IsNearZero())
            return Float64Path3DLocalFrame.Create(
                t,
                origin,
                LinFloat64Vector3D.E1,
                LinFloat64Vector3D.E2,
                LinFloat64Vector3D.E3
            );

        var sDt2 = vDt1.VectorESp(vDt2) / sDt1;

        var vDs1 = vDt1 / sDt1;
        var vDs2 = (sDt1 * vDt2 - sDt2 * vDt1) / sDt1.Cube();

        if (vDs2.IsNearZero())
            return Float64Path3DLocalFrame.Create(
                t,
                origin,
                vDs1,
                vDs1.GetUnitNormalPair()
            );

        var e1 = vDs1;
        var e2 = (vDs2 - vDs2.ProjectOnUnitVector(vDs1)).ToUnitLinVector3D();
        var e3 = e1.VectorUnitCross(e2);

        return Float64Path3DLocalFrame.Create(
            t,
            origin,
            e1,
            e2,
            e3
        );
    }

    /// <summary>
    /// https://en.wikipedia.org/wiki/Frenet%E2%80%93Serret_formulas
    /// </summary>
    /// <param name="path"></param>
    /// <param name="t"></param>
    /// <returns></returns>
    public static LinFloat64AffineFrame3D GetFrenetSerretAffineFrame(this Float64Path3D path, double t)
    {
        var origin = path.GetValue(t);

        var vDt1 = path.GetDerivative1Value(t);
        var vDt2 = path.GetDerivative2Value(t);
        //var vDt3 = GetSignalVectorDerivative3(t);

        var sDt1 = vDt1.VectorENorm();

        if (sDt1.IsNearZero())
            return LinFloat64AffineFrame3D.Create(
                origin,
                LinFloat64Vector3D.E1,
                LinFloat64Vector3D.E2,
                LinFloat64Vector3D.E3
            );

        var sDt2 = vDt1.VectorESp(vDt2) / sDt1;
        //var sDt3 = (vDt2.VectorDot(vDt2) + vDt1.VectorDot(vDt3) - sDt2.Square()) / sDt1;

        var vDs1 = vDt1 / sDt1;
        var vDs2 = (sDt1 * vDt2 - sDt2 * vDt1) / sDt1.Cube();
        //var vDs3 = (sDt1.Square() * vDt3 - 3 * sDt1 * sDt2 * vDt2 + (3 * sDt2.Square() - sDt1 * sDt3) * vDt1) / sDt1.Power(5);

        var e1 = vDs1;
        var e2 = (vDs2 - vDs2.ProjectOnUnitVector(vDs1)).ToUnitLinVector3D();
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

        return LinFloat64AffineFrame3D.Create(origin, e1, e2, e3);
    }

}