using TextComposerLib.Text;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.BabylonJs;

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