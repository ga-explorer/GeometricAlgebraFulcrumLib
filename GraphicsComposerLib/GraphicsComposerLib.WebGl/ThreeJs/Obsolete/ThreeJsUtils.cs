using EuclideanGeometryLib.BasicMath.Tuples;
using GraphicsComposerLib.WebGl.ThreeJs.Obsolete.Math;
using TextComposerLib.Code.JavaScript;

namespace GraphicsComposerLib.WebGl.ThreeJs.Obsolete
{
    public static class ThreeJsUtils
    {
        public static JavaScriptAttributesDictionary SetThreeJsVector3Value(this JavaScriptAttributesDictionary composer, string key, ITuple3D value)
        {
            composer.SetTextValue(
                key, 
                value.ToThreeJsVector3().GetJavaScriptCode()
            );

            return composer;
        }

        public static JavaScriptAttributesDictionary SetThreeJsVector3Value(this JavaScriptAttributesDictionary composer, string key, ITuple3D value, ITuple3D valueDefault)
        {
            composer.SetTextValue(
                key, 
                value.ToThreeJsVector3().ToString(), 
                valueDefault.ToThreeJsVector3().ToString()
            );

            return composer;
        }


        public static TjVector3 ToThreeJsVector3(this ITuple3D value)
        {
            return value is TjVector3 v ? v : new TjVector3(value);
        }
    }
}