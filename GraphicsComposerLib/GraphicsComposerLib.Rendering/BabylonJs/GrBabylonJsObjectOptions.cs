using TextComposerLib.Text;

namespace GraphicsComposerLib.Rendering.BabylonJs
{
    public abstract class GrBabylonJsObjectOptions :
        GrBabylonJsAttributeSet
    {
        public override string GetCode()
        {
            return GetKeyValueCodePairs()
                .Select(p => $"{p.Key}: {p.Value}")
                .Concatenate(
                    ", ", 
                    "{", 
                    "}"
                );
        }
        
        public override string ToString()
        {
            return GetCode();
        }
    }
}