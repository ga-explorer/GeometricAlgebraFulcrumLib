using GraphicsComposerLib.Xeogl.Constants;

namespace GraphicsComposerLib.Xeogl.Materials
{
    public abstract class XeoglMaterial : XeoglComponent
    {
        public abstract XeoglMaterialType MaterialType { get; }

        public string MaterialTypeName 
            => MaterialType.GetName();
    }
}
