namespace GeometricAlgebraFulcrumLib.Utilities.Structures.AttributeSet;

/// <summary>
/// This interface represents a set of attributes for code generation
/// When the number of attributes per object is large, using a field to
/// hold each attribute value is impractical when most attributes use their
/// default values. This interface uses an internal dictionary to store the
/// non-default values for only the used attributes per object
/// </summary>
public interface ISparseCodeAttributeSet :
    IReadOnlyDictionary<string, ISparseCodeAttributeValue>
{
    ISparseCodeAttributeSet Clear();

    bool RemoveAttribute(string key);
    
    string GetAttributeValueText(string key);

    ISparseCodeAttributeValue GetAttributeValue(string key);

    ISparseCodeAttributeValue<T> GetAttributeValue<T>(string key);

    Tuple<bool, ISparseCodeAttributeValue<T>> TryGetAttributeValue<T>(string key);
    
    bool TryGetAttributeValue(string key, out ISparseCodeAttributeValue value);

    bool TryGetAttributeValue<T>(string key, out ISparseCodeAttributeValue<T> value);

    SparseCodeAttributeSet SetAttributeValue(string key, ISparseCodeAttributeValue? value);

    SparseCodeAttributeSet SetAttributeValues(IEnumerable<KeyValuePair<string, ISparseCodeAttributeValue?>> keyValuePairs);

    IEnumerable<KeyValuePair<string, string>> GetKeyValueCodePairs();
}