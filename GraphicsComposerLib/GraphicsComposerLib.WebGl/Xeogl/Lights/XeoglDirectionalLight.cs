using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;
using NumericalGeometryLib.BasicMath.Tuples.Mutable;
using GraphicsComposerLib.WebGl.Xeogl.Constants;
using TextComposerLib.Code.JavaScript;

namespace GraphicsComposerLib.WebGl.Xeogl.Lights
{
    /// <summary>
    /// http://xeogl.org/docs/classes/DirLight.html
    /// </summary>
    public sealed class XeoglDirectionalLight : XeoglLight
    {
        private static Tuple3D DefaultLightDirection { get; }
            = new Tuple3D(1, 1, 1);


        public override string JavaScriptClassName => "DirLight";

        public XeoglSpace Space { get; set; } 
            = XeoglSpace.View;

        public bool CastsShadow { get; set; }

        public MutableTuple3D LightDirection { get; set; }
            = new MutableTuple3D(DefaultLightDirection);


        public XeoglDirectionalLight()
        {
        }

        public XeoglDirectionalLight(ITuple3D lightDirection)
        {
            LightDirection.SetTuple(lightDirection);
        }


        public override void UpdateConstructorAttributes(JavaScriptAttributesDictionary composer)
        {
            base.UpdateConstructorAttributes(composer);

            composer
                .SetValue("space", Space, XeoglSpace.View)
                .SetValue("shadow", CastsShadow, false)
                .SetNumbersArrayValue("dir", LightDirection, DefaultLightDirection);
        }

        
        //public override string ToString()
        //{
        //    var composer = new XeoglAttributesTextComposer();

        //    UpdateAttributesComposer(composer);

        //    return composer
        //        .AppendXeoglConstructorCall(this)
        //        .ToString();
        //}
    }
}
