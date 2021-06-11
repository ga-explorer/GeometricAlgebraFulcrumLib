using CodeComposerLib.Irony.Semantic.Type;

namespace CodeComposerLib.Irony.Semantic.Expression.Value
{
    public class ValueNull : ILanguageValuePrimitive
    {
        public ILanguageType ExpressionType { get; protected set; }

        public IronyAst RootAst => ExpressionType.RootAst;

        /// <summary>
        /// A language value is always a simple expression
        /// </summary>
        public bool IsSimpleExpression => true;


        protected ValueNull(ILanguageType valueType)
        {
            ExpressionType = valueType;
        }


        public virtual ILanguageValue DuplicateValue(bool deepCopy)
        {
            return this;
        }


        public static ValueNull Create(ILanguageType valueType)
        {
            return new ValueNull(valueType);
        }
    }
}
