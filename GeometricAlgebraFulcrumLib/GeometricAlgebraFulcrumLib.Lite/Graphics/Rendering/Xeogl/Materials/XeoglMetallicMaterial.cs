using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Xeogl.Constants;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Xeogl.Materials
{
    public sealed class XeoglMetallicMaterial : XeoglMaterial
    {
        public override string JavaScriptClassName => "MetallicMaterial";

        public override XeoglMaterialType MaterialType 
            => XeoglMaterialType.Metallic;


    }
}