using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Xeogl.Constants;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Xeogl.Materials;

public sealed class XeoglOutlineMaterial : XeoglMaterial
{
    public override string JavaScriptClassName => "OutlineMaterial";

    public override XeoglMaterialType MaterialType
        => XeoglMaterialType.Outline;


}