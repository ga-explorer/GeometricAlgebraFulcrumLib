using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Frames.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using SixLabors.ImageSharp;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space3D.Curves;

public interface IParametricCurveLocalFrame3D :
    ILinFloat64Vector3D
{
    int Index { get; }

    /// <summary>
    /// The parameter value that gives the curve point
    /// </summary>
    Float64Scalar ParameterValue { get; }

    /// <summary>
    /// The curve point where the frame is attached
    /// </summary>
    LinFloat64Vector3D Point { get; }

    Color Color { get; set; }

    /// <summary>
    /// The 1st direction vector, also the curve tangent
    /// </summary>
    LinFloat64Vector3D Tangent { get; }

    /// <summary>
    /// The normal vector of the frame
    /// </summary>
    LinFloat64Normal3D Normal1 { get; }

    /// <summary>
    /// The binormal vector of the frame
    /// </summary>
    LinFloat64Normal3D Normal2 { get; }
}