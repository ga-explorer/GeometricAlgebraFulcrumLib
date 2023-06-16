using GeometricAlgebraFulcrumLib.MathBase.BasicMath;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Borders;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Parametric.Space3D.Frames;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.MathBase.Geometry.Parametric.Space3D.Curves.Samplers
{
    public interface IParametricCurveSampler3D :
        IGeometricElement,
        IReadOnlyList<ParametricCurveLocalFrame3D>
    {
        IParametricCurve3D Curve { get; }

        Float64Range1D ParameterRange { get; }
        
        bool IsPeriodic { get; }

        IEnumerable<double> GetParameterValues();

        IEnumerable<Float64Range1D> GetParameterSections();

        IEnumerable<Float64Vector3D> GetPoints();

        IEnumerable<Float64Vector3D> GetTangents();

        IEnumerable<ParametricCurveLocalFrame3D> GetFrames();
    }
}
