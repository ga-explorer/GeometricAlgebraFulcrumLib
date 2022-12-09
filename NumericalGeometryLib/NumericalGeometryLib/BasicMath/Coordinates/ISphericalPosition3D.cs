using NumericalGeometryLib.BasicMath.Tuples;

namespace NumericalGeometryLib.BasicMath.Coordinates
{
    public interface ISphericalPosition3D : IFloat64Tuple3D
    {
        double R { get; }

        PlanarAngle Theta { get; }

        PlanarAngle Phi { get; }
    }
}