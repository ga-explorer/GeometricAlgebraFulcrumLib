using GeometricAlgebraFulcrumLib.Utilities.Structures.AttributeSet;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.BabylonJs.Constants;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.BabylonJs.Values;

public sealed class GrBabylonJsAnimationLoopModeValue :
    SparseCodeAttributeValue<GrBabylonJsAnimationLoopMode>
{
    public static implicit operator GrBabylonJsAnimationLoopModeValue(string valueText)
    {
        return new GrBabylonJsAnimationLoopModeValue(valueText);
    }

    public static implicit operator GrBabylonJsAnimationLoopModeValue(GrBabylonJsAnimationLoopMode value)
    {
        return new GrBabylonJsAnimationLoopModeValue(value);
    }


    private GrBabylonJsAnimationLoopModeValue(string valueText)
        : base(valueText)
    {
    }

    private GrBabylonJsAnimationLoopModeValue(GrBabylonJsAnimationLoopMode value)
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

public sealed class GrBabylonJsAnimationTypeValue :
    SparseCodeAttributeValue<GrBabylonJsAnimationType>
{
    public static implicit operator GrBabylonJsAnimationTypeValue(string valueText)
    {
        return new GrBabylonJsAnimationTypeValue(valueText);
    }

    public static implicit operator GrBabylonJsAnimationTypeValue(GrBabylonJsAnimationType value)
    {
        return new GrBabylonJsAnimationTypeValue(value);
    }


    private GrBabylonJsAnimationTypeValue(string valueText)
        : base(valueText)
    {
    }

    private GrBabylonJsAnimationTypeValue(GrBabylonJsAnimationType value)
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