using GraphicsComposerLib.WebGl.Xeogl.Constants;

namespace GraphicsComposerLib.WebGl.Xeogl.Materials
{
    public sealed class XeoglEdgeMaterial : XeoglMaterial
    {
        public override string JavaScriptClassName => "EdgeMaterial";

        public override XeoglMaterialType MaterialType
            => XeoglMaterialType.Edge;


    }
}