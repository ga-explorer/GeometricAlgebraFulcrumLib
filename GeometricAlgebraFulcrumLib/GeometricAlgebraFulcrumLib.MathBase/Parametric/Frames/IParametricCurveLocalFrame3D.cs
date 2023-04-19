using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Frames.Space3D;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples.Immutable;

namespace GeometricAlgebraFulcrumLib.MathBase.Parametric.Frames
{
    public interface IParametricCurveLocalFrame3D :
        IFloat64Tuple3D
    {
        int Index { get; }

        /// <summary>
        /// The parameter value that gives the curve point
        /// </summary>
        double ParameterValue { get; }

        /// <summary>
        /// The curve point where the frame is attached
        /// </summary>
        Float64Tuple3D Point { get; }

        Color Color { get; set; }

        /// <summary>
        /// The 1st direction vector, also the curve tangent
        /// </summary>
        Float64Tuple3D Tangent { get; }

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