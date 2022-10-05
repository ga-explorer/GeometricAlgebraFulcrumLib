using GraphicsComposerLib.Rendering.Xeogl.Constants;

namespace GraphicsComposerLib.Rendering.Xeogl.Materials
{
    public sealed class XeoglSpecularMaterial : XeoglMaterial
    {
        public override string JavaScriptClassName => "SpecularMaterial";

        public override XeoglMaterialType MaterialType
            => XeoglMaterialType.Specular;


    }
}