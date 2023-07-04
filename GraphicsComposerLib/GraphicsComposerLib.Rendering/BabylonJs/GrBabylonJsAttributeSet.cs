using DataStructuresLib.AttributeSet;
using Humanizer;

namespace GraphicsComposerLib.Rendering.BabylonJs;

public abstract class GrBabylonJsAttributeSet :
    SparseCodeAttributeSet,
    IGrBabylonJsCodeElement
{
    public override IEnumerable<KeyValuePair<string, string>> GetKeyValueCodePairs()
    {
        foreach (var (key, attributeValue) in this)
        {
            yield return new KeyValuePair<string, string>(
                key.Camelize(),
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