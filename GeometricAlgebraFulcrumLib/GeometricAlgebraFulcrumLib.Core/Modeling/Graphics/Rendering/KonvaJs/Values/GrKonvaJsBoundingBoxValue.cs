using GeometricAlgebraFulcrumLib.Utilities.Structures.AttributeSet;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Borders.Space2D.Immutable;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.KonvaJs.Values;

public sealed class GrKonvaJsBoundingBoxValue :
    SparseCodeAttributeValue<BoundingBox2D>
{
    internal static GrKonvaJsBoundingBoxValue Create(BoundingBox2D value)
    {
        return new GrKonvaJsBoundingBoxValue(value);
    }


    public static implicit operator GrKonvaJsBoundingBoxValue(string valueText)
    {
        return new GrKonvaJsBoundingBoxValue(valueText);
    }
    

    private GrKonvaJsBoundingBoxValue(string valueText)
        : base(valueText)
    {
    }

    private GrKonvaJsBoundingBoxValue(BoundingBox2D value)
        : base(value)
    {
    }


    public override string GetCode()
    {
        return string.IsNullOrEmpty(ValueText) 
            ? Value.GetKonvaJsCode() 
            : ValueText;
    }
}