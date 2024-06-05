namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Statistics;

public interface IEventSummary : IReadOnlyCollection<EventOutcomeSummary>
{
    string Name { get; }

    string Description { get; }

    void Reset();
}