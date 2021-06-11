using System;

namespace CodeComposerLib.Irony.Semantic.Type
{
    /// <summary>
    /// This class represents a pointer to another type
    /// </summary>
    public class TypePointer : ILanguageType
    {
        /// <summary>
        /// The type of the data that is pointed to
        /// </summary>
        public ILanguageType DataType { get; }

        /// <summary>
        /// The parent DSL for this array
        /// </summary>
        public IronyAst RootAst => DataType.RootAst;


        protected TypePointer(ILanguageType dataType)
        {
            DataType = dataType;
        }


        public virtual string TypeSignature => "pointer(" + DataType.TypeSignature + ")";

        public virtual bool IsSameType(ILanguageType languageType)
        {
            if (!(languageType is TypePointer))
                return false;

            return ((TypePointer)languageType).DataType.IsSameType(DataType);
        }

        public virtual bool IsCompatibleType(ILanguageType languageType)
        {
            if (!(languageType is TypePointer))
                return false;

            return ((TypePointer)languageType).DataType.IsCompatibleType(DataType);
        }

        //public virtual void AcceptVisitor(IASTNodeAcyclicVisitor visitor)
        //{
        //    if (visitor is IASTNodeAcyclicVisitor<TypePointer>)
        //        ((IASTNodeAcyclicVisitor<TypePointer>)visitor).Visit(this);

        //    //You can write fall back logic here if needed.
        //}

        public virtual string OperatorName
        {
            get { throw new NotImplementedException(); }
        }

        
        public static TypePointer Create(ILanguageType dataType)
        {
            return new TypePointer(dataType);
        }
    }
}
