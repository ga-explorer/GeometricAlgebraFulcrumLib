using EuclideanGeometryLib.BasicMath.Tuples;
using EuclideanGeometryLib.BasicMath.Tuples.Immutable;
using EuclideanGeometryLib.BasicMath.Tuples.Mutable;
using GraphicsComposerLib.Xeogl.Constants;

namespace GraphicsComposerLib.Xeogl.Lights
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


        internal override void UpdateAttributesComposer(XeoglCodeComposer composer)
        {
            base.UpdateAttributesComposer(composer);

            composer
                .SetAttributeValue("space", Space, XeoglSpace.View)
                .SetAttributeValue("shadow", CastsShadow, false)
                .SetAttributeValue("dir", LightDirection, DefaultLightDirection);
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
