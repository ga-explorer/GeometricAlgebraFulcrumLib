using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace DataStructuresLib.Statistics;

public sealed class SingleOutcomeEventSummary : IEventSummary
{
    public static SingleOutcomeEventSummary Create(string name)
    {
        return new SingleOutcomeEventSummary(name);
    }

    public static SingleOutcomeEventSummary Create(string name, string description)
    {
        return new SingleOutcomeEventSummary(name)
        {
            Description = description
        };
    }


    private readonly Stopwatch _stopwatch 
        = new Stopwatch();


    public string Name { get; }

    public string Description
    {
        get => Outcomes.Description;
        set => Outcomes.Description = value;
    }

    public EventOutcomeSummary Outcomes { get; } 
        = new EventOutcomeSummary();

    public int Count 
        => 1;


    private SingleOutcomeEventSummary(string name)
    {
        Name = name;
    }


    public void Reset()
    {
        Outcomes.Reset();
    }

    public void Begin()
    {
        Outcomes.Count++;
        _stopwatch.Restart();
    }

    public void End()
    {
        _stopwatch.Stop();
        Outcomes.Duration += _stopwatch.Elapsed;
    }

        
    public IEnumerator<EventOutcomeSummary> GetEnumerator()
    {
        yield return Outcomes;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        yield return Outcomes;
    }

        
    public override string ToString()
    {
        var composer = new StringBuilder();

        composer
            .Append(
                string.IsNullOrEmpty(Description)
                    ? Name
                    : Description
            )
            .Append(": ")
            .Append(Outcomes.Count.ToString("###,###,###,###,###,###,##0"))
            .Append(", ")
            .AppendLine(Outcomes.Duration.ToString("G"));

        return composer.ToString();
    }
}