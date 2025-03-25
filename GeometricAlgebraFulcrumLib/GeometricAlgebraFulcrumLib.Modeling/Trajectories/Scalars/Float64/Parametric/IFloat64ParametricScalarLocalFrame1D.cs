using SixLabors.ImageSharp;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Scalars.Float64.Parametric;

public interface IFloat64ParametricScalarLocalFrame1D
{
    int Index { get; }

    double Point { get; }

    Color Color { get; set; }

    double ParameterValue { get; }

    double Tangent { get; }
}