using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Frames.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using SixLabors.ImageSharp;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Primitives;

public interface IGraphicsSurfaceLocalFrame3D : 
    ILinFloat64Vector3D
{
    int Index { get; }

    LinFloat64Vector3D Point { get; }

    Color Color { get; set; }

    LinFloat64Vector2D ParameterValue { get; }

    LinFloat64Normal3D Normal { get; }
}