using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Xeogl.Constants;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Xeogl.Materials
{
    public abstract class XeoglMaterial : XeoglComponent
    {
        public abstract XeoglMaterialType MaterialType { get; }

        public string MaterialTypeName 
            => MaterialType.GetName();
    }
}
