using EuclideanGeometryLib.BasicMath.Tuples.Immutable;
using TextComposerLib.Code.JavaScript;

namespace GraphicsComposerLib.WebGl.Xeogl.Cameras
{
    public static class XeoglCameraDefaults
    {
        public static Tuple3D EyePoint { get; }
            = new Tuple3D(0, 0, 10);

        public static Tuple3D LookAtPoint { get; }
            = Tuple3D.Zero;

        public static Tuple3D WorldUpDirection { get; }
            = new Tuple3D(0, 1, 0);

        public static string WorldAxisArray { get; }
            = new double[] {1, 0, 0, 0, 1, 0, 0, 0, 1}
                .ToJavaScriptNumbersArrayText();
    }
}