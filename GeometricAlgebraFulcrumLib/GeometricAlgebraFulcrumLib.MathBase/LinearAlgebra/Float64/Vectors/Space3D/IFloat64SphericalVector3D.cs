using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D
{
    public interface IFloat64SphericalVector3D : 
        IFloat64Tuple3D
    {
        Float64Scalar R { get; }

        Float64PlanarAngle Theta { get; }

        Float64PlanarAngle Phi { get; }

        bool IsUnitVector();

        bool IsNearUnitVector(double epsilon = 1e-12d);
    }
}