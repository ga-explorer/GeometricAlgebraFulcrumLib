using System;
using System.Collections.Generic;
using System.Text;

namespace GeometricAlgebraFulcrumLib.Utilities.Code.Irony.Semantic.Type;

/// <summary>
/// This class represents an ordered n-tuple of other language types
/// </summary>
public class TypeTuple : ILanguageType
{
    /// <summary>
    /// The list of items types of the tuple
    /// </summary>
    public List<ILanguageType> TupleItemsTypes { get; } = new List<ILanguageType>();

    /// <summary>
    /// The number of items in the tuple
    /// </summary>
    public int TupleSize => TupleItemsTypes.Count;

    /// <summary>
    /// The parent DSL for this typle type
    /// </summary>
    public IronyAst RootAst => TupleItemsTypes[0].RootAst;


    protected TypeTuple(IEnumerable<ILanguageType> itemsTypes)
    {
        TupleItemsTypes.AddRange(itemsTypes);

        if (TupleItemsTypes.Count < 1)
            throw new InvalidOperationException("A tuple type must contain at lest one item");
    }


    public virtual string TypeSignature
    {
        get 
        {
            var s = new StringBuilder();

            s.Append("tuple(");

            if (TupleItemsTypes.Count > 0)
            {
                foreach (var itemType in TupleItemsTypes)
                    s.Append(itemType.TypeSignature).Append(", ");

                s.Length -= 2;
            }

            s.Append(")");

            return s.ToString();
        }
    }

    public virtual bool IsSameType(ILanguageType languageType)
    {
        if (!(languageType is TypeTuple))
            return false;

        var typeTuple = (TypeTuple)languageType;

        if (typeTuple.TupleSize != TupleSize)
            return false;

        for (var i = 0; i < TupleSize; i++)
            if (typeTuple.TupleItemsTypes[i].IsSameType(TupleItemsTypes[i]) == false)
                return false;

        return true;
    }

    public virtual bool IsCompatibleType(ILanguageType languageType)
    {
        if (!(languageType is TypeTuple))
            return false;

        var typeTuple = (TypeTuple)languageType;

        if (typeTuple.TupleSize != TupleSize)
            return false;

        for (var i = 0; i < TupleSize; i++)
            if (typeTuple.TupleItemsTypes[i].IsCompatibleType(TupleItemsTypes[i]) == false)
                return false;

        return true;
    }

    //public virtual void AcceptVisitor(IASTNodeAcyclicVisitor visitor)
    //{
    //    if (visitor is IASTNodeAcyclicVisitor<TypeTuple>)
    //        ((IASTNodeAcyclicVisitor<TypeTuple>)visitor).Visit(this);

    //    //You can write fall back logic here if needed.
    //}


    public static TypeTuple Create(IEnumerable<ILanguageType> itemsTypes)
    {
        return new TypeTuple(itemsTypes);
    }
}