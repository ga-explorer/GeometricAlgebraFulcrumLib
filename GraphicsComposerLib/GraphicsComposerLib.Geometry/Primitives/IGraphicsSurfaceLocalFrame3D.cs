
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Frames.Space3D;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples.Immutable;
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

        Normal3D Normal { get; }
    }
}