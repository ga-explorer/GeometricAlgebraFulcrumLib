using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Xeogl.Constants;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Xeogl.Materials;

public sealed class XeoglSpecularMaterial : XeoglMaterial
{
    public override string JavaScriptClassName => "SpecularMaterial";

    public override XeoglMaterialType MaterialType
        => XeoglMaterialType.Specular;


}