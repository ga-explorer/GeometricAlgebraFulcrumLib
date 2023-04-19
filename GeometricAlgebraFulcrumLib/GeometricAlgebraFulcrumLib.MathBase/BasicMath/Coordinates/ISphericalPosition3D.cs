using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples;

namespace GeometricAlgebraFulcrumLib.MathBase.BasicMath.Coordinates
{
    public interface ISphericalPosition3D : IFloat64Tuple3D
    {
        double R { get; }

        Float64PlanarAngle Theta { get; }

        Float64PlanarAngle Phi { get; }
    }
}