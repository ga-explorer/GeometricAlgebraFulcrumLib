using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;
using GraphicsComposerLib.Rendering.ThreeJs.Obsolete.Math;
using TextComposerLib.Code.JavaScript;

namespace GraphicsComposerLib.Rendering.ThreeJs.Obsolete
{
    public static class ThreeJsUtils
    {
        public static JavaScriptAttributesDictionary SetThreeJsVector3Value(this JavaScriptAttributesDictionary composer, string key, IFloat64Vector3D value)
        {
            composer.SetTextValue(
                key, 
                value.ToThreeJsVector3().GetJavaScriptCode()
            );

            return composer;
        }

        public static JavaScriptAttributesDictionary SetThreeJsVector3Value(this JavaScriptAttributesDictionary composer, string key, IFloat64Vector3D value, IFloat64Vector3D valueDefault)
        {
            composer.SetTextValue(
                key, 
                value.ToThreeJsVector3().ToString(), 
                valueDefault.ToThreeJsVector3().ToString()
            );

            return composer;
        }


        public static TjVector3 ToThreeJsVector3(this IFloat64Vector3D value)
        {
            return value is TjVector3 v ? v : new TjVector3(value);
        }
    }
}