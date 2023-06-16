using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;
using TextComposerLib.Code.JavaScript;

namespace GraphicsComposerLib.Rendering.Xeogl.Cameras
{
    public static class XeoglCameraDefaults
    {
        public static Float64Vector3D EyePoint { get; }
            = Float64Vector3D.Create(0, 0, 10);

        public static Float64Vector3D LookAtPoint { get; }
            = Float64Vector3D.Zero;

        public static Float64Vector3D WorldUpDirection { get; }
            = Float64Vector3D.Create(0, 1, 0);

        public static string WorldAxisArray { get; }
            = new double[] {1, 0, 0, 0, 1, 0, 0, 0, 1}
                .ToJavaScriptNumbersArrayText();
    }
}