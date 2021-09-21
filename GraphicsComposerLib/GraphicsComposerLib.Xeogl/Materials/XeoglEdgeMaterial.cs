using GraphicsComposerLib.Xeogl.Constants;

namespace GraphicsComposerLib.Xeogl.Materials
{
    public sealed class XeoglEdgeMaterial : XeoglMaterial
    {
        public override string JavaScriptClassName => "EdgeMaterial";

        public override XeoglMaterialType MaterialType
            => XeoglMaterialType.Edge;


    }
}