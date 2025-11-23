using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Numerical;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Numerical.Matrices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Modeling.Calculus.Curves;

public class DifferentialCurveFrame3D :
    ITriplet<double>
{
    /// <summary>
    /// Create a set of 3 right-handed orthonormal direction vectors from the given vector
    /// </summary>
    /// <param name="parameterValue"></param>
    /// <param name="origin"></param>
    /// <param name="direction"></param>
    /// <param name="rightHanded"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DifferentialCurveFrame3D CreateOrthonormal(double parameterValue, ITriplet<double> origin, ITriplet<double> direction, bool rightHanded = true)
    {
        Debug.Assert(
            !direction.L2NormSquared().IsNearZero()
        );

        var u = direction.ToUnitLinVector3D();
        var v = direction.GetUnitNormal();
        var w = rightHanded ? u.VectorUnitCross(v) : v.VectorUnitCross(u);

        return new DifferentialCurveFrame3D(
            parameterValue,
            origin.ToLinVector3D(),
            u,
            v,
            w
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DifferentialCurveFrame3D Create(double parameterValue, ITriplet<double> origin, ITriplet<double> direction1, IPair<ITriplet<double>> direction23Pair)
    {
        Debug.Assert(
            !direction1.L2NormSquared().IsNearZero()
        );

        return new DifferentialCurveFrame3D(
            parameterValue,
            origin.ToLinVector3D(),
            direction1.ToLinVector3D(),
            direction23Pair.Item1.ToLinVector3D(),
            direction23Pair.Item2.ToLinVector3D()
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DifferentialCurveFrame3D Create(double parameterValue, LinFloat64Vector3D origin, LinFloat64Vector3D direction1, IPair<LinFloat64Vector3D> direction23Pair)
    {
        Debug.Assert(
            !direction1.L2NormSquared().IsNearZero()
        );

        return new DifferentialCurveFrame3D(
            parameterValue,
            origin.ToLinVector3D(),
            direction1.ToLinVector3D(),
            direction23Pair.Item1.ToLinVector3D(),
            direction23Pair.Item2.ToLinVector3D()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DifferentialCurveFrame3D Create(double parameterValue, ITriplet<double> origin, ITriplet<double> direction1, ITriplet<double> direction2, ITriplet<double> direction3)
    {
        Debug.Assert(
            !direction1.L2NormSquared().IsNearZero()
        );

        return new DifferentialCurveFrame3D(
            parameterValue,
            origin.ToLinVector3D(),
            direction1.ToLinVector3D(),
            direction2.ToLinVector3D(),
            direction3.ToLinVector3D()
        );
    }


    public double ParameterValue { get; }
        
    public int VSpaceDimensions 
        => 3;

    public double Item1 
        => Origin.X;

    public double Item2 
        => Origin.Y;

    public double Item3 
        => Origin.Z;

    public double X 
        => Origin.X;

    public double Y 
        => Origin.Y;

    public double Z 
        => Origin.Z;

    public LinFloat64Vector3D Origin { get; }
        
    public LinFloat64Vector3D Direction1 { get; }

    public LinFloat64Vector3D Direction2 { get; }

    public LinFloat64Vector3D Direction3 { get; }
        
    public double this[int index] 
        => index switch
        {
            0 => X,
            1 => Y,
            2 => Z,
            _ => Float64Scalar.Zero
        };


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private DifferentialCurveFrame3D(double parameterValue, LinFloat64Vector3D origin, LinFloat64Vector3D direction1, LinFloat64Vector3D direction2, LinFloat64Vector3D direction3)
    {
        ParameterValue = parameterValue;
        Origin = origin;
        Direction1 = direction1;
        Direction2 = direction2;
        Direction3 = direction3;

        Debug.Assert(IsValid());
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return ParameterValue.IsValid() &&
               Origin.IsValid() &&
               Direction1.IsValid() &&
               Direction2.IsValid() &&
               Direction3.IsValid();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsOrthogonal(double tolerance = Float64Utils.DefaultTolerance)
    {
        return 
            Direction1.VectorDot(Direction2).IsNearZero(tolerance) &&
            Direction1.VectorDot(Direction3).IsNearZero(tolerance) &&
            Direction2.VectorDot(Direction3).IsNearZero(tolerance);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsOrthonormal(double tolerance = Float64Utils.DefaultTolerance)
    {
        return 
            Direction1.VectorDot(Direction1).IsNearOne(tolerance) &&
            Direction2.VectorDot(Direction2).IsNearOne(tolerance) &&
            Direction3.VectorDot(Direction3).IsNearOne(tolerance) &&
            Direction1.VectorDot(Direction2).IsNearZero(tolerance) &&
            Direction1.VectorDot(Direction3).IsNearZero(tolerance) &&
            Direction2.VectorDot(Direction3).IsNearZero(tolerance);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsFrame3D()
    {
        return !Direction1.Determinant(Direction2, Direction3).IsNearZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsRightHanded()
    {
        return Direction1.Determinant(Direction2, Direction3) > 0.0d;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsLeftHanded()
    {
        return Direction1.Determinant(Direction2, Direction3) < 0.0d;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SquareMatrix3 GetDirectionsMatrix()
    {
        return new SquareMatrix3
        {
            Scalar00 = Direction1.X,
            Scalar10 = Direction1.Y,
            Scalar20 = Direction1.Z,

            Scalar01 = Direction2.X,
            Scalar11 = Direction2.Y,
            Scalar21 = Direction2.Z,

            Scalar02 = Direction3.X,
            Scalar12 = Direction3.Y,
            Scalar22 = Direction3.Z
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3D GetLocalVector(double u, double v, double w)
    {
        return u * Direction1 +
               v * Direction2 +
               w * Direction3;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3D GetLocalVector(ITriplet<double> scalarList)
    {
        return scalarList.Item1 * Direction1 +
               scalarList.Item2 * Direction2 +
               scalarList.Item3 * Direction3;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3D GetLocalPoint(ITriplet<double> scalarList)
    {
        return Origin +
               scalarList.Item1 * Direction1 +
               scalarList.Item2 * Direction2 +
               scalarList.Item3 * Direction3;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3D GetLocalPoint(double u, double v, double w)
    {
        return Origin +
               u * Direction1 +
               v * Direction2 +
               w * Direction3;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Quaternion GetRotationPartQuaternion()
    {
        return GetDirectionsMatrix().GetRotationPart().GetQuaternion();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Quaternion GetRelativeRotationQuaternion(SquareMatrix3 refFrameMatrixInverse)
    {
        var matrix = GetDirectionsMatrix() * refFrameMatrixInverse;

        return matrix.GetRotationPart().GetQuaternion();
    }
}