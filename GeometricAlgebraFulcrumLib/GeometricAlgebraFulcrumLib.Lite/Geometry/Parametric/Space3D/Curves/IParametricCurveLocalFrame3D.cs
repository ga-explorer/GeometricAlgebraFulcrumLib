using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Frames.Space3D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space3D.Curves
{
    public interface IParametricCurveLocalFrame3D :
        IFloat64Vector3D
    {
        int Index { get; }

        /// <summary>
        /// The parameter value that gives the curve point
        /// </summary>
        Float64Scalar ParameterValue { get; }

        /// <summary>
        /// The curve point where the frame is attached
        /// </summary>
        Float64Vector3D Point { get; }

        Color Color { get; set; }

        /// <summary>
        /// The 1st direction vector, also the curve tangent
        /// </summary>
        Float64Vector3D Tangent { get; }

        /// <summary>
        /// The normal vector of the frame
        /// </summary>
        Normal3D Normal1 { get; }

        /// <summary>
        /// The binormal vector of the frame
        /// </summary>
        Normal3D Normal2 { get; }
    }
}