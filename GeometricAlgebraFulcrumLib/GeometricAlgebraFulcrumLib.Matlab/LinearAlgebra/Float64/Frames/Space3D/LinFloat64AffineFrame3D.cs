using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Matlab.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Matlab.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Frames.Space3D;

/// <summary>
/// This class represents a directions frame of 3 vectors U, V, W where
/// the components are double precision numbers
/// </summary>
public class LinFloat64AffineFrame3D :
    ILinFloat64Vector3D
{
    /// <summary>
    /// Create a set of 3 right-handed orthonormal direction vectors from the given vector
    /// </summary>
    /// <param name="origin"></param>
    /// <param name="direction"></param>
    /// <param name="rightHanded"></param>
    /// <returns></returns>
    
    public static LinFloat64AffineFrame3D CreateOrthonormal(ILinFloat64Vector3D origin, ILinFloat64Vector3D direction, bool rightHanded = true)
    {
        Debug.Assert(!direction.VectorENormSquared().IsNearZero());

        var u = direction.ToUnitLinVector3D();
        var v = direction.GetUnitNormal();
        var w = rightHanded ? u.VectorUnitCross(v) : v.VectorUnitCross(u);

        return new LinFloat64AffineFrame3D(
            origin.ToLinVector3D(),
            u,
            v,
            w
        );
    }

    
    public static LinFloat64AffineFrame3D Create(ILinFloat64Vector3D origin, ILinFloat64Vector3D direction1, ILinFloat64Vector3D direction2, ILinFloat64Vector3D direction3)
    {
        Debug.Assert(!direction1.VectorENormSquared().IsNearZero());

        return new LinFloat64AffineFrame3D(
            origin.ToLinVector3D(),
            direction1.ToLinVector3D(),
            direction2.ToLinVector3D(),
            direction3.ToLinVector3D()
        );
    }


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


    
    private LinFloat64AffineFrame3D(LinFloat64Vector3D origin, LinFloat64Vector3D direction1, LinFloat64Vector3D direction2, LinFloat64Vector3D direction3)
    {
        Origin = origin;
        Direction1 = direction1;
        Direction2 = direction2;
        Direction3 = direction3;

        Debug.Assert(IsValid());
    }


    
    public bool IsValid()
    {
        return Origin.IsValid() &&
               Direction1.IsValid() &&
               Direction2.IsValid() &&
               Direction3.IsValid();
    }

    
    public bool IsFrame3D()
    {
        if (Direction1.VectorENormSquared().IsNearZero()) return false;
        if (Direction2.VectorENormSquared().IsNearZero()) return false;
        if (Direction3.VectorENormSquared().IsNearZero()) return false;

        return true;
    }

    
    public bool IsRightHanded()
    {
        return Direction1.Determinant(Direction2, Direction3) > 0.0d;
    }

    
    public bool IsLeftHanded()
    {
        return Direction1.Determinant(Direction2, Direction3) < 0.0d;
    }

    
    public LinFloat64Vector3D GetLocalVector(double u, double v, double w)
    {
        return u * Direction1 +
               v * Direction2 +
               w * Direction3;
    }

    
    public LinFloat64Vector3D GetLocalVector(ITriplet<double> scalarList)
    {
        return scalarList.Item1 * Direction1 +
               scalarList.Item2 * Direction2 +
               scalarList.Item3 * Direction3;
    }

    
    public LinFloat64Vector3D GetLocalPoint(ITriplet<double> scalarList)
    {
        return Origin +
               scalarList.Item1 * Direction1 +
               scalarList.Item2 * Direction2 +
               scalarList.Item3 * Direction3;
    }

    
    public LinFloat64Vector3D GetLocalPoint(double u, double v, double w)
    {
        return Origin +
               u * Direction1 +
               v * Direction2 +
               w * Direction3;
    }
}