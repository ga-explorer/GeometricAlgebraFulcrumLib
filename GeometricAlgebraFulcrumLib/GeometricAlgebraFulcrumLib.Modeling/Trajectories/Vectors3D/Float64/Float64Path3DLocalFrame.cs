using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Frames.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Matrices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using SixLabors.ImageSharp;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors3D.Float64;

public sealed record Float64Path3DLocalFrame :
    IFloat64Path3DLocalFrame
{
    public static Float64Path3DLocalFrame CreateFromAffineFrame(double parameterValue, LinFloat64AffineFrame3D affineFrame)
    {
        return new Float64Path3DLocalFrame(
            parameterValue,
            affineFrame.Origin,
            affineFrame.Direction1,
            affineFrame.Direction2,
            affineFrame.Direction3
        );
    }


    /// <summary>
    /// Create a local frame based on the tangent only
    /// </summary>
    /// <param name="parameterValue"></param>
    /// <param name="point"></param>
    /// <param name="tangentVector"></param>
    /// <returns></returns>
    public static Float64Path3DLocalFrame Create(double parameterValue, ILinFloat64Vector3D point, ILinFloat64Vector3D tangentVector)
    {
        var tangent =
            tangentVector.ToUnitLinVector3D();

        var (normal1, normal2) =
            tangent.GetUnitNormalPair();

        return new Float64Path3DLocalFrame(
            parameterValue,
            point,
            tangent,
            normal1,
            normal2
        );
    }

    public static Float64Path3DLocalFrame Create(double parameterValue, ILinFloat64Vector3D point, ILinFloat64Vector3D tangent, IPair<ILinFloat64Vector3D> normalPair)
    {
        return new Float64Path3DLocalFrame(
            parameterValue,
            point.ToLinVector3D(),
            tangent.ToLinVector3D(),
            normalPair.Item1.ToLinVector3D(),
            normalPair.Item2.ToLinVector3D()
        );
    }

    public static Float64Path3DLocalFrame Create(double parameterValue, ILinFloat64Vector3D point, ILinFloat64Vector3D tangent, ILinFloat64Vector3D normal1, ILinFloat64Vector3D normal2)
    {
        return new Float64Path3DLocalFrame(
            parameterValue,
            point.ToLinVector3D(),
            tangent.ToLinVector3D(),
            normal1.ToLinVector3D(),
            normal2.ToLinVector3D()
        );
    }


    /// <summary>
    /// The curve parameter value at the given curve point
    /// </summary>
    public double TimeValue { get; }

    public int Index { get; internal set; } = -1;

    /// <summary>
    /// A point on the curve
    /// </summary>
    public LinFloat64Vector3D Point { get; }

    public int VSpaceDimensions
        => 3;

    public Float64Scalar Item1
        => Point.X;

    public Float64Scalar Item2
        => Point.Y;

    public Float64Scalar Item3
        => Point.Z;

    public Float64Scalar X
        => Point.X;

    public Float64Scalar Y
        => Point.Y;

    public Float64Scalar Z
        => Point.Z;

    public Color Color { get; set; }

    /// <summary>
    /// The first unit vector orthogonal to the tangent and normal at the given curve point
    /// </summary>
    public LinFloat64Normal3D Normal1 { get; }
        = new LinFloat64Normal3D();

    /// <summary>
    /// The second unit vector orthogonal to the tangent and normal at the given curve point
    /// </summary>
    public LinFloat64Normal3D Normal2 { get; }
        = new LinFloat64Normal3D();

    /// <summary>
    /// The tangent unit vector to the curve at the given curve point
    /// </summary>
    public LinFloat64Vector3D Tangent { get; }


    private Float64Path3DLocalFrame(double parameterValue, ILinFloat64Vector3D point, ILinFloat64Vector3D tangent, ILinFloat64Vector3D normal1, ILinFloat64Vector3D normal2)
    {
        TimeValue = parameterValue;
        Point = point.ToLinVector3D();
        Tangent = tangent.ToLinVector3D();
        Normal1.Set(normal1);
        Normal2.Set(normal2);

        //Debug.Assert(IsValid());
    }


    public bool IsValid()
    {
        var length1 = Normal1.VectorENormSquared();
        var length2 = Normal2.VectorENormSquared();
        var length3 = Tangent.VectorENormSquared();

        var cosAngle1 = Normal1.VectorESp(Normal2);
        var cosAngle2 = Normal2.VectorESp(Tangent);
        var cosAngle3 = Tangent.VectorESp(Normal1);

        var isValid =
            !double.IsNaN(TimeValue) &&
            Point.IsValid() &&
            Tangent.IsValid() &&
            Normal1.IsValid() &&
            Normal2.IsValid() &&
            length1.IsNearEqual(1) &&
            length2.IsNearEqual(1) &&
            length3.IsNearEqual(1) &&
            cosAngle1.IsNearZero() &&
            cosAngle2.IsNearZero() &&
            cosAngle3.IsNearZero();

        return isValid;
    }

    internal Float64Path3DLocalFrame UpdateNormals(ILinFloat64Vector3D normal1, ILinFloat64Vector3D normal2)
    {
        Normal1.Set(normal1);
        Normal2.Set(normal2);

        Debug.Assert(IsValid());

        return this;
    }

    public Float64Path3DLocalFrame SetFrenetNormals(ILinFloat64Vector3D secondDerivativeVector)
    {
        var normal1 = secondDerivativeVector.VectorCross(Tangent).ToUnitLinVector3D();
        var normal2 = Tangent.VectorCross(normal1);

        Normal1.Set(normal1);
        Normal2.Set(normal2);

        Debug.Assert(IsValid());

        return this;
    }

    public Float64Path3DLocalFrame SetSimpleRotationNormals(Float64Path3DLocalFrame sourceFrame)
    {
        var (normal1, normal2) =
            sourceFrame.RotateNormalsByTangent(Tangent);

        Normal1.Set(normal1);
        Normal2.Set(normal2);

        //Debug.Assert(IsValid());

        return this;
    }

    /// <summary>
    /// See paper "Computation of Rotation Minimizing Frames"
    /// https://www.microsoft.com/en-us/research/wp-content/uploads/2016/12/Computation-of-rotation-minimizing-frames.pdf
    /// </summary>
    /// <param name="sourceFrame"></param>
    /// <returns></returns>
    public Float64Path3DLocalFrame SetMinimizedRotationNormals(Float64Path3DLocalFrame sourceFrame)
    {
        var planeNormal1 =
            Point - sourceFrame.Point;

        var (normal1L, normal2L, tangentL) =
            planeNormal1.ReflectVectorsOnVector(
                sourceFrame.Normal1,
                sourceFrame.Normal2,
                sourceFrame.Tangent
            );


        var planeNormal2 =
            Tangent - tangentL;

        var (normal1, normal2) =
            planeNormal2.ReflectVectorsOnVector(
                normal1L,
                normal2L
            );

        Normal1.Set(normal1);
        Normal2.Set(normal2);

        Debug.Assert(IsValid());

        return this;
    }


    public double GetMaxDirectionAngleWithFrame(Float64Path3DLocalFrame frame2)
    {
        var maxAngle = 0d;

        var angle = Normal1.GetAngle(frame2.Normal1);
        if (maxAngle < angle.RadiansValue) maxAngle = angle.RadiansValue;

        angle = Normal2.GetAngle(frame2.Normal2);
        if (maxAngle < angle.RadiansValue) maxAngle = angle.RadiansValue;

        angle = Tangent.GetAngle(frame2.Tangent);
        if (maxAngle < angle.RadiansValue) maxAngle = angle.RadiansValue;

        return maxAngle;
    }

    public Triplet<LinFloat64Vector3D> RotateDirectionsByTangent(ILinFloat64Vector3D newTangent)
    {
        var matrix =
            SquareMatrix3.CreateVectorToVectorRotationMatrix3D(Tangent, newTangent);

        var newNormal1 = matrix * Normal1;
        var newNormal2 = matrix * Normal2;

        return new Triplet<LinFloat64Vector3D>(newNormal1, newNormal2, newTangent.ToLinVector3D());

        //var x = Tangent;
        //var y = Normal1;
        //var z = Normal2;
        //var xRotated = newTangent.ToTuple3D();

        ////Begin GA-FuL MetaContext Code Generation, 2021-11-20T13:06:13.1183386+02:00
        ////MetaContext: TestCode
        ////Input Variables: 12 used, 0 not used, 12 total.
        ////Temp Variables: 138 sub-expressions, 0 generated temps, 138 total.
        ////Target Temp Variables: 14 total.
        ////Output Variables: 6 total.
        ////Computations: 1 average, 144 total.
        ////Memory Reads: 1.7222222222222223 average, 248 total.
        ////Memory Writes: 144 total.
        ////
        ////MetaContext Binding Data:
        ////   1 = constant: '1'
        ////   -1 = constant: '-1'
        ////   2 = constant: '2'
        ////   Rational[1,2] = constant: 'Rational[1,2]'
        ////   x1 = parameter: x.X
        ////   x2 = parameter: x.Y
        ////   x3 = parameter: x.Z
        ////   xRotated1 = parameter: xRotated.X
        ////   xRotated2 = parameter: xRotated.Y
        ////   xRotated3 = parameter: xRotated.Z
        ////   y1 = parameter: y.X
        ////   y2 = parameter: y.Y
        ////   y3 = parameter: y.Z
        ////   z1 = parameter: z.X
        ////   z2 = parameter: z.Y
        ////   z3 = parameter: z.Z

        //var temp0 = x.X * xRotated.X;
        //var temp1 = x.Y * xRotated.Y;
        //temp0 += temp1;
        //temp1 = x.Z * xRotated.Z;
        //temp0 += temp1;
        //temp1 = -temp0;
        //temp1 = 1 + temp1;
        //temp1 = 0.5d * temp1;
        //temp1 = Math.Pow(temp1, 0.5d);
        //var temp2 = x.Y * xRotated.X;
        //var temp3 = x.X * xRotated.Y;
        //temp3 = -temp3;
        //temp2 += temp3;
        //temp3 = temp2 * temp2;
        //temp3 = -temp3;
        //var temp4 = x.Z * xRotated.X;
        //var temp5 = x.X * xRotated.Z;
        //temp5 = -temp5;
        //temp4 += temp5;
        //temp5 = temp4 * temp4;
        //temp5 = -temp5;
        //temp3 += temp5;
        //temp5 = x.Z * xRotated.Y;
        //var temp6 = x.Y * xRotated.Z;
        //temp6 = -temp6;
        //temp5 += temp6;
        //temp6 = temp5 * temp5;
        //temp6 = -temp6;
        //temp3 += temp6;
        //temp3 = -temp3;
        //temp3 = Math.Pow(temp3, 0.5d);
        //temp3 = 1 / temp3;
        //temp2 *= temp3;
        //temp2 = temp1 * temp2;
        //temp6 = -temp2;
        //var temp7 = temp2 * y.Z;
        //temp4 *= temp3;
        //temp4 = temp1 * temp4;
        //var temp8 = temp4 * y.Y;
        //temp8 = -temp8;
        //temp7 += temp8;
        //temp3 = temp5 * temp3;
        //temp1 *= temp3;
        //temp3 = temp1 * y.X;
        //temp3 = temp7 + temp3;
        //temp5 = temp6 * temp3;
        //temp5 = -temp5;
        //temp7 = -temp4;
        //temp0 = 1 + temp0;
        //temp0 = 0.5d * temp0;
        //temp0 = Math.Pow(temp0, 0.5d);
        //temp8 = temp0 * y.X;
        //var temp9 = temp2 * y.Y;
        //temp8 += temp9;
        //temp9 = temp4 * y.Z;
        //temp8 += temp9;
        //temp9 = temp7 * temp8;
        //var temp10 = -temp1;
        //var temp11 = temp0 * y.Y;
        //var temp12 = temp2 * y.X;
        //temp12 = -temp12;
        //temp11 += temp12;
        //temp12 = temp1 * y.Z;
        //temp11 += temp12;
        //temp12 = temp10 * temp11;
        //temp9 += temp12;
        //temp12 = temp0 * y.Z;
        //var temp13 = temp4 * y.X;
        //temp13 = -temp13;
        //temp12 += temp13;
        //temp13 = temp1 * y.Y;
        //temp13 = -temp13;
        //temp12 += temp13;
        //temp13 = temp0 * temp12;
        //temp9 += temp13;
        //var yRotatedZ = temp5 + temp9;

        //temp5 = temp3 * temp7;
        //temp9 = temp6 * temp8;
        //temp13 = temp0 * temp11;
        //temp9 += temp13;
        //temp13 = temp10 * temp12;
        //temp13 = -temp13;
        //temp9 += temp13;
        //var yRotatedY = temp5 + temp9;

        //temp3 *= temp10;
        //temp3 = -temp3;
        //temp5 = temp0 * temp8;
        //temp8 = temp6 * temp11;
        //temp8 = -temp8;
        //temp5 += temp8;
        //temp8 = temp7 * temp12;
        //temp8 = -temp8;
        //temp5 += temp8;
        //var yRotatedX = temp3 + temp5;

        //temp3 = temp0 * z.X;
        //temp5 = temp2 * z.Y;
        //temp3 += temp5;
        //temp5 = temp4 * z.Z;
        //temp3 += temp5;
        //temp5 = temp7 * temp3;
        //temp8 = temp0 * z.Y;
        //temp9 = temp2 * z.X;
        //temp9 = -temp9;
        //temp8 += temp9;
        //temp9 = temp1 * z.Z;
        //temp8 += temp9;
        //temp9 = temp10 * temp8;
        //temp5 += temp9;
        //temp9 = temp0 * z.Z;
        //temp11 = temp4 * z.X;
        //temp11 = -temp11;
        //temp9 += temp11;
        //temp11 = temp1 * z.Y;
        //temp11 = -temp11;
        //temp9 += temp11;
        //temp11 = temp0 * temp9;
        //temp5 += temp11;
        //temp2 *= z.Z;
        //temp4 *= z.Y;
        //temp4 = -temp4;
        //temp2 += temp4;
        //temp1 *= z.X;
        //temp1 = temp2 + temp1;
        //temp2 = temp6 * temp1;
        //temp2 = -temp2;
        //var zRotatedZ = temp5 + temp2;

        //temp2 = temp6 * temp3;
        //temp4 = temp0 * temp8;
        //temp2 += temp4;
        //temp4 = temp10 * temp9;
        //temp4 = -temp4;
        //temp2 += temp4;
        //temp4 = temp7 * temp1;
        //var zRotatedY = temp2 + temp4;

        //temp0 *= temp3;
        //temp2 = temp6 * temp8;
        //temp2 = -temp2;
        //temp0 += temp2;
        //temp2 = temp7 * temp9;
        //temp2 = -temp2;
        //temp0 += temp2;
        //temp1 = temp10 * temp1;
        //temp1 = -temp1;
        //var zRotatedX = temp0 + temp1;

        ////Finish GA-FuL MetaContext Code Generation, 2021-11-20T13:06:13.2049363+02:00

        //var newNormal1 = new Tuple3D(yRotatedX, yRotatedY, yRotatedZ);
        //var newNormal2 = new Tuple3D(zRotatedX, zRotatedY, zRotatedZ);

        //return new Triplet<Tuple3D>(newNormal1, newNormal2, newTangent.ToTuple3D());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Pair<LinFloat64Vector3D> RotateNormalsByTangent(ILinFloat64Vector3D newTangent)
    {
        var matrix =
            SquareMatrix3.CreateVectorToVectorRotationMatrix3D(Tangent, newTangent);

        var newNormal1 = matrix * Normal1;
        var newNormal2 = matrix * Normal2;

        return new Pair<LinFloat64Vector3D>(newNormal1, newNormal2);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Path3DLocalFrame RotateNormalsBy(LinFloat64Angle angle)
    {
        Debug.Assert(angle.IsValid());

        return new Float64Path3DLocalFrame(
            TimeValue,
            Point,
            Tangent,
            Normal1.RotateVectorUsingAxisAngle(Tangent, angle),
            Normal2.RotateVectorUsingAxisAngle(Tangent, angle)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Path3DLocalFrame TranslateBy(ILinFloat64Vector3D translationVector)
    {
        Debug.Assert(translationVector.IsValid());

        return new Float64Path3DLocalFrame(
            TimeValue,
            LinFloat64Vector3D.Create(Point.X + translationVector.X,
                Point.Y + translationVector.Y,
                Point.Z + translationVector.Z),
            Tangent,
            Normal1,
            Normal2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Path3DLocalFrame TranslateRotateNormalsBy(ILinFloat64Vector3D translationVector, LinFloat64Angle angle)
    {
        Debug.Assert(translationVector.IsValid());

        return new Float64Path3DLocalFrame(
            TimeValue,
            LinFloat64Vector3D.Create(Point.X + translationVector.X,
                Point.Y + translationVector.Y,
                Point.Z + translationVector.Z),
            Tangent,
            Normal1.RotateVectorUsingAxisAngle(Tangent, angle),
            Normal2.RotateVectorUsingAxisAngle(Tangent, angle)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Path3DLocalFrame GetNegativeFrame()
    {
        return new Float64Path3DLocalFrame(
            TimeValue,
            Point,
            -Tangent,
            Normal1.GetNegative(),
            Normal2.GetNegative()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Path3DLocalFrame GetRotatedFrameUsingQuaternion(LinFloat64Quaternion quaternion)
    {
        return new Float64Path3DLocalFrame(
            TimeValue,
            Point,
            Tangent.RotateVectorUsingQuaternion(quaternion),
            Normal1.RotateVectorUsingQuaternion(quaternion),
            Normal2.RotateVectorUsingQuaternion(quaternion)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString()
    {
        return new StringBuilder()
            .AppendLine("Frame {")
            .AppendLine($"         t: {TimeValue:G}")
            .AppendLine($"     Point: {Point}")
            .AppendLine($"   Tangent: {Tangent}")
            .AppendLine($"   Normal1: {Normal1}")
            .AppendLine($"   Normal2: {Normal2}")
            .Append("}")
            .ToString();
    }

}