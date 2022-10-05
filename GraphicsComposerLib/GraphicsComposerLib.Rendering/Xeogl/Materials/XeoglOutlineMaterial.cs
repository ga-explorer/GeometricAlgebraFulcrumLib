using GraphicsComposerLib.Rendering.Xeogl.Constants;

namespace GraphicsComposerLib.Rendering.Xeogl.Materials
{
    public sealed class XeoglOutlineMaterial : XeoglMaterial
    {
        public override string JavaScriptClassName => "OutlineMaterial";

        public override XeoglMaterialType MaterialType
            => XeoglMaterialType.Outline;


    }
}