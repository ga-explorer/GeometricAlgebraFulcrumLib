﻿using GeometricAlgebraFulcrumLib.Core.Structures.Collections.Generative.Finite.Natural;

namespace GeometricAlgebraFulcrumLib.Core.Structures.Collections.Generative.Finite;

/// <summary>
/// This class represents an even symmetric finite collection based on a base
/// natural finite collection of elements
/// </summary>
/// <typeparam name="T"></typeparam>
public sealed class FcEven<T> : FiniteCollection<T>
{
    /// <summary>
    /// Create an even symmetric finite collection
    /// </summary>
    /// <param name="baseCollection"></param>
    /// <returns></returns>
    public static FcEven<T> Create(NaturalFiniteCollection<T> baseCollection)
    {
        return new FcEven<T>(baseCollection);
    }


    /// <summary>
    /// The base natural collection of this even collection
    /// </summary>
    public NaturalFiniteCollection<T> BaseCollection { get; private set; }

    public override int Count => BaseCollection.Count * 2 - 1;

    public override int MinIndex => -BaseCollection.MaxIndex;

    public override int MaxIndex => BaseCollection.MaxIndex;

    public T this[int index] =>
        BaseCollection == null || index > BaseCollection.MaxIndex
            ? DefaultValue
            : BaseCollection.GetItem(index >= 0 ? index : -index);


    private FcEven(NaturalFiniteCollection<T> baseCollection)
    {
        BaseCollection = baseCollection;
    }


    public override T GetItem(int index)
    {
        return BaseCollection == null || index > BaseCollection.MaxIndex
            ? DefaultValue
            : BaseCollection.GetItem(index >= 0 ? index : -index);
    }

    /// <summary>
    /// Reset the specs of this collection
    /// </summary>
    /// <param name="baseCollection"></param>
    /// <returns></returns>
    public FcEven<T> Reset(NaturalFiniteCollection<T> baseCollection)
    {
        BaseCollection = baseCollection;

        return this;
    }
}