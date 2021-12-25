using NumericalGeometryLib.BasicMath.Tuples;

namespace NumericalGeometryLib.BasicMath.Coordinates
{
    public interface ISphericalPosition3D : ITuple3D
    {
        double R { get; }

        PlanarAngle Theta { get; }

        PlanarAngle Phi { get; }
    }
}