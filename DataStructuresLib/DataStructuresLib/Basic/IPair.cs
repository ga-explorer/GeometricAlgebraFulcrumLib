namespace DataStructuresLib.Basic
{
    public interface IPair<out TValue>
    {
        TValue Item1 { get; }

        TValue Item2 { get; }
    }
}