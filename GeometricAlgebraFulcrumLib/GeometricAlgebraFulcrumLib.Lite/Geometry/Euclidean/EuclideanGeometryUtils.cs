using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Euclidean
{
    public static class EuclideanGeometryUtils
    {
        public static Triplet<Float64Vector3D> GetCirclePointsTriplet3D(this IFloat64Vector3D center, IFloat64Vector3D normal, double radius)
        {
            var q =
                LinUnitBasisVector3D.PositiveZ.CreateAxisToVectorRotationQuaternion(
                    normal.ToUnitVector(Float64Vector3D.E3)
                );

            return GetXyCirclePointsTriplet3D(radius).MapItems(
                v => center + q.RotateVector(v)
            );
        }

        public static Triplet<Float64Vector3D> GetUnitXyCirclePointsTriplet3D()
        {
            var point1 = Float64Vector3D.E1;
            
            var point2 = Float64Vector3D.Create(
                Float64PlanarAngle.Angle120.Cos(),
                Float64PlanarAngle.Angle120.Sin(), 
                Float64Scalar.Zero
            );
            
            var point3 = Float64Vector3D.Create(
                Float64PlanarAngle.Angle240.Cos(),
                Float64PlanarAngle.Angle240.Sin(), 
                Float64Scalar.Zero
            );

            return new Triplet<Float64Vector3D>(point1, point2, point3);
        }
        
        public static Triplet<Float64Vector3D> GetXyCirclePointsTriplet3D(double radius)
        {
            var point1 = radius * Float64Vector3D.E1;
            
            var point2 = Float64Vector3D.Create(
                radius * Float64PlanarAngle.Angle120.Cos(),
                radius * Float64PlanarAngle.Angle120.Sin(), 
                Float64Scalar.Zero
            );
            
            var point3 = Float64Vector3D.Create(
                radius * Float64PlanarAngle.Angle240.Cos(),
                radius * Float64PlanarAngle.Angle240.Sin(), 
                Float64Scalar.Zero
            );

            return new Triplet<Float64Vector3D>(point1, point2, point3);
        }

    }
}
