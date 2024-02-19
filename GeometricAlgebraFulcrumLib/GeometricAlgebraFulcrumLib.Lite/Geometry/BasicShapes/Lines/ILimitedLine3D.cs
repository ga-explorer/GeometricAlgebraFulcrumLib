namespace GeometricAlgebraFulcrumLib.Lite.Geometry.BasicShapes.Lines;

public interface ILimitedLine3D : ILine3D
{
    double ParameterMinValue { get; }

    double ParameterMaxValue { get; }
}