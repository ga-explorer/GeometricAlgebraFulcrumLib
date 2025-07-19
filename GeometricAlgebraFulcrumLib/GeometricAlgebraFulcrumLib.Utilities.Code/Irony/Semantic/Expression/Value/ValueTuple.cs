using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraFulcrumLib.Utilities.Code.Irony.Semantic.Type;

namespace GeometricAlgebraFulcrumLib.Utilities.Code.Irony.Semantic.Expression.Value;

public class ValueTuple : ILanguageValueComposite
{
    public TypeTuple ValueTupleType { get; }

    public List<ILanguageValue> Value { get; private set; }

    public ILanguageType ExpressionType => ValueTupleType;

    public IronyAst RootAst => ValueTupleType.RootAst;

    /// <summary>
    /// A language value is always a simple expression
    /// </summary>
    public bool IsSimpleExpression => true;


    protected ValueTuple(TypeTuple valueType, IEnumerable<ILanguageValue> value)
    {
        ValueTupleType = valueType;

        Value.AddRange(value);
    }


    public virtual ILanguageValue DuplicateValue(bool deepCopy)
    {
        return 
            deepCopy ? 
                new ValueTuple(ValueTupleType, Value.Select(x => x.DuplicateValue(true))) : 
                new ValueTuple(ValueTupleType, Value);
    }


    public static ValueTuple Create(TypeTuple valueType, IEnumerable<ILanguageValue> value)
    {
        return new ValueTuple(valueType, value);
    }
}