using GeometricAlgebraFulcrumLib.MathBase.BasicMath;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Borders;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Parametric.Space2D.Frames;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space2D;

namespace GeometricAlgebraFulcrumLib.MathBase.Geometry.Parametric.Space2D.Curves.Samplers
{
    public interface IParametricCurveSampler2D :
        IGeometricElement,
        IReadOnlyList<ParametricCurveLocalFrame2D>
    {
        IParametricCurve2D Curve { get; }

        Float64Range1D ParameterRange { get; }
        
        bool IsPeriodic { get; }

        IEnumerable<double> GetParameterValues();

        IEnumerable<Float64Range1D> GetParameterSections();

        IEnumerable<Float64Vector2D> GetPoints();

        IEnumerable<Float64Vector2D> GetTangents();

        IEnumerable<ParametricCurveLocalFrame2D> GetFrames();
    }
}
