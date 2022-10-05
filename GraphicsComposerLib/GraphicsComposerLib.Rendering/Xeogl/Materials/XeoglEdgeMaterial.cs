using GraphicsComposerLib.Rendering.Xeogl.Constants;

namespace GraphicsComposerLib.Rendering.Xeogl.Materials
{
    public sealed class XeoglEdgeMaterial : XeoglMaterial
    {
        public override string JavaScriptClassName => "EdgeMaterial";

        public override XeoglMaterialType MaterialType
            => XeoglMaterialType.Edge;


    }
}