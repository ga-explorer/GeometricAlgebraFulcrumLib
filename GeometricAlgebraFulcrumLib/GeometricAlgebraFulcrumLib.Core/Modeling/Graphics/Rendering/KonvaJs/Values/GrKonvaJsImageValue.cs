﻿using GeometricAlgebraFulcrumLib.Utilities.Structures.AttributeSet;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.KonvaJs.Shapes;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.KonvaJs.Values;

public sealed class GrKonvaJsImageValue :
    SparseCodeAttributeValue<GrKonvaJsImage>
{
    public static implicit operator GrKonvaJsImageValue(string valueText)
    {
        return new GrKonvaJsImageValue(valueText);
    }

    public static implicit operator GrKonvaJsImageValue(GrKonvaJsImage value)
    {
        return new GrKonvaJsImageValue(value);
    }
    

    private GrKonvaJsImageValue(string valueText)
        : base(valueText)
    {
    }

    private GrKonvaJsImageValue(GrKonvaJsImage value)
        : base(value)
    {
    }


    public override string GetCode()
    {
        return string.IsNullOrEmpty(ValueText) 
            ? Value.GetCode() 
            : ValueText;
    }
}