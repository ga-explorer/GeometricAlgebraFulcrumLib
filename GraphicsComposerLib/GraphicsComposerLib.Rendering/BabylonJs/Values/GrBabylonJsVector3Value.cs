using DataStructuresLib.Basic;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.Rendering.BabylonJs.Values;

public sealed class GrBabylonJsVector3Value :
    GrBabylonJsValue<ITriplet<double>>
{
    internal static GrBabylonJsVector3Value Create(ITriplet<double> value)
    {
        return new GrBabylonJsVector3Value(value);
    }


    public static implicit operator GrBabylonJsVector3Value(string valueText)
    {
        return new GrBabylonJsVector3Value(valueText);
    }

    public static implicit operator GrBabylonJsVector3Value(Tuple3D value)
    {
        return new GrBabylonJsVector3Value(value);
    }


    private GrBabylonJsVector3Value(string valueText)
        : base(valueText)
    {
    }

    private GrBabylonJsVector3Value(ITriplet<double> value)
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