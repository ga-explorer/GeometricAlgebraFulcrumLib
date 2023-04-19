namespace GeometricAlgebraFulcrumLib.MathBase.BasicShapes.Lines
{
    public interface ILimitedLine2D : ILine2D
    {
        double ParameterMinValue { get; }

        double ParameterMaxValue { get; }
    }
}