using GraphicsComposerLib.Rendering.Xeogl.Constants;

namespace GraphicsComposerLib.Rendering.Xeogl.Materials
{
    public sealed class XeoglEmphasisMaterial : XeoglMaterial
    {
        public override string JavaScriptClassName => "EmphasisMaterial";

        public override XeoglMaterialType MaterialType
            => XeoglMaterialType.Emphasis;


    }
}