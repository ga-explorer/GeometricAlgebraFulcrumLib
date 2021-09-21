using GraphicsComposerLib.Xeogl.Constants;

namespace GraphicsComposerLib.Xeogl.Materials
{
    public sealed class XeoglMetallicMaterial : XeoglMaterial
    {
        public override string JavaScriptClassName => "MetallicMaterial";

        public override XeoglMaterialType MaterialType 
            => XeoglMaterialType.Metallic;


    }
}