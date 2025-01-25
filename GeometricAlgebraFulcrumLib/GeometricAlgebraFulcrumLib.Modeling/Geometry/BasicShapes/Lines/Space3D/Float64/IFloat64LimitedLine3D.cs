namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Lines.Space3D.Float64;

public interface IFloat64LimitedLine3D :
    IFloat64Line3D
{
    double ParameterMinValue { get; }

    double ParameterMaxValue { get; }
}