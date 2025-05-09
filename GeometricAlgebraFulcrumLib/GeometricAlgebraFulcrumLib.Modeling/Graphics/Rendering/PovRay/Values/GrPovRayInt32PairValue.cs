using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;

public sealed class GrPovRayInt32PairValue :
    GrPovRayValue<Pair<int>>
{
    public static implicit operator GrPovRayInt32PairValue(string valueText)
    {
        return new GrPovRayInt32PairValue(valueText);
    }

    public static implicit operator GrPovRayInt32PairValue(Pair<int> value)
    {
        return new GrPovRayInt32PairValue(value);
    }
    

    private GrPovRayInt32PairValue(string valueText)
        : base(valueText)
    {
    }

    private GrPovRayInt32PairValue(Pair<int> value)
        : base(value)
    {
    }


    public override string GetPovRayCode()
    {
        return string.IsNullOrEmpty(ValueText) 
            ? Value.GetPovRayCode() 
            : ValueText;
    }
}