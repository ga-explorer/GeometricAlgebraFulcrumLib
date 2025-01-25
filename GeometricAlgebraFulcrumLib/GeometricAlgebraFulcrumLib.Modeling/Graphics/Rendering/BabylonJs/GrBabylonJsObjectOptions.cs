using GeometricAlgebraFulcrumLib.Utilities.Text.Text;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs;

public abstract class GrBabylonJsObjectOptions :
    GrBabylonJsAttributeSet
{
    public override string GetBabylonJsCode()
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
        return GetBabylonJsCode();
    }
}