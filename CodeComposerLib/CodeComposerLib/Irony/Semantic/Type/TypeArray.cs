namespace CodeComposerLib.Irony.Semantic.Type
{
    /// <summary>
    /// This class represents an array of fixed size of items of a single language type
    /// </summary>
    public class TypeArray : ILanguageType
    {
        /// <summary>
        /// The type of the items of the array
        /// </summary>
        public ILanguageType ArrayItemType { get; }

        /// <summary>
        /// The size of the array
        /// </summary>
        public int ArraySize { get; }

        /// <summary>
        /// The parent Irony DSL for this array type
        /// </summary>
        public IronyAst RootAst => ArrayItemType.RootAst;


        protected TypeArray(ILanguageType arrayItemType, int arraySize)
        {
            ArrayItemType = arrayItemType;
            ArraySize = arraySize;
        }


        public virtual string TypeSignature => "array(" + ArrayItemType.TypeSignature + ", " + ArraySize + ")";

        public virtual bool IsSameType(ILanguageType languageType)
        {
            if (!(languageType is TypeArray))
                return false;

            return ArrayItemType.IsSameType(((TypeArray)languageType).ArrayItemType);
        }

        public virtual bool IsCompatibleType(ILanguageType languageType)
        {
            if (!(languageType is TypeArray))
                return false;

            return ArrayItemType.IsCompatibleType(((TypeArray)languageType).ArrayItemType);
        }

        //public virtual void AcceptVisitor(IASTNodeAcyclicVisitor visitor)
        //{
        //    if (visitor is IASTNodeAcyclicVisitor<TypeArray>)
        //        ((IASTNodeAcyclicVisitor<TypeArray>)visitor).Visit(this);

        //    //You can write fall back logic here if needed.
        //}


        public static TypeArray Create(ILanguageType arrayItemType, int arraySize)
        {
            return new TypeArray(arrayItemType, arraySize);
        }
    }
}
