namespace NumericalGeometryLib.BasicShapes.Lines
{
    public interface ILimitedLine3D : ILine3D
    {
        double ParameterMinValue { get; }

        double ParameterMaxValue { get; }
    }
}