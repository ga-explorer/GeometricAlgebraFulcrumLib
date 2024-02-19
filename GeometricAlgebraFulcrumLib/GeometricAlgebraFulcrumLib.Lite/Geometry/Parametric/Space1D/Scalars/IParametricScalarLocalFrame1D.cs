namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space1D.Scalars;

public interface IParametricScalarLocalFrame1D
{
    int Index { get; }

    double Point { get; }

    Color Color { get; set; }

    double ParameterValue { get; }

    double Tangent { get; }
}