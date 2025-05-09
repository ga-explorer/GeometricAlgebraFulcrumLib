namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

public interface IUInt64IndexedValue<out T>
{
    public ulong Index { get; }

    public T Value { get; }
}