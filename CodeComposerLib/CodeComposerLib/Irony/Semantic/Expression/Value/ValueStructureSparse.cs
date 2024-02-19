using CodeComposerLib.Irony.Semantic.Type;

namespace CodeComposerLib.Irony.Semantic.Expression.Value;

public class ValueStructureSparse : ValueCompositeSparse<string>
{
    public TypeStructure ValueStructureType { get; }

    public override ILanguageType ExpressionType => ValueStructureType;


    protected ValueStructureSparse(TypeStructure valueType)
    {
        ValueStructureType = valueType;
    }


    public override ILanguageValue this[string accessKey]
    {
        get
        {
            if (InternalDictionary.TryGetValue(accessKey, out var compValue))
                return compValue;

            var dataMemberType = ValueStructureType.GetDataMemberType(accessKey);

            compValue = ValueStructureType.RootAst.CreateDefaultValue(dataMemberType);

            InternalDictionary.Add(accessKey, compValue);

            return compValue;
        }
        set
        {
            //TODO: Should you verify that the structure type contains a data member with the name in 'access_key'?

            if (InternalDictionary.ContainsKey(accessKey))
                InternalDictionary[accessKey] = value;

            else
                InternalDictionary.Add(accessKey, value);
        }
    }

    public override ILanguageValue DuplicateValue(bool deepCopy)
    {
        var value = new ValueStructureSparse(ValueStructureType);

        foreach (var pair in InternalDictionary)
            value.InternalDictionary.Add(pair.Key, pair.Value);

        return value;
    }


    public override string ToString()
    {
        return RootAst.Describe(this);
    }


    public static ValueStructureSparse Create(TypeStructure valueType)
    {
        return new ValueStructureSparse(valueType);
    }
}