using System.Collections.Generic;

namespace DataStructuresLib.Statistics;

public interface IEventSummary : IReadOnlyCollection<EventOutcomeSummary>
{
    string Name { get; }

    string Description { get; }

    void Reset();
}