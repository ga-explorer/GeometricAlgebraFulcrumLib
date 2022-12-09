using NumericalGeometryLib.BasicMath.Tuples;

namespace NumericalGeometryLib.BasicMath.Coordinates
{
    public interface IPolarPosition2D : 
        IFloat64Tuple2D
    {
        double R { get; }

        PlanarAngle Theta { get; }
    }
}