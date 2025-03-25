using GeometricAlgebraFulcrumLib.Algebra;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors3D.Float64.Samplers;

public interface IParametricCurveSampler3D :
    IAlgebraicElement,
    IReadOnlyList<Float64Path3DLocalFrame>
{
    Float64Path3D Curve { get; }

    Float64ScalarRange ParameterRange { get; }

    bool IsPeriodic { get; }

    IEnumerable<double> GetParameterValues();

    IEnumerable<Float64ScalarRange> GetParameterSections();

    IEnumerable<LinFloat64Vector3D> GetPoints();

    IEnumerable<LinFloat64Vector3D> GetTangents();

    IEnumerable<Float64Path3DLocalFrame> GetFrames();
}