using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DataStructuresLib.Statistics;

public sealed class EventSummaryCollection 
    : IReadOnlyCollection<IEventSummary>
{
    private readonly List<IEventSummary> _eventsList
        = new List<IEventSummary>();


    public int Count 
        => _eventsList.Count;

    public IEventSummary this[int eventIndex]
        => eventIndex >= 0 && eventIndex < _eventsList.Count 
            ? _eventsList[eventIndex] 
            : null;

    public IEventSummary this[string eventName]
        => _eventsList.FirstOrDefault(e => e.Name == eventName);


    public EventSummaryCollection AddEvent(IEventSummary eventSummary)
    {
        _eventsList.Add(eventSummary);

        return this;
    }

    public EventSummaryCollection AddEvents(params IEventSummary[] eventSummaryList)
    {
        _eventsList.AddRange(eventSummaryList);

        return this;
    }

    public EventSummaryCollection ResetEvents()
    {
        foreach (var eventSummary in _eventsList)
            eventSummary.Reset();

        return this;
    }


    public IEnumerator<IEventSummary> GetEnumerator()
    {
        return _eventsList.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return _eventsList.GetEnumerator();
    }

    public override string ToString()
    {
        return _eventsList
            .Select(v => v.ToString())
            .ConcatenateText(Environment.NewLine);
    }
}