using GraphicsComposerLib.Xeogl.Constants;

namespace GraphicsComposerLib.Xeogl.Materials
{
    public sealed class XeoglEmphasisMaterial : XeoglMaterial
    {
        public override string JavaScriptClassName => "EmphasisMaterial";

        public override XeoglMaterialType MaterialType
            => XeoglMaterialType.Emphasis;


    }
}