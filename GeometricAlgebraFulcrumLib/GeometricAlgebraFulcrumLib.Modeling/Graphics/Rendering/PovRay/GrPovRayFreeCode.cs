using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay;

public class GrPovRayFreeCode : 
    IGrPovRayStatement
{
    public GrPovRayCodeValue Code { get; }

    
    
    public GrPovRayFreeCode(string code)
    {
        Code = code;
    }


    public bool IsEmptyCodeElement()
    {
        return Code.IsEmptyCodeElement();
    }

    public string GetPovRayCode()
    {
        return Code.GetAttributeValueCode();
    }

    public override string ToString()
    {
        return GetPovRayCode();
    }
}