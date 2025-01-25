namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Lines.Space2D.Float64;

public interface IFloat64LimitedLine2D :
    IFloat64Line2D
{
    double ParameterMinValue { get; }

    double ParameterMaxValue { get; }
}