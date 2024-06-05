using GeometricAlgebraFulcrumLib.Utilities.Structures.AttributeSet;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.BabylonJs.Materials;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.BabylonJs.Values;

public sealed class GrBabylonJsMaterialValue :
    SparseCodeAttributeValue<GrBabylonJsMaterial>
{
    public static implicit operator GrBabylonJsMaterialValue(string valueText)
    {
        return new GrBabylonJsMaterialValue(valueText);
    }

    public static implicit operator GrBabylonJsMaterialValue(GrBabylonJsMaterial value)
    {
        return new GrBabylonJsMaterialValue(value);
    }


    private GrBabylonJsMaterialValue(string valueText)
        : base(valueText)
    {
    }

    private GrBabylonJsMaterialValue(GrBabylonJsMaterial value)
        : base(value)
    {
    }


    public override string GetCode()
    {
        return string.IsNullOrEmpty(ValueText) 
            ? Value.ToString() 
            : ValueText;
    }
}