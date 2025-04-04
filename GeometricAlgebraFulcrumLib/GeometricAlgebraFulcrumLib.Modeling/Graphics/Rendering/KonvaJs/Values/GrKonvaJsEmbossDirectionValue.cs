﻿using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Constants;
using GeometricAlgebraFulcrumLib.Utilities.Structures.AttributeSet;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Values;

public sealed class GrKonvaJsEmbossDirectionValue :
    SparseCodeAttributeValue<GrKonvaJsEmbossDirection>
{
    public static implicit operator GrKonvaJsEmbossDirectionValue(string valueText)
    {
        return new GrKonvaJsEmbossDirectionValue(valueText);
    }

    public static implicit operator GrKonvaJsEmbossDirectionValue(GrKonvaJsEmbossDirection value)
    {
        return new GrKonvaJsEmbossDirectionValue(value);
    }


    private GrKonvaJsEmbossDirectionValue(string valueText)
        : base(valueText)
    {
    }

    private GrKonvaJsEmbossDirectionValue(GrKonvaJsEmbossDirection value)
        : base(value)
    {
    }


    public override string GetAttributeValueCode()
    {
        return string.IsNullOrEmpty(ValueText) 
            ? Value.GetKonvaJsCode() 
            : ValueText;
    }
}