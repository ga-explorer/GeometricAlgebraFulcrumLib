using GraphicsComposerLib.Xeogl.Constants;

namespace GraphicsComposerLib.Xeogl.Materials
{
    public sealed class XeoglOutlineMaterial : XeoglMaterial
    {
        public override string JavaScriptClassName => "OutlineMaterial";

        public override XeoglMaterialType MaterialType
            => XeoglMaterialType.Outline;


    }
}