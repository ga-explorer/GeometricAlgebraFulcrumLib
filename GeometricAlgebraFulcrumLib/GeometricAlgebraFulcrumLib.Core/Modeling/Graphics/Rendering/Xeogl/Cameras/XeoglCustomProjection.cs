using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.Xeogl.Constants;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Matrices;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.Xeogl.Cameras;

public sealed class XeoglCustomProjection : XeoglCameraProjection
{
    public override string JavaScriptClassName 
        => "CustomProjection";

    public override XeoglCameraProjectionType ProjectionType
        => XeoglCameraProjectionType.Custom;

    public SquareMatrix4 Matrix { get; }
        = new SquareMatrix4();
}