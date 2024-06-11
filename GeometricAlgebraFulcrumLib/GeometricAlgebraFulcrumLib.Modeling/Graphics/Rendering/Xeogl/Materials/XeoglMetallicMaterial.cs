using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Xeogl.Constants;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Xeogl.Materials;

public sealed class XeoglMetallicMaterial : XeoglMaterial
{
    public override string JavaScriptClassName => "MetallicMaterial";

    public override XeoglMaterialType MaterialType 
        => XeoglMaterialType.Metallic;


}