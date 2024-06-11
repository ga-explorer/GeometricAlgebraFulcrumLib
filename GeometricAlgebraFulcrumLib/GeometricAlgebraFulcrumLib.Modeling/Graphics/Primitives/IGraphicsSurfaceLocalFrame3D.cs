using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Frames.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using SixLabors.ImageSharp;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Primitives;

public interface IGraphicsSurfaceLocalFrame3D : 
    ILinFloat64Vector3D
{
    int Index { get; }

    LinFloat64Vector3D Point { get; }

    Color Color { get; set; }

    Pair<Float64Scalar> ParameterValue { get; }

    LinFloat64Normal3D Normal { get; }
}