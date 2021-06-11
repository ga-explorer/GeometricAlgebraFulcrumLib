using CodeComposerLib.Irony.Semantic.Type;

namespace CodeComposerLib.Irony.Semantic.Expression.Value
{
    public class ValueReference : ILanguageValueComposite
    {
        public TypeReference ValueReferenceType { get; }

        public ILanguageType ExpressionType => ValueReferenceType;

        public IronyAst RootAst => ValueReferenceType.RootAst;

        public ILanguageValue Value { get; }

        /// <summary>
        /// A language value is always a simple expression
        /// </summary>
        public bool IsSimpleExpression => true;


        protected ValueReference(TypeReference valueType, ILanguageValue value)
        {
            ValueReferenceType = valueType;

            Value  = value;
        }


        public virtual ILanguageValue DuplicateValue(bool deepCopy)
        {
            return new ValueReference(ValueReferenceType, Value);
        }


        public static ValueReference Create(TypeReference valueType, ILanguageValue value)
        {
            return new ValueReference(valueType, value);
        }
    }
}
