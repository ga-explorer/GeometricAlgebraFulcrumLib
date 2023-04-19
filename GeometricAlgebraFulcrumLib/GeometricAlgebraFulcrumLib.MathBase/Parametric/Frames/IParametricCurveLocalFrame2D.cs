using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples.Immutable;

namespace GeometricAlgebraFulcrumLib.MathBase.Parametric.Frames
{
    public interface IParametricCurveLocalFrame2D :
        IFloat64Tuple2D
    {
        int Index { get; }

        Float64Tuple2D Point { get; }

        Color Color { get; set; }

        double ParameterValue { get; }

        Float64Tuple2D Tangent { get; }
    }
}