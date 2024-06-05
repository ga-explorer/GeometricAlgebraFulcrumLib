using GeometricAlgebraFulcrumLib.Core.Algebra;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space3D.Curves.Samplers;

public interface IParametricCurveSampler3D :
    IAlgebraicElement,
    IReadOnlyList<ParametricCurveLocalFrame3D>
{
    IParametricCurve3D Curve { get; }

    Float64ScalarRange ParameterRange { get; }
        
    bool IsPeriodic { get; }
        
    IEnumerable<Float64Scalar> GetParameterValues();

    IEnumerable<Float64ScalarRange> GetParameterSections();

    IEnumerable<LinFloat64Vector3D> GetPoints();

    IEnumerable<LinFloat64Vector3D> GetTangents();

    IEnumerable<ParametricCurveLocalFrame3D> GetFrames();
}