using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Meshes;
using GeometricAlgebraFulcrumLib.Utilities.Structures.AttributeSet;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

public sealed class GrBabylonJsMeshValue :
    SparseCodeAttributeValue<GrBabylonJsMesh>
{
    public static implicit operator GrBabylonJsMeshValue(string valueText)
    {
        return new GrBabylonJsMeshValue(valueText);
    }

    public static implicit operator GrBabylonJsMeshValue(GrBabylonJsMesh value)
    {
        return new GrBabylonJsMeshValue(value);
    }


    private GrBabylonJsMeshValue(string valueText)
        : base(valueText)
    {
    }

    private GrBabylonJsMeshValue(GrBabylonJsMesh value)
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