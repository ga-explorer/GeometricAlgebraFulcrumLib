using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.VGa.Float64;

public static class EuclideanGeometryUtils
{
    public static Triplet<LinFloat64Vector3D> GetCirclePointsTriplet3D(this ILinFloat64Vector3D center, ILinFloat64Vector3D normal, double radius)
    {
        var q =
            LinUnitBasisVector3D.PositiveZ.CreateAxisToVectorRotationQuaternion(
                normal.ToUnitLinVector3D(LinFloat64Vector3D.E3)
            );

        return GetXyCirclePointsTriplet3D(radius).MapItems(
            v => center + q.RotateVector(v)
        );
    }

    public static Triplet<LinFloat64Vector3D> GetUnitXyCirclePointsTriplet3D()
    {
        var point1 = LinFloat64Vector3D.E1;

        var point2 = LinFloat64Vector3D.Create(
            LinFloat64PolarAngle.Angle120.Cos(),
            LinFloat64PolarAngle.Angle120.Sin(),
            Float64Scalar.Zero
        );

        var point3 = LinFloat64Vector3D.Create(
            LinFloat64PolarAngle.Angle240.Cos(),
            LinFloat64PolarAngle.Angle240.Sin(),
            Float64Scalar.Zero
        );

        return new Triplet<LinFloat64Vector3D>(point1, point2, point3);
    }

    public static Triplet<LinFloat64Vector3D> GetXyCirclePointsTriplet3D(double radius)
    {
        var point1 = radius * LinFloat64Vector3D.E1;

        var point2 = LinFloat64Vector3D.Create(
            radius * LinFloat64PolarAngle.Angle120.Cos(),
            radius * LinFloat64PolarAngle.Angle120.Sin(),
            Float64Scalar.Zero
        );

        var point3 = LinFloat64Vector3D.Create(
            radius * LinFloat64PolarAngle.Angle240.Cos(),
            radius * LinFloat64PolarAngle.Angle240.Sin(),
            Float64Scalar.Zero
        );

        return new Triplet<LinFloat64Vector3D>(point1, point2, point3);
    }

}