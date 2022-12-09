
using DataStructuresLib.Basic;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;
using GraphicsComposerLib.Geometry.Primitives.Vertices;
using SixLabors.ImageSharp;

namespace GraphicsComposerLib.Geometry.Primitives
{
    public interface IGraphicsSurfaceLocalFrame3D : 
        IFloat64Tuple3D
    {
        int Index { get; }

        Float64Tuple3D Point { get; }

        Color Color { get; set; }

        Pair<double> ParameterValue { get; }

        GrNormal3D Normal { get; }
    }
}