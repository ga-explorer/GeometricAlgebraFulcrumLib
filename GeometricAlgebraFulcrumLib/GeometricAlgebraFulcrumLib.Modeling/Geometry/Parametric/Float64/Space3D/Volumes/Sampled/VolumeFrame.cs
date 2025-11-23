using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space3D.Volumes.Sampled;

public readonly struct VolumeFrame
{
    public readonly double ParamU;
    public readonly double ParamV;
    public readonly double ParamW;
    public readonly LinFloat64Vector3D Point;
    public readonly LinFloat64Vector3D Gradient;
    public readonly double Value;

    public VolumeFrame(double u, double v, double w, LinFloat64Vector3D point, LinFloat64Vector3D gradient, double value)
    {
        ParamU = u; ParamV = v; ParamW = w; Point = point; Gradient = gradient; Value = value;
    }
}