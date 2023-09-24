using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Xeogl.Constants;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Xeogl.Materials
{
    public sealed class XeoglEdgeMaterial : XeoglMaterial
    {
        public override string JavaScriptClassName => "EdgeMaterial";

        public override XeoglMaterialType MaterialType
            => XeoglMaterialType.Edge;


    }
}