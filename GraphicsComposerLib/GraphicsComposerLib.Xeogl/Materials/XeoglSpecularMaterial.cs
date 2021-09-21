using GraphicsComposerLib.Xeogl.Constants;

namespace GraphicsComposerLib.Xeogl.Materials
{
    public sealed class XeoglSpecularMaterial : XeoglMaterial
    {
        public override string JavaScriptClassName => "SpecularMaterial";

        public override XeoglMaterialType MaterialType
            => XeoglMaterialType.Specular;


    }
}