﻿using GeometricAlgebraFulcrumLib.Utilities.Structures.AttributeSet;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

public sealed class GrBabylonJsInt32ArrayValue :
    SparseCodeAttributeValue<IReadOnlyList<int>>
{
    internal static GrBabylonJsInt32ArrayValue Create(IReadOnlyList<int> value)
    {
        return new GrBabylonJsInt32ArrayValue(value);
    }


    public static implicit operator GrBabylonJsInt32ArrayValue(string valueText)
    {
        return new GrBabylonJsInt32ArrayValue(valueText);
    }

    public static implicit operator GrBabylonJsInt32ArrayValue(int[] value)
    {
        return new GrBabylonJsInt32ArrayValue(value);
    }
    

    private GrBabylonJsInt32ArrayValue(string valueText)
        : base(valueText)
    {
    }

    private GrBabylonJsInt32ArrayValue(IReadOnlyList<int> value)
        : base(value)
    {
    }


    public override string GetAttributeValueCode()
    {
        return string.IsNullOrEmpty(ValueText) 
            ? Value.GetBabylonJsCode() 
            : ValueText;
    }
}