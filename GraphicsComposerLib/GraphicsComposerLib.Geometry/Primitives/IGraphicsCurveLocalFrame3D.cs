using System.Drawing;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;
using GraphicsComposerLib.Geometry.Primitives.Vertices;

namespace GraphicsComposerLib.Geometry.Primitives
{
    public interface IGraphicsCurveLocalFrame3D : 
        ITuple3D
    {
        int Index { get; }

        Tuple3D Point { get; }

        Color Color { get; set; }

        double ParameterValue { get; }

        Tuple3D Tangent { get; }

        GrNormal3D Normal1 { get; }

        GrNormal3D Normal2 { get; }
    }
}