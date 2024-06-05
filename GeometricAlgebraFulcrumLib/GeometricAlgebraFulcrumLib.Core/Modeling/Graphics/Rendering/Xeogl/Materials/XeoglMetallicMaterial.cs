using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.Xeogl.Constants;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.Xeogl.Materials;

public sealed class XeoglMetallicMaterial : XeoglMaterial
{
    public override string JavaScriptClassName => "MetallicMaterial";

    public override XeoglMaterialType MaterialType 
        => XeoglMaterialType.Metallic;


}