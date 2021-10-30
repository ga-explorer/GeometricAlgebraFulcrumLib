using GraphicsComposerLib.WebGl.Xeogl.Constants;

namespace GraphicsComposerLib.WebGl.Xeogl.Materials
{
    public sealed class XeoglOutlineMaterial : XeoglMaterial
    {
        public override string JavaScriptClassName => "OutlineMaterial";

        public override XeoglMaterialType MaterialType
            => XeoglMaterialType.Outline;


    }
}