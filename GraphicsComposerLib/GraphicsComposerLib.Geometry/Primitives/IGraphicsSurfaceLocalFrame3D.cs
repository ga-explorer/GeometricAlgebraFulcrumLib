using System.Drawing;
using DataStructuresLib.Basic;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;
using GraphicsComposerLib.Geometry.Primitives.Vertices;

namespace GraphicsComposerLib.Geometry.Primitives
{
    public interface IGraphicsSurfaceLocalFrame3D : 
        ITuple3D
    {
        int Index { get; }

        Tuple3D Point { get; }

        Color Color { get; set; }

        Pair<double> ParameterValue { get; }

        GrNormal3D Normal { get; }
    }
}