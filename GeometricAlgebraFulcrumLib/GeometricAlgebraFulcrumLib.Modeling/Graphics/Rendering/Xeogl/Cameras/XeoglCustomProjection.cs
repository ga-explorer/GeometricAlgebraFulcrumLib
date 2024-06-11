using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Matrices;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Xeogl.Constants;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Xeogl.Cameras;

public sealed class XeoglCustomProjection : XeoglCameraProjection
{
    public override string JavaScriptClassName 
        => "CustomProjection";

    public override XeoglCameraProjectionType ProjectionType
        => XeoglCameraProjectionType.Custom;

    public SquareMatrix4 Matrix { get; }
        = new SquareMatrix4();
}