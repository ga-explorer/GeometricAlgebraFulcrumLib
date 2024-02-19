using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataStructuresLib;

namespace TextComposerLib.Loggers.EventLog;

public sealed class EventLogComposerPeriod
{
    private static readonly IntegerSequenceGenerator SeqGen = new IntegerSequenceGenerator();


    public int EventId { get; }

    public string EventName { get; }

    public EventLogComposerPeriod ParentEvent { get; internal set; }

    public DateTime Start { get; }

    public DateTime End { get; private set; }

    public bool EventEnded { get; private set; }

    private List<EventLogComposerPeriod> _subEvents;


    public TimeSpan Span => End - Start;

    public bool HasParentEvent => ParentEvent != null;

    public bool HasSubEvents => _subEvents != null && _subEvents.Count > 0;

    public int SubEventsCount => _subEvents?.Count ?? 0;

    public IEnumerable<EventLogComposerPeriod> SubEvents => _subEvents ?? Enumerable.Empty<EventLogComposerPeriod>();


    internal EventLogComposerPeriod(string eventName)
    {
        Start = DateTime.Now;

        EventId = SeqGen.GetNewCountId();

        EventName = eventName;
    }


    internal void AddSubEvent(EventLogComposerPeriod subEvent)
    {
        if (_subEvents == null)
            _subEvents = new List<EventLogComposerPeriod>();

        _subEvents.Add(subEvent);
    }

    private void EndEvent(DateTime endTime)
    {
        if (EventEnded)
            return;

        End = endTime;
        EventEnded = true;

        if (_subEvents == null || _subEvents.Count == 0)
            return;

        foreach (var subEvent in _subEvents)
            subEvent.EndEvent(End);
    }

    internal void EndEvent()
    {
        EndEvent(DateTime.Now);
    }


    public override string ToString()
    {
        var s = new StringBuilder();

        s.Append("<")
            .Append(EventId)
            .Append("> ")
            .Append(EventName)
            .Append(": ")
            .Append(Start.TimeOfDay)
            .Append(" -> ");

        if (EventEnded)
            s.Append(End.TimeOfDay)
                .Append(" = ")
                .AppendLine(Span.ToString());
        else
            s.AppendLine("not ended");

        return s.ToString();
    }
}