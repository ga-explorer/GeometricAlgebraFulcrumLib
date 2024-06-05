using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.Xeogl.Constants;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.Xeogl.Materials;

public abstract class XeoglMaterial : XeoglComponent
{
    public abstract XeoglMaterialType MaterialType { get; }

    public string MaterialTypeName 
        => MaterialType.GetName();
}