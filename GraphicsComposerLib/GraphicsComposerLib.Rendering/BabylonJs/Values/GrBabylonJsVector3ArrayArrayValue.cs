using DataStructuresLib.Basic;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.Rendering.BabylonJs.Values;

public sealed class GrBabylonJsVector3ArrayArrayValue :
    GrBabylonJsValue<IReadOnlyList<IReadOnlyList<ITriplet<double>>>>
{
    internal static GrBabylonJsVector3ArrayArrayValue Create(IReadOnlyList<IReadOnlyList<ITriplet<double>>> value)
    {
        return new GrBabylonJsVector3ArrayArrayValue(value);
    }


    public static implicit operator GrBabylonJsVector3ArrayArrayValue(string valueText)
    {
        return new GrBabylonJsVector3ArrayArrayValue(valueText);
    }

    public static implicit operator GrBabylonJsVector3ArrayArrayValue(Float64Tuple3D[][] value)
    {
        return new GrBabylonJsVector3ArrayArrayValue(value);
    }
    
    public static implicit operator GrBabylonJsVector3ArrayArrayValue(IFloat64Tuple3D[][] value)
    {
        return new GrBabylonJsVector3ArrayArrayValue(value);
    }


    private GrBabylonJsVector3ArrayArrayValue(string valueText)
        : base(valueText)
    {
    }

    private GrBabylonJsVector3ArrayArrayValue(IReadOnlyList<IReadOnlyList<ITriplet<double>>> value)
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