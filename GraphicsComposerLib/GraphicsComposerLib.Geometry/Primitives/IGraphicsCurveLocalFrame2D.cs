using System.Drawing;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.Geometry.Primitives
{
    public interface IGraphicsCurveLocalFrame2D : 
        ITuple2D
    {
        int Index { get; }

        Tuple2D Point { get; }

        Color Color { get; set; }

        double ParameterValue { get; }

        Tuple2D Tangent { get; }
    }
}