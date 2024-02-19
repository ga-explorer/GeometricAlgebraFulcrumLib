namespace DataStructuresLib.Basic;

public interface ITriplet<out TValue>
{
    TValue Item1 { get; }

    TValue Item2 { get; }

    TValue Item3 { get; }
}