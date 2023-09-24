namespace GeometricAlgebraFulcrumLib.Lite.Geometry.BasicShapes.Lines
{
    public interface ILimitedLine2D : ILine2D
    {
        double ParameterMinValue { get; }

        double ParameterMaxValue { get; }
    }
}