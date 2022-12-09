using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;
using GraphicsComposerLib.Geometry.Primitives.Vertices;
using SixLabors.ImageSharp;

namespace GraphicsComposerLib.Geometry.Primitives
{
    public interface IGraphicsCurveLocalFrame3D : 
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
        GrNormal3D Normal1 { get; }

        /// <summary>
        /// The binormal vector of the frame
        /// </summary>
        GrNormal3D Normal2 { get; }
    }
}