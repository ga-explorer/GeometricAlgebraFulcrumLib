using GraphicsComposerLib.WebGl.Xeogl.Constants;

namespace GraphicsComposerLib.WebGl.Xeogl.Materials
{
    public abstract class XeoglMaterial : XeoglComponent
    {
        public abstract XeoglMaterialType MaterialType { get; }

        public string MaterialTypeName 
            => MaterialType.GetName();
    }
}
