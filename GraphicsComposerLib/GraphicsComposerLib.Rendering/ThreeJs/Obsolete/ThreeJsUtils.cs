using GraphicsComposerLib.Rendering.ThreeJs.Obsolete.Math;
using NumericalGeometryLib.BasicMath.Tuples;
using TextComposerLib.Code.JavaScript;

namespace GraphicsComposerLib.Rendering.ThreeJs.Obsolete
{
    public static class ThreeJsUtils
    {
        public static JavaScriptAttributesDictionary SetThreeJsVector3Value(this JavaScriptAttributesDictionary composer, string key, IFloat64Tuple3D value)
        {
            composer.SetTextValue(
                key, 
                value.ToThreeJsVector3().GetJavaScriptCode()
            );

            return composer;
        }

        public static JavaScriptAttributesDictionary SetThreeJsVector3Value(this JavaScriptAttributesDictionary composer, string key, IFloat64Tuple3D value, IFloat64Tuple3D valueDefault)
        {
            composer.SetTextValue(
                key, 
                value.ToThreeJsVector3().ToString(), 
                valueDefault.ToThreeJsVector3().ToString()
            );

            return composer;
        }


        public static TjVector3 ToThreeJsVector3(this IFloat64Tuple3D value)
        {
            return value is TjVector3 v ? v : new TjVector3(value);
        }
    }
}