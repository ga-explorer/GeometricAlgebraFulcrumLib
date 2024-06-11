using SixLabors.ImageSharp;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space1D.Scalars;

public interface IFloat64ParametricScalarLocalFrame1D
{
    int Index { get; }

    double Point { get; }

    Color Color { get; set; }

    double ParameterValue { get; }

    double Tangent { get; }
}