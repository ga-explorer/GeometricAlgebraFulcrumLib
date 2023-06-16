namespace GeometricAlgebraFulcrumLib.MathBase.Geometry.Parametric.Space1D.Frames
{
    public interface IParametricCurveLocalFrame1D
    {
        int Index { get; }

        double Point { get; }

        Color Color { get; set; }

        double ParameterValue { get; }

        double Tangent { get; }
    }
}