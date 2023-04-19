using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples.Immutable;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples.Mutable;
using GraphicsComposerLib.Rendering.Xeogl.Constants;
using TextComposerLib.Code.JavaScript;

namespace GraphicsComposerLib.Rendering.Xeogl.Lights
{
    /// <summary>
    /// http://xeogl.org/docs/classes/DirLight.html
    /// </summary>
    public sealed class XeoglDirectionalLight : XeoglLight
    {
        private static Float64Tuple3D DefaultLightDirection { get; }
            = new Float64Tuple3D(1, 1, 1);


        public override string JavaScriptClassName => "DirLight";

        public XeoglSpace Space { get; set; } 
            = XeoglSpace.View;

        public bool CastsShadow { get; set; }

        public MutableFloat64Tuple3D LightDirection { get; set; }
            = new MutableFloat64Tuple3D(DefaultLightDirection);


        public XeoglDirectionalLight()
        {
        }

        public XeoglDirectionalLight(IFloat64Tuple3D lightDirection)
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
