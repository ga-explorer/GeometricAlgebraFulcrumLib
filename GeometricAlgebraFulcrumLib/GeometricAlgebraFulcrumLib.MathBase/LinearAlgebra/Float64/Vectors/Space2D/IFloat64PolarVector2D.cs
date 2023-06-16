using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space2D
{
    public interface IFloat64PolarVector2D : 
        IFloat64Tuple2D
    {
        Float64Scalar R { get; }

        Float64PlanarAngle Theta { get; }
        
        bool IsUnitVector();

        bool IsNearUnitVector(double epsilon = 1e-12d);
    }
}