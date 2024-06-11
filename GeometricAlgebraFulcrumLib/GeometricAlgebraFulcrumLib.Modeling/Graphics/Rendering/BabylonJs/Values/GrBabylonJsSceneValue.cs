using GeometricAlgebraFulcrumLib.Utilities.Structures.AttributeSet;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

public sealed class GrBabylonJsSceneValue :
    SparseCodeAttributeValue<GrBabylonJsScene>
{
    public static implicit operator GrBabylonJsSceneValue(string valueText)
    {
        return new GrBabylonJsSceneValue(valueText);
    }

    public static implicit operator GrBabylonJsSceneValue(GrBabylonJsScene value)
    {
        return new GrBabylonJsSceneValue(value);
    }


    private GrBabylonJsSceneValue(string valueText)
        : base(valueText)
    {
    }

    private GrBabylonJsSceneValue(GrBabylonJsScene value)
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