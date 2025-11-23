using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space3D.Volumes.Sampled;

public interface IVolumeEvaluator
{
    (LinFloat64Vector3D Position, LinFloat64Vector3D Gradient, double Value) GetSample(double u, double v, double w);
}