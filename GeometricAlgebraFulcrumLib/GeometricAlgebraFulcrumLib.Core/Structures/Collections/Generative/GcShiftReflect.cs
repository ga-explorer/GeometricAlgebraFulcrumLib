namespace GeometricAlgebraFulcrumLib.Core.Structures.Collections.Generative;

public sealed class GcShiftReflect<T> : GenerativeCollection<T>
{
    public static GcShiftReflect<T> Create(GenerativeCollection<T> baseCollection)
    {
        return new GcShiftReflect<T>(baseCollection);
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
    public GenerativeCollection<T> BaseCollection { get; set; }

    public T this[int index]
    {
        get
        {
            if (BaseCollection == null) return DefaultValue;

            return BaseCollection.GetItem(
                IsReflected
                    ? ShiftFactor + index
                    : ShiftFactor - index
            );
        }
    }


    private GcShiftReflect(GenerativeCollection<T> baseCollection)
    {
        ShiftFactor = 0;
        IsReflected = false;
        BaseCollection = baseCollection;
    }


    public override T GetItem(int index)
    {
        if (BaseCollection == null) return DefaultValue;

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
    public GcShiftReflect<T> Reset(GenerativeCollection<T> baseCollection)
    {
        ShiftFactor = 0;
        IsReflected = false;
        BaseCollection = baseCollection;

        return this;
    }

    /// <summary>
    /// Reset the specs of this collection
    /// </summary>
    /// <returns></returns>
    public GcShiftReflect<T> ResetShiftReflect()
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
    public GcShiftReflect<T> ApplyShift(int shiftCount)
    {
        ShiftFactor += shiftCount;
        return this;
    }

    /// <summary>
    /// Apply an additional index reflection on the zero index to the elements 
    /// of the base collection
    /// </summary>
    /// <returns></returns>
    public GcShiftReflect<T> ApplyReflect()
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
    public GcShiftReflect<T> ApplyReflect(int reflectionCenter)
    {
        ShiftFactor = 2 * reflectionCenter - ShiftFactor;
        IsReflected = !IsReflected;

        return this;
    }

}