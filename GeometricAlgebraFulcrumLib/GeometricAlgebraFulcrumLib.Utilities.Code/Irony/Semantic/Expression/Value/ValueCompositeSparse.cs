using System.Collections;
using GeometricAlgebraFulcrumLib.Utilities.Code.Irony.Semantic.Type;

namespace GeometricAlgebraFulcrumLib.Utilities.Code.Irony.Semantic.Expression.Value;

public abstract class ValueCompositeSparse<TK> : ILanguageValueCompositeSparse<TK> where TK : IComparable<TK>
{
    public abstract ILanguageType ExpressionType { get; }

    protected readonly Dictionary<TK, ILanguageValue> InternalDictionary = new Dictionary<TK, ILanguageValue>();

    /// <summary>
    /// A language value is always a simple expression
    /// </summary>
    public bool IsSimpleExpression => true;


    public abstract ILanguageValue this[TK accessKey] { get; set; }

    public abstract ILanguageValue DuplicateValue(bool deepCopy);


    public void Add(TK key, ILanguageValue value)
    {
        InternalDictionary.Add(key, value);
    }

    public bool ContainsKey(TK key)
    {
        return InternalDictionary.ContainsKey(key);
    }

    public ICollection<TK> Keys => InternalDictionary.Keys;

    public bool Remove(TK key)
    {
        return InternalDictionary.Remove(key);
    }

    public bool TryGetValue(TK key, out ILanguageValue value)
    {
        return InternalDictionary.TryGetValue(key, out value);
    }

    public ICollection<ILanguageValue> Values => InternalDictionary.Values;

    public void Add(KeyValuePair<TK, ILanguageValue> item)
    {
        InternalDictionary.Add(item.Key, item.Value);
    }

    public void Clear()
    {
        InternalDictionary.Clear();
    }

    public bool Contains(KeyValuePair<TK, ILanguageValue> item)
    {
        throw new NotImplementedException();
    }

    public void CopyTo(KeyValuePair<TK, ILanguageValue>[] array, int arrayIndex)
    {
        throw new NotImplementedException();
    }

    public int Count => InternalDictionary.Count;

    public bool IsReadOnly => false;

    public bool Remove(KeyValuePair<TK, ILanguageValue> item)
    {
        throw new NotImplementedException();
    }

    public IEnumerator<KeyValuePair<TK, ILanguageValue>> GetEnumerator()
    {
        return InternalDictionary.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return InternalDictionary.GetEnumerator();
    }

    public IronyAst RootAst => ExpressionType.RootAst;


    //public abstract void AcceptVisitor(IASTNodeAcyclicVisitor visitor);
}