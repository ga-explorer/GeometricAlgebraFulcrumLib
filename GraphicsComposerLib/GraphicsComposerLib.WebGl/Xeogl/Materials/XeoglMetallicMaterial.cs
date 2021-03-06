using GraphicsComposerLib.WebGl.Xeogl.Constants;

namespace GraphicsComposerLib.WebGl.Xeogl.Materials
{
    public sealed class XeoglMetallicMaterial : XeoglMaterial
    {
        public override string JavaScriptClassName => "MetallicMaterial";

        public override XeoglMaterialType MaterialType 
            => XeoglMaterialType.Metallic;


    }
}