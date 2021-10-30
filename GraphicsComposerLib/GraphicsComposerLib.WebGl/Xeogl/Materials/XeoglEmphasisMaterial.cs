using GraphicsComposerLib.WebGl.Xeogl.Constants;

namespace GraphicsComposerLib.WebGl.Xeogl.Materials
{
    public sealed class XeoglEmphasisMaterial : XeoglMaterial
    {
        public override string JavaScriptClassName => "EmphasisMaterial";

        public override XeoglMaterialType MaterialType
            => XeoglMaterialType.Emphasis;


    }
}