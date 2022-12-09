using DataStructuresLib.Basic;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.Rendering.BabylonJs.Values;

public sealed class GrBabylonJsVector3ArrayValue :
    GrBabylonJsValue<IReadOnlyList<ITriplet<double>>>
{
    internal static GrBabylonJsVector3ArrayValue Create(IReadOnlyList<ITriplet<double>> value)
    {
        return new GrBabylonJsVector3ArrayValue(value);
    }


    public static implicit operator GrBabylonJsVector3ArrayValue(string valueText)
    {
        return new GrBabylonJsVector3ArrayValue(valueText);
    }

    public static implicit operator GrBabylonJsVector3ArrayValue(Float64Tuple3D[] value)
    {
        return new GrBabylonJsVector3ArrayValue(value);
    }
    
    public static implicit operator GrBabylonJsVector3ArrayValue(IFloat64Tuple3D[] value)
    {
        return new GrBabylonJsVector3ArrayValue(value);
    }


    private GrBabylonJsVector3ArrayValue(string valueText)
        : base(valueText)
    {
    }

    private GrBabylonJsVector3ArrayValue(IReadOnlyList<ITriplet<double>> value)
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