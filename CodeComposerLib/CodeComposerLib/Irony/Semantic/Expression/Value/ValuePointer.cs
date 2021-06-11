using CodeComposerLib.Irony.Semantic.Type;

namespace CodeComposerLib.Irony.Semantic.Expression.Value
{
    public class ValuePointer : ILanguageValueComposite
    {
        public TypePointer ValuePointerType { get; }

        public ILanguageType ExpressionType => ValuePointerType;

        public IronyAst RootAst => ValuePointerType.RootAst;

        public ILanguageValue Value { get; }

        /// <summary>
        /// A language value is always a simple expression
        /// </summary>
        public bool IsSimpleExpression => true;


        protected ValuePointer(TypePointer valueType, ILanguageValue value)
        {
            ValuePointerType = valueType;

            Value  = value;
        }


        public virtual ILanguageValue DuplicateValue(bool deepCopy)
        {
            return new ValuePointer(ValuePointerType, Value);
        }


        public static ValuePointer Create(TypePointer valueType, ILanguageValue value)
        {
            return new ValuePointer(valueType, value);
        }
    }
}
