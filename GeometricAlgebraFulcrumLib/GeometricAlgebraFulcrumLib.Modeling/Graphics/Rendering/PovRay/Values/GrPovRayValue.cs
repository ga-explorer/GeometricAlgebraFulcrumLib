using GeometricAlgebraFulcrumLib.Utilities.Structures.AttributeSet;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;

public abstract class GrPovRayValue :
    SparseCodeAttributeValue,
    IGrPovRayCodeElement
{
    protected GrPovRayValue(string valueText) 
        : base(valueText)
    {
    }


    public bool IsEmptyCodeElement()
    {
        return IsEmpty;
    }

    public abstract string GetPovRayCode();

    public override string GetAttributeValueCode()
    {
        return GetPovRayCode();
    }

    public override string ToString()
    {
        return GetPovRayCode();
    }
}

public abstract class GrPovRayValue<T> :
    SparseCodeAttributeValue<T>,
    IGrPovRayCodeElement
{
    protected GrPovRayValue(string valueText) 
        : base(valueText)
    {
    }

    protected GrPovRayValue(T value) 
        : base(value)
    {
    }
    

    public bool IsEmptyCodeElement()
    {
        return IsEmpty;
    }

    public abstract string GetPovRayCode();
    
    public override string GetAttributeValueCode()
    {
        return GetPovRayCode();
    }
    
    public override string ToString()
    {
        return GetPovRayCode();
    }
}