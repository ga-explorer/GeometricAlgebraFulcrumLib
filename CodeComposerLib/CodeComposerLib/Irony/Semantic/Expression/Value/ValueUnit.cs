using CodeComposerLib.Irony.Semantic.Type;

namespace CodeComposerLib.Irony.Semantic.Expression.Value
{
    public sealed class ValueUnit : ILanguageValuePrimitive
    {
        public ILanguageType ExpressionType { get; }

        public IronyAst RootAst => ExpressionType.RootAst;

        /// <summary>
        /// A language value is always a simple expression
        /// </summary>
        public bool IsSimpleExpression => true;


        public ValueUnit(ILanguageType valueType)
        {
            ExpressionType = valueType;
        }


        public ILanguageValue DuplicateValue(bool deepCopy)
        {
            return this;
        }


        public static ValueUnit Create(ILanguageType valueType)
        {
            return new ValueUnit(valueType);
        }
    }
}
