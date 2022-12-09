using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.Rendering.BabylonJs.Values;

public sealed class GrBabylonJsQuaternionValue :
    GrBabylonJsValue<IFloat64Tuple4D>
{
    internal static GrBabylonJsQuaternionValue Create(IFloat64Tuple4D value)
    {
        return new GrBabylonJsQuaternionValue(value);
    }


    public static implicit operator GrBabylonJsQuaternionValue(string valueText)
    {
        return new GrBabylonJsQuaternionValue(valueText);
    }

    public static implicit operator GrBabylonJsQuaternionValue(Float64Tuple4D value)
    {
        return new GrBabylonJsQuaternionValue(value);
    }


    private GrBabylonJsQuaternionValue(string valueText)
        : base(valueText)
    {
    }

    private GrBabylonJsQuaternionValue(IFloat64Tuple4D value)
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