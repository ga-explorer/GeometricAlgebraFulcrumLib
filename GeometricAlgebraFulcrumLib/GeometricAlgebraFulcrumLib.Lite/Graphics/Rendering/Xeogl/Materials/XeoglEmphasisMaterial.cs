using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Xeogl.Constants;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Xeogl.Materials
{
    public sealed class XeoglEmphasisMaterial : XeoglMaterial
    {
        public override string JavaScriptClassName => "EmphasisMaterial";

        public override XeoglMaterialType MaterialType
            => XeoglMaterialType.Emphasis;


    }
}