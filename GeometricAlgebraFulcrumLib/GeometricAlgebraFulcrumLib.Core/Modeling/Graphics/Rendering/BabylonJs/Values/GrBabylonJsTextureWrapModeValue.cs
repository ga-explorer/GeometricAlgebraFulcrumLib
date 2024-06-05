using GeometricAlgebraFulcrumLib.Utilities.Structures.AttributeSet;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.BabylonJs.Constants;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.BabylonJs.Values;

public sealed class GrBabylonJsTextureWrapModeValue :
    SparseCodeAttributeValue<GrBabylonJsTextureWrapMode>
{
    public static implicit operator GrBabylonJsTextureWrapModeValue(string valueText)
    {
        return new GrBabylonJsTextureWrapModeValue(valueText);
    }

    public static implicit operator GrBabylonJsTextureWrapModeValue(GrBabylonJsTextureWrapMode value)
    {
        return new GrBabylonJsTextureWrapModeValue(value);
    }


    private GrBabylonJsTextureWrapModeValue(string valueText)
        : base(valueText)
    {
    }

    private GrBabylonJsTextureWrapModeValue(GrBabylonJsTextureWrapMode value)
        : base(value)
    {
    }


    public override string GetCode()
    {
        return string.IsNullOrEmpty(ValueText) 
            ? Value.GetBabylonJsCode() 
            : ValueText;
    }
}