using System;
using CodeComposerLib.Irony.Semantic.Type;

namespace CodeComposerLib.Irony.Semantic.Expression.Value;

public class ValueArraySparse : ValueCompositeSparse<int>
{
    public TypeArray ValueArrayType { get; }


    public override ILanguageType ExpressionType => ValueArrayType;

    public ILanguageType ItemType => ValueArrayType.ArrayItemType;


    protected ValueArraySparse(TypeArray valueType)
    {
        ValueArrayType = valueType;
    }

    public override ILanguageValue this[int accessKey]
    {
        get
        {
            throw new NotImplementedException();
        }
        set
        {
            throw new NotImplementedException();
        }
    }

    public override ILanguageValue DuplicateValue(bool deepCopy)
    {
        throw new NotImplementedException();
    }
}