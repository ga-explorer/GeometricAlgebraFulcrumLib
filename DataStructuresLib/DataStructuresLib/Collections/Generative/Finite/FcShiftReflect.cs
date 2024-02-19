using System;

namespace DataStructuresLib.Collections.Generative.Finite;

/// <summary>
/// This class represents an index shift\reflect operation on the elements of a base
/// finite collection
/// </summary>
/// <typeparam name="T"></typeparam>
public sealed class FcShiftReflect<T> : FiniteCollection<T>
{
    /// <summary>
    /// Create a shift\reflect collection with default identity mapping of base
    /// collection elements
    /// </summary>
    /// <param name="baseCollection"></param>
    /// <returns></returns>
    public static FcShiftReflect<T> Create(FiniteCollection<T> baseCollection)
    {
        return new FcShiftReflect<T>(baseCollection);
    }


    /// <summary>
    /// The amount of index shift used in this collection
    /// </summary>
    public int ShiftFactor { get; private set; }

    /// <summary>
    /// The reflection factor, a +1 for no-reflection or -1 for a reflection
    /// </summary>
    public int ReflectFactor => IsReflected ? -1 : 1;

    /// <summary>
    /// True if this collection applies a reflection of the base collection
    /// </summary>
    public bool IsReflected { get; private set; }

    /// <summary>
    /// True if this collection applies a shift of the base collection
    /// </summary>
    public bool IsShifted => ShiftFactor != 0;

    /// <summary>
    /// The base collection
    /// </summary>
    public FiniteCollection<T> BaseCollection { get; private set; }

    public override int Count => BaseCollection.Count;

    public override int MinIndex =>
        IsReflected
            ? ShiftFactor - BaseCollection.MaxIndex
            : ShiftFactor + BaseCollection.MinIndex;

    public override int MaxIndex =>
        IsReflected
            ? ShiftFactor - BaseCollection.MinIndex
            : ShiftFactor + BaseCollection.MaxIndex;


    private FcShiftReflect(FiniteCollection<T> baseCollection)
    {
        if (baseCollection == null)
            throw new ArgumentNullException(nameof(baseCollection));

        ShiftFactor = 0;
        IsReflected = false;
        BaseCollection = baseCollection;
    }


    public override T GetItem(int index)
    {
        return BaseCollection.GetItem(
            IsReflected
                ? ShiftFactor + index
                : ShiftFactor - index
        );
    }

    /// <summary>
    /// Reset the specs of this collection
    /// </summary>
    /// <param name="baseCollection"></param>
    /// <returns></returns>
    public FcShiftReflect<T> Reset(FiniteCollection<T> baseCollection)
    {
        if (baseCollection == null)
            throw new ArgumentNullException(nameof(baseCollection));

        ShiftFactor = 0;
        IsReflected = false;
        BaseCollection = baseCollection;

        return this;
    }

    /// <summary>
    /// Reset the specs of this collection
    /// </summary>
    /// <returns></returns>
    public FcShiftReflect<T> ResetShiftReflect()
    {
        ShiftFactor = 0;
        IsReflected = false;

        return this;
    }

    /// <summary>
    /// Apply an additional index shift to the elements of the base collection
    /// </summary>
    /// <param name="shiftCount"></param>
    /// <returns></returns>
    public FcShiftReflect<T> ApplyShift(int shiftCount)
    {
        ShiftFactor += shiftCount;
        return this;
    }

    /// <summary>
    /// Apply an additional index reflection on the zero index to the elements 
    /// of the base collection
    /// </summary>
    /// <returns></returns>
    public FcShiftReflect<T> ApplyReflect()
    {
        ShiftFactor = -ShiftFactor;
        IsReflected = !IsReflected;

        return this;
    }

    /// <summary>
    /// Apply an additional index reflection on the given index to the elements 
    /// of the base collection
    /// </summary>
    /// <param name="reflectionCenter"></param>
    /// <returns></returns>
    public FcShiftReflect<T> ApplyReflect(int reflectionCenter)
    {
        ShiftFactor = 2 * reflectionCenter - ShiftFactor;
        IsReflected = !IsReflected;

        return this;
    }
}