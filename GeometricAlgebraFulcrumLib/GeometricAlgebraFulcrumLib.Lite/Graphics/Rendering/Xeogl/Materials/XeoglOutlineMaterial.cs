using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Xeogl.Constants;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Xeogl.Materials;

public sealed class XeoglOutlineMaterial : XeoglMaterial
{
    public override string JavaScriptClassName => "OutlineMaterial";

    public override XeoglMaterialType MaterialType
        => XeoglMaterialType.Outline;


}