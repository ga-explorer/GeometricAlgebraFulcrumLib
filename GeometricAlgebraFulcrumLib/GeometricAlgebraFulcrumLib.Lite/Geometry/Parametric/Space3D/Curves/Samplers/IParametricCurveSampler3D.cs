using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space3D.Curves.Samplers
{
    public interface IParametricCurveSampler3D :
        IGeometricElement,
        IReadOnlyList<ParametricCurveLocalFrame3D>
    {
        IParametricCurve3D Curve { get; }

        Float64ScalarRange ParameterRange { get; }
        
        bool IsPeriodic { get; }
        
        IEnumerable<Float64Scalar> GetParameterValues();

        IEnumerable<Float64ScalarRange> GetParameterSections();

        IEnumerable<Float64Vector3D> GetPoints();

        IEnumerable<Float64Vector3D> GetTangents();

        IEnumerable<ParametricCurveLocalFrame3D> GetFrames();
    }
}
