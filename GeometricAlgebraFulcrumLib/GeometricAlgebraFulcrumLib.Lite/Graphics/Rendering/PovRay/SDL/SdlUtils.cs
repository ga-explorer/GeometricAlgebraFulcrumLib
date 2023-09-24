using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.PovRay.SDL.Values;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.PovRay.SDL
{
    public static class SdlUtils
    {
        public static void Add<T>(this List<T> list, params T[] items)
        {
            list.AddRange(items);
        }


        public static string ScalarOrDefault(this ISdlScalarValue s, string dflt = "0")
        {
            return ReferenceEquals(s, null) ? dflt : s.ToString();
        }


        public static SdlScalarLiteral SdlScalar(this double x)
        {
            return new SdlScalarLiteral(x);
        }

        public static SdlVectorLiteral3D SdlVector(double x, double y, double z)
        {
            return new SdlVectorLiteral3D(x, y, z);
        }

        public static SdlVectorLiteral3D SdlVector(this double x)
        {
            return new SdlVectorLiteral3D(x, x, x);
        }


        public static SdlScene AddFreeCode(this SdlScene scene, string code)
        {
            scene.Statements.Add(new SdlFreeCode { Code = code });

            return scene;
        }
    }
}
