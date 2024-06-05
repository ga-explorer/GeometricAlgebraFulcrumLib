using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Frames.Space3D;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;
using SixLabors.ImageSharp;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Primitives;

public interface IGraphicsSurfaceLocalFrame3D : 
    ILinFloat64Vector3D
{
    int Index { get; }

    LinFloat64Vector3D Point { get; }

    Color Color { get; set; }

    Pair<Float64Scalar> ParameterValue { get; }

    LinFloat64Normal3D Normal { get; }
}