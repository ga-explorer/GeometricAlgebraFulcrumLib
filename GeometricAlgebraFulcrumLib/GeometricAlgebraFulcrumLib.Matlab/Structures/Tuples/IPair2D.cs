namespace GeometricAlgebraFulcrumLib.Matlab.Structures.Tuples
{
    public interface IPair2D<out TValue>
    {
        TValue Item11 { get; }

        TValue Item12 { get; }

        TValue Item21 { get; }

        TValue Item22 { get; }
    }
}