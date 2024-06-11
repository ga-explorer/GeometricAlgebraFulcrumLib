using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Values;
using GeometricAlgebraFulcrumLib.Utilities.Structures.AttributeSet;
using Humanizer;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs;

public abstract class GrKonvaJsAttributeSet :
    SparseCodeAttributeSet,
    IGrKonvaJsCodeElement
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


    public GrKonvaJsBooleanValue? GetKonvaJsBooleanValue(string key)
    {
        return GetAttributeValueOrNull<GrKonvaJsBooleanValue>(key);
    }
    
    public GrKonvaJsBoundingBoxValue? GetKonvaJsBoundingBoxValue(string key)
    {
        return GetAttributeValueOrNull<GrKonvaJsBoundingBoxValue>(key);
    }
    
    public GrKonvaJsCacheConfigValue? GetKonvaJsCacheConfigValue(string key)
    {
        return GetAttributeValueOrNull<GrKonvaJsCacheConfigValue>(key);
    }
    
    public GrKonvaJsCodeValue? GetKonvaJsCodeValue(string key)
    {
        return GetAttributeValueOrNull<GrKonvaJsCodeValue>(key);
    }
    
    public GrKonvaJsColor3Value? GetKonvaJsColor3Value(string key)
    {
        return GetAttributeValueOrNull<GrKonvaJsColor3Value>(key);
    }
    
    public GrKonvaJsColorValue? GetKonvaJsColor4Value(string key)
    {
        return GetAttributeValueOrNull<GrKonvaJsColorValue>(key);
    }
    
    public GrKonvaJsColorLinearGradientListValue? GetKonvaJsColorLinearGradientListValue(string key)
    {
        return GetAttributeValueOrNull<GrKonvaJsColorLinearGradientListValue>(key);
    }

    public GrKonvaJsEmbossDirectionValue? GetKonvaJsEmbossDirectionValue(string key)
    {
        return GetAttributeValueOrNull<GrKonvaJsEmbossDirectionValue>(key);
    }
    

}