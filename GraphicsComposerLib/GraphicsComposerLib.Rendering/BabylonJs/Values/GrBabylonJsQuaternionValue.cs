using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.Rendering.BabylonJs.Values;

public sealed class GrBabylonJsQuaternionValue :
    GrBabylonJsValue<ITuple4D>
{
    internal static GrBabylonJsQuaternionValue Create(ITuple4D value)
    {
        return new GrBabylonJsQuaternionValue(value);
    }


    public static implicit operator GrBabylonJsQuaternionValue(string valueText)
    {
        return new GrBabylonJsQuaternionValue(valueText);
    }

    public static implicit operator GrBabylonJsQuaternionValue(Tuple4D value)
    {
        return new GrBabylonJsQuaternionValue(value);
    }


    private GrBabylonJsQuaternionValue(string valueText)
        : base(valueText)
    {
    }

    private GrBabylonJsQuaternionValue(ITuple4D value)
        : base(value)
    {
    }


    public override string GetCode()
    {
        return string.IsNullOrEmpty(ValueText) 
            ? Value.GetQuaternionBabylonJsCode() 
            : ValueText;
    }
}