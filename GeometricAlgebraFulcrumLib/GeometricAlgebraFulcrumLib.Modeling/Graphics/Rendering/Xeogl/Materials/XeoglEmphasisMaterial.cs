using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Xeogl.Constants;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Xeogl.Materials;

public sealed class XeoglEmphasisMaterial : XeoglMaterial
{
    public override string JavaScriptClassName => "EmphasisMaterial";

    public override XeoglMaterialType MaterialType
        => XeoglMaterialType.Emphasis;


}