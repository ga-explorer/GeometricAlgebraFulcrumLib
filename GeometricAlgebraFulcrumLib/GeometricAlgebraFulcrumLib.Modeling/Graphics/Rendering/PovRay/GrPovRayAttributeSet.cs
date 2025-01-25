using GeometricAlgebraFulcrumLib.Utilities.Structures.AttributeSet;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay;

public abstract class GrPovRayAttributeSet :
    SparseCodeAttributeSet,
    IGrPovRayCodeElement
{
    public override IEnumerable<KeyValuePair<string, string>> GetKeyValueCodePairs()
    {
        var keyValueCodePairs = 
            this.OrderBy(p => p.Key);

        foreach (var (key, attributeValue) in keyValueCodePairs)
        {
            yield return new KeyValuePair<string, string>(
                key,
                attributeValue.GetAttributeValueCode()
            );
        }
    }


    //public GrPovRayBooleanValue? GetPovRayBooleanValue(string key)
    //{
    //    return GetAttributeValueOrNull<GrPovRayBooleanValue>(key);
    //}
    
    //public GrPovRayCodeValue? GetPovRayCodeValue(string key)
    //{
    //    return GetAttributeValueOrNull<GrPovRayCodeValue>(key);
    //}

    //public GrPovRayColor3Value? GetPovRayColor3Value(string key)
    //{
    //    return GetAttributeValueOrNull<GrPovRayColor3Value>(key);
    //}

    //public GrPovRayColor4Value? GetPovRayColor4Value(string key)
    //{
    //    return GetAttributeValueOrNull<GrPovRayColor4Value>(key);
    //}


    public override string GetAttributeSetCode()
    {
        return GetPovRayCode();
    }

    public bool IsEmptyCodeElement()
    {
        return Count == 0;
    }

    public abstract string GetPovRayCode();
}