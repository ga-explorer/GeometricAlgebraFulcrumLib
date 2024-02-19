using CodeComposerLib.Irony.Semantic.Type;

namespace CodeComposerLib.Irony.Semantic.Expression.Value;

/// <summary>
/// This class represents an atomic value (like bool, int and float in C++)
/// </summary>
/// <typeparam name="T">The C# type of the value</typeparam>
public class ValuePrimitive<T> : ILanguageValuePrimitive
{
    //TODO: Make this mutable for one time (useful for defining default values for new types)
    public TypePrimitive ValuePrimitiveType { get; }

    public T Value { get; }

    public ILanguageType ExpressionType => ValuePrimitiveType;

    public IronyAst RootAst => ValuePrimitiveType.RootAst;

    /// <summary>
    /// A language value is always a simple expression
    /// </summary>
    public bool IsSimpleExpression => true;


    protected ValuePrimitive(TypePrimitive valueType, T value)
    {
        ValuePrimitiveType = valueType;

        Value = value;
    }


    public virtual ILanguageValue DuplicateValue(bool deepCopy)
    {
        return this;
    }


    public override string ToString()
    {
        return Value.ToString();
    }


    public static ValuePrimitive<T> Create(TypePrimitive valueType, T value)
    {
        return new ValuePrimitive<T>(valueType, value);
    }
}