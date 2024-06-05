using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Utilities.Text.Code.JavaScript;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.Xeogl.Cameras;

public static class XeoglCameraDefaults
{
    public static LinFloat64Vector3D EyePoint { get; }
        = LinFloat64Vector3D.Create(0, 0, 10);

    public static LinFloat64Vector3D LookAtPoint { get; }
        = LinFloat64Vector3D.Zero;

    public static LinFloat64Vector3D WorldUpDirection { get; }
        = LinFloat64Vector3D.Create(0, 1, 0);

    public static string WorldAxisArray { get; }
        = new double[] {1, 0, 0, 0, 1, 0, 0, 0, 1}
            .ToJavaScriptNumbersArrayText();
}