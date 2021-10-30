using GraphicsComposerLib.WebGl.Xeogl.Constants;

namespace GraphicsComposerLib.WebGl.Xeogl.Materials
{
    public sealed class XeoglSpecularMaterial : XeoglMaterial
    {
        public override string JavaScriptClassName => "SpecularMaterial";

        public override XeoglMaterialType MaterialType
            => XeoglMaterialType.Specular;


    }
}