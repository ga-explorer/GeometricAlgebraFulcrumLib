using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples;

namespace GeometricAlgebraFulcrumLib.MathBase.BasicMath.Coordinates
{
    public interface IPolarPosition2D : 
        IFloat64Tuple2D
    {
        double R { get; }

        Float64PlanarAngle Theta { get; }
    }
}