namespace GeometricAlgebraFulcrumLib.Core.Structures.Tuples;

public interface IHexad<out TValue>
{
    TValue Item1 { get; }

    TValue Item2 { get; }

    TValue Item3 { get; }

    TValue Item4 { get; }

    TValue Item5 { get; }
        
    TValue Item6 { get; }
}