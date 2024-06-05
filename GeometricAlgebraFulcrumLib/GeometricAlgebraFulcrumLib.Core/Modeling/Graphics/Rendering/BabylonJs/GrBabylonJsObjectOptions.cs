using GeometricAlgebraFulcrumLib.Utilities.Text.Text;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.BabylonJs;

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