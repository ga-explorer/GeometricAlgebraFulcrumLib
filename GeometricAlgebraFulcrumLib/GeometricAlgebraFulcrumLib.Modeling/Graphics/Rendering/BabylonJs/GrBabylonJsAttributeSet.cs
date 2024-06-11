using GeometricAlgebraFulcrumLib.Utilities.Structures.AttributeSet;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs;

public abstract class GrBabylonJsAttributeSet :
    SparseCodeAttributeSet,
    IGrBabylonJsCodeElement
{
    public override IEnumerable<KeyValuePair<string, string>> GetKeyValueCodePairs()
    {
        var keyValueCodePairs = 
            this.OrderBy(p => p.Key);

        foreach (var (key, attributeValue) in keyValueCodePairs)
        {
            yield return new KeyValuePair<string, string>(
                key,
                attributeValue.GetCode()
            );
        }
    }


    //public GrBabylonJsBooleanValue? GetBabylonJsBooleanValue(string key)
    //{
    //    return GetAttributeValueOrNull<GrBabylonJsBooleanValue>(key);
    //}
    
    //public GrBabylonJsCodeValue? GetBabylonJsCodeValue(string key)
    //{
    //    return GetAttributeValueOrNull<GrBabylonJsCodeValue>(key);
    //}

    //public GrBabylonJsColor3Value? GetBabylonJsColor3Value(string key)
    //{
    //    return GetAttributeValueOrNull<GrBabylonJsColor3Value>(key);
    //}

    //public GrBabylonJsColor4Value? GetBabylonJsColor4Value(string key)
    //{
    //    return GetAttributeValueOrNull<GrBabylonJsColor4Value>(key);
    //}
    

}