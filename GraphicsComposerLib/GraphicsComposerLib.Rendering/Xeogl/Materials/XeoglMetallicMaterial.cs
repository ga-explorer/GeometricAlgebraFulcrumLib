using GraphicsComposerLib.Rendering.Xeogl.Constants;

namespace GraphicsComposerLib.Rendering.Xeogl.Materials
{
    public sealed class XeoglMetallicMaterial : XeoglMaterial
    {
        public override string JavaScriptClassName => "MetallicMaterial";

        public override XeoglMaterialType MaterialType 
            => XeoglMaterialType.Metallic;


    }
}