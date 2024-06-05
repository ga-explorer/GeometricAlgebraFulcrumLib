namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Sequences.Periodic1D;

public interface IPeriodicSequencesAggregate1D<out T> 
    : IPeriodicSequence1D<T> 
{
    IReadOnlyList<IPeriodicSequence1D<T>> BaseSequences { get; }
}