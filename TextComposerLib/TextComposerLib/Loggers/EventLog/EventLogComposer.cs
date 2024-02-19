using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TextComposerLib.Loggers.EventLog;

public sealed class EventLogComposer
{
    private EventLogComposerPeriod _activeEvent;

    private readonly List<EventLogComposerPeriod> _rootEvents =
        new List<EventLogComposerPeriod>();

    private readonly Dictionary<int, EventLogComposerPeriod> _events =
        new Dictionary<int, EventLogComposerPeriod>();



    public void Clear()
    {
        _activeEvent = null;
        _rootEvents.Clear();
        _events.Clear();
    }

    public IEnumerable<EventLogComposerPeriod> Events()
    {
        return _events.Select(pair => pair.Value);
    }

    public IEnumerable<string> EventNames()
    {
        return _events.Select(pair => pair.Value.EventName).Distinct();
    }

    public Dictionary<string, TimeSpan> RootEventsSpan()
    {
        var result = new Dictionary<string, TimeSpan>();

        var rootEventNames =
            _events.Select(pair => pair.Value.EventName).Distinct();

        foreach (var rootEventName in rootEventNames)
        {
            var rootEvents = RootEvents(rootEventName);

            result.Add(
                rootEventName, 
                rootEvents.Aggregate(
                    new TimeSpan(), 
                    (accum, rootEvent) => accum + rootEvent.Span
                )
            );
        }

        return result;
    }

    public string RootEventsSpanToString()
    {
        var dict = RootEventsSpan();

        var s = new StringBuilder();

        foreach (var pair in dict)
            s.Append(pair.Key)
                .Append(": ")
                .AppendLine(pair.Value.ToString());

        return s.ToString();
    }

    public IEnumerable<EventLogComposerPeriod> Events(string eventName)
    {
        return 
            _events
                .Where(pair => pair.Value.EventName == eventName)
                .Select(pair => pair.Value);
    }

    public IEnumerable<EventLogComposerPeriod> ActiveEvents()
    {
        return 
            _events
                .Where(pair => ! pair.Value.EventEnded)
                .Select(pair => pair.Value);
    }

    public IEnumerable<EventLogComposerPeriod> RootEvents()
    {
        return _rootEvents;
    }

    public IEnumerable<EventLogComposerPeriod> RootEvents(string eventName)
    {
        foreach (var rootEvent in _rootEvents)
        {
            var stack = new Stack<EventLogComposerPeriod>();

            stack.Push(rootEvent);

            while (stack.Count > 0)
            {
                var eventPeriod = stack.Pop();

                if (eventPeriod.EventName == eventName)
                    yield return eventPeriod;

                else
                {
                    if (!eventPeriod.HasSubEvents)
                        continue;

                    foreach (var subEvent in eventPeriod.SubEvents)
                        stack.Push(subEvent);
                }
            }
        }
    }


    public int StartEvent(string eventName)
    {
        var eventPeriod = new EventLogComposerPeriod(eventName);

        _events.Add(eventPeriod.EventId, eventPeriod);

        if (_activeEvent == null)
        {
            _rootEvents.Add(eventPeriod);
        }
        else
        {
            eventPeriod.ParentEvent = _activeEvent;

            _activeEvent.AddSubEvent(eventPeriod);
        }

        _activeEvent = eventPeriod;

        return eventPeriod.EventId;
    }

    public EventLogComposerPeriod EndEvent(int id)
    {
        var eventPeriod = _events[id];

        eventPeriod.EndEvent();

        _activeEvent = eventPeriod.ParentEvent;

        return eventPeriod;
    }

    public override string ToString()
    {
        var s = new StringBuilder();

        foreach (var pair in _events)
            s.Append(pair.Value);

        return s.ToString();
    }
}