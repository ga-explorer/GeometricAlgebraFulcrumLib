using NumericalGeometryLib.BasicMath.Tuples.Immutable;
using TextComposerLib.Code.JavaScript;

namespace GraphicsComposerLib.Rendering.Xeogl.Cameras
{
    public static class XeoglCameraDefaults
    {
        public static Float64Tuple3D EyePoint { get; }
            = new Float64Tuple3D(0, 0, 10);

        public static Float64Tuple3D LookAtPoint { get; }
            = Float64Tuple3D.Zero;

        public static Float64Tuple3D WorldUpDirection { get; }
            = new Float64Tuple3D(0, 1, 0);

        public static string WorldAxisArray { get; }
            = new double[] {1, 0, 0, 0, 1, 0, 0, 0, 1}
                .ToJavaScriptNumbersArrayText();
    }
}