using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Frames.Space3D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Primitives
{
    public interface IGraphicsSurfaceLocalFrame3D : 
        IFloat64Vector3D
    {
        int Index { get; }

        Float64Vector3D Point { get; }

        Color Color { get; set; }

        Pair<double> ParameterValue { get; }

        Normal3D Normal { get; }
    }
}