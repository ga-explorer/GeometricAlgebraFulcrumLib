using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Xeogl.Constants;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Xeogl.Materials;

public sealed class XeoglSpecularMaterial : XeoglMaterial
{
    public override string JavaScriptClassName => "SpecularMaterial";

    public override XeoglMaterialType MaterialType
        => XeoglMaterialType.Specular;


}