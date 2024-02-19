using System;
using System.Collections.Generic;
using CodeComposerLib.Irony.Semantic.Expression.ValueAccess;
using CodeComposerLib.Irony.Semantic.Type;

namespace CodeComposerLib.Irony.Semantic.Expression.Value;

public class ValueStructure : ILanguageValueComposite
{
    public TypeStructure ValueStructureType { get; }

    public Dictionary<string, ILanguageValue> Value { get; }

    public ILanguageType ExpressionType => ValueStructureType;

    public IronyAst RootAst => ValueStructureType.RootAst;

    /// <summary>
    /// A language value is always a simple expression
    /// </summary>
    public bool IsSimpleExpression => true;


    protected ValueStructure(TypeStructure valueType, Dictionary<string, ILanguageValue> value)
    {
        ValueStructureType = valueType;

        Value = value;
    }

    protected ValueStructure(TypeStructure valueType)
    {
        ValueStructureType = valueType;

        Value = new Dictionary<string,ILanguageValue>();
    }


    public virtual ILanguageValue DuplicateValue(bool deepCopy)
    {
        var value = new Dictionary<string, ILanguageValue>();

        if (deepCopy)
            foreach (var pair in Value)
                value.Add(pair.Key, pair.Value.DuplicateValue(true));

        else
            foreach (var pair in Value)
                value.Add(pair.Key, pair.Value);

        return new ValueStructure(ValueStructureType, value);
    }

    public ILanguageValue GetComponentValue(string componentName)
    {
        throw new NotImplementedException();
    }

    public void SetComponentValue(string componentName, ILanguageValue value)
    {
        throw new NotImplementedException();
    }

    public ILanguageValue GetValue(LanguageValueAccess valueAccess)
    {
        throw new NotImplementedException();
    }

    public void SetValue(LanguageValueAccess valueAccess, ILanguageValue value)
    {
        throw new NotImplementedException();
    }

    public ILanguageValue GetComponentValue(ValueAccessStep accessStep)
    {
        throw new NotImplementedException();
    }

    public void SetComponentValue(ValueAccessStep accessStep, ILanguageValue value)
    {
        throw new NotImplementedException();
    }


    //public static ValueStructure Create(TypeStructure value_type)
    //{
    //    return new ValueStructure(value_type);
    //}

    public static ValueStructure Create(TypeStructure valueType, Dictionary<string, ILanguageValue> value)
    {
        return new ValueStructure(valueType, value);
    }
}