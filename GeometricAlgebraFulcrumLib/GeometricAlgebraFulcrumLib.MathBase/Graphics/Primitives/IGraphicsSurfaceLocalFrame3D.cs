using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Frames.Space3D;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.MathBase.Graphics.Primitives
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