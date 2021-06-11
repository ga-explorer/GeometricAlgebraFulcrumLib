namespace DataStructuresLib.Basic
{
    public interface IQuad<out TValue>
    {
        TValue Item1 { get; }

        TValue Item2 { get; }

        TValue Item3 { get; }

        TValue Item4 { get; }
    }
}