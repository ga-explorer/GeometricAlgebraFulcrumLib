using System.Drawing;

namespace GraphicsComposerLib.Xeogl.Materials
{
    /// <summary>
    /// http://xeogl.org/docs/classes/Fresnel.html
    /// </summary>
    public sealed class XeoglFresnelEffect : XeoglComponent
    {
        public override string JavaScriptClassName => "Fresnel";

        public Color EdgeColor { get; set; } = Color.Black;

        public Color CenterColor { get; set; } = Color.White;

        public double EdgeBias { get; set; } = 0;

        public double CenterBias { get; set; } = 1;

        public double Power { get; set; }


        internal override void UpdateAttributesComposer(XeoglCodeComposer composer)
        {
            base.UpdateAttributesComposer(composer);

            composer
                .SetAttributeValueRgb("cfg.edgeColor", EdgeColor, Color.Black)
                .SetAttributeValueRgb("cfg.centerColor", CenterColor, Color.White)
                .SetAttributeValue("edgeBias", EdgeBias, 0)
                .SetAttributeValue("centerBias", CenterBias, 1)
                .SetAttributeValue("power", Power, 0);
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
