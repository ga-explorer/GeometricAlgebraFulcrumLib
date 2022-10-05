using GraphicsComposerLib.Rendering.Xeogl.Constants;

namespace GraphicsComposerLib.Rendering.Xeogl.Materials
{
    public abstract class XeoglMaterial : XeoglComponent
    {
        public abstract XeoglMaterialType MaterialType { get; }

        public string MaterialTypeName 
            => MaterialType.GetName();
    }
}
