using System;

namespace CodeComposerLib.Irony.Semantic.Type;

/// <summary>
/// This class represents a reference to another type
/// </summary>
public class TypeReference : ILanguageType
{
    /// <summary>
    /// The type of the data that is referenced
    /// </summary>
    public ILanguageType DataType { get; }

    /// <summary>
    /// The parent DSL for this array
    /// </summary>
    public IronyAst RootAst => DataType.RootAst;


    protected TypeReference(ILanguageType dataType)
    {
        DataType = dataType;
    }


    public virtual string TypeSignature => "reference(" + DataType.TypeSignature + ")";

    public virtual bool IsSameType(ILanguageType languageType)
    {
        if (!(languageType is TypeReference))
            return false;

        return ((TypeReference)languageType).DataType.IsSameType(DataType);
    }

    public virtual bool IsCompatibleType(ILanguageType languageType)
    {
        if (!(languageType is TypeReference))
            return false;

        return ((TypeReference)languageType).DataType.IsCompatibleType(DataType);
    }

    //public virtual void AcceptVisitor(IASTNodeAcyclicVisitor visitor)
    //{
    //    if (visitor is IASTNodeAcyclicVisitor<TypeReference>)
    //        ((IASTNodeAcyclicVisitor<TypeReference>)visitor).Visit(this);

    //    //You can write fall back logic here if needed.
    //}

    public virtual string OperatorName
    {
        get { throw new NotImplementedException(); }
    }

        
    public static TypeReference Create(ILanguageType dataType)
    {
        return new TypeReference(dataType);
    }
}