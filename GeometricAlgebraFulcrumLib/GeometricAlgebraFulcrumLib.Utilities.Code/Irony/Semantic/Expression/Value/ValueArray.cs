using System;
using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraFulcrumLib.Utilities.Code.Irony.Semantic.Type;

namespace GeometricAlgebraFulcrumLib.Utilities.Code.Irony.Semantic.Expression.Value;

public class ValueArray : ILanguageValueComposite
{
    public TypeArray ValueArrayType { get; }

    public List<ILanguageValue> Value { get; } = new List<ILanguageValue>();


    public ILanguageType ExpressionType => ValueArrayType;

    public IronyAst RootAst => ValueArrayType.RootAst;

    public ILanguageType ItemType => ValueArrayType.ArrayItemType;

    /// <summary>
    /// A language value is always a simple expression
    /// </summary>
    public bool IsSimpleExpression => true;


    protected ValueArray(TypeArray valueType, IEnumerable<ILanguageValue> value)
    {
        ValueArrayType = valueType;

        Value.AddRange(value);
    }


    public ILanguageValue this[int index]
    {
        get
        {
            return Value[index];
        }
        set
        {
            if (value == null)
                throw new ArgumentNullException();

            if (!value.ExpressionType.IsSameType(ExpressionType))
                throw new InvalidCastException();

            Value[index] = value;
        }
    }

    public virtual ILanguageValue DuplicateValue(bool deepCopy)
    {
        return 
            deepCopy ? 
                new ValueArray(ValueArrayType, Value.Select(x => x.DuplicateValue(true))) : 
                new ValueArray(ValueArrayType, Value);
    }


    //public virtual void AcceptVisitor(IASTNodeAcyclicVisitor visitor)
    //{
    //    if (visitor is IASTNodeAcyclicVisitor<ValueArray>)
    //        ((IASTNodeAcyclicVisitor<ValueArray>)visitor).Visit(this);

    //    //You can write fall back logic here if needed.
    //}


    public static ValueArray Create(TypeArray valueType, IEnumerable<ILanguageValue> value)
    {
        return new ValueArray(valueType, value);
    }
}