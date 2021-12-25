using NumericalGeometryLib.BasicMath.Tuples;

namespace NumericalGeometryLib.BasicMath.Coordinates
{
    public interface IPolarPosition2D : ITuple2D
    {
        double R { get; }

        PlanarAngle Theta { get; }
    }
}