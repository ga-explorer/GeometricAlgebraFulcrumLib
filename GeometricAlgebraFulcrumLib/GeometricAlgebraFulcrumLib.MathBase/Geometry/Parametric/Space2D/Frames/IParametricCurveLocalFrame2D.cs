using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space2D;

namespace GeometricAlgebraFulcrumLib.MathBase.Geometry.Parametric.Space2D.Frames
{
    public interface IParametricCurveLocalFrame2D :
        IFloat64Tuple2D
    {
        int Index { get; }

        Float64Vector2D Point { get; }

        Color Color { get; set; }

        double ParameterValue { get; }

        Float64Vector2D Tangent { get; }
        
        Float64Vector2D Normal { get; }
    }
}