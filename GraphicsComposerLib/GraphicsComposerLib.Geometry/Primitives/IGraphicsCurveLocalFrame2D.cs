
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;
using SixLabors.ImageSharp;

namespace GraphicsComposerLib.Geometry.Primitives
{
    public interface IGraphicsCurveLocalFrame2D : 
        IFloat64Tuple2D
    {
        int Index { get; }

        Float64Tuple2D Point { get; }

        Color Color { get; set; }

        double ParameterValue { get; }

        Float64Tuple2D Tangent { get; }
    }
}