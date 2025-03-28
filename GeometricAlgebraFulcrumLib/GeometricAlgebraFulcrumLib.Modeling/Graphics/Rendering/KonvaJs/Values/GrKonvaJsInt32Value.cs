﻿using GeometricAlgebraFulcrumLib.Utilities.Structures.AttributeSet;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Values;

public sealed class GrKonvaJsInt32Value :
    SparseCodeAttributeValue<int>
{
    public static implicit operator GrKonvaJsInt32Value(string valueText)
    {
        return new GrKonvaJsInt32Value(valueText);
    }

    public static implicit operator GrKonvaJsInt32Value(int value)
    {
        return new GrKonvaJsInt32Value(value);
    }
    
    public static implicit operator GrKonvaJsInt32Value(float value)
    {
        return new GrKonvaJsInt32Value((int) value);
    }
    
    public static implicit operator GrKonvaJsInt32Value(double value)
    {
        return new GrKonvaJsInt32Value((int) value);
    }


    private GrKonvaJsInt32Value(string valueText)
        : base(valueText)
    {
    }

    private GrKonvaJsInt32Value(int value)
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