using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Meshes;
using GeometricAlgebraFulcrumLib.Utilities.Structures.AttributeSet;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

public sealed class GrBabylonJsLinesMeshValue :
    SparseCodeAttributeValue<GrBabylonJsLinesMesh>
{
    public static implicit operator GrBabylonJsLinesMeshValue(string valueText)
    {
        return new GrBabylonJsLinesMeshValue(valueText);
    }

    public static implicit operator GrBabylonJsLinesMeshValue(GrBabylonJsLinesMesh value)
    {
        return new GrBabylonJsLinesMeshValue(value);
    }


    private GrBabylonJsLinesMeshValue(string valueText)
        : base(valueText)
    {
    }

    private GrBabylonJsLinesMeshValue(GrBabylonJsLinesMesh value)
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