using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace DataStructuresLib.Statistics;

public sealed class BooleanOutcomesEventSummary : IEventSummary
{
    public static BooleanOutcomesEventSummary Create(string name)
    {
        return new BooleanOutcomesEventSummary(name);
    }

    public static BooleanOutcomesEventSummary Create(string name, string description)
    {
        return new BooleanOutcomesEventSummary(name)
        {
            Description = description
        };
    }


    private readonly Stopwatch _stopwatch 
        = new Stopwatch();


    public string Name { get; }

    public string Description { get; set; }
        = string.Empty;

    public EventOutcomeSummary AllOutcomes { get; } 
        = new EventOutcomeSummary()
        {
            Description = "All Outcomes"
        };

    public EventOutcomeSummary TrueOutcomes { get; } 
        = new EventOutcomeSummary()
        {
            Description = "True Outcomes"
        };

    public EventOutcomeSummary FalseOutcomes { get; }
        = new EventOutcomeSummary()
        {
            Description = "False Outcomes"
        };

    public EventOutcomeSummary KnownOutcomes 
        => new EventOutcomeSummary()
        {
            Description = "Known Outcomes",
            Count = TrueOutcomes.Count + FalseOutcomes.Count,
            Duration = TrueOutcomes.Duration + FalseOutcomes.Duration
        };

    public EventOutcomeSummary UnknownOutcomes 
        => new EventOutcomeSummary()
        {
            Description = "Unknown Outcomes",
            Count = AllOutcomes.Count - (TrueOutcomes.Count + FalseOutcomes.Count),
            Duration = AllOutcomes.Duration - (TrueOutcomes.Duration + FalseOutcomes.Duration)
        };

    public bool HasUnknownOutcomes
        => AllOutcomes.Count > (TrueOutcomes.Count + FalseOutcomes.Count);

    public int Count 
        => 3;


    private BooleanOutcomesEventSummary(string name)
    {
        Name = name;
    }


    public void Reset()
    {
        AllOutcomes.Reset();
        TrueOutcomes.Reset();
        FalseOutcomes.Reset();
    }

    public void Begin()
    {
        AllOutcomes.Count++;
        _stopwatch.Restart();
    }

    public void End(bool outcome)
    {
        _stopwatch.Stop();
        AllOutcomes.Duration += _stopwatch.Elapsed;

        if (outcome)
        {
            TrueOutcomes.Count++;
            TrueOutcomes.Duration += _stopwatch.Elapsed;

            return;
        }

        FalseOutcomes.Count++;
        FalseOutcomes.Duration += _stopwatch.Elapsed;
    }

    public void EndWithTrueOutcome()
    {
        _stopwatch.Stop();
        AllOutcomes.Duration += _stopwatch.Elapsed;

        TrueOutcomes.Count++;
        TrueOutcomes.Duration += _stopwatch.Elapsed;
    }

    public void EndWithFalseOutcome()
    {
        _stopwatch.Stop();
        AllOutcomes.Duration += _stopwatch.Elapsed;

        FalseOutcomes.Count++;
        FalseOutcomes.Duration += _stopwatch.Elapsed;
    }


    public IEnumerator<EventOutcomeSummary> GetEnumerator()
    {
        yield return AllOutcomes;
        yield return TrueOutcomes;
        yield return FalseOutcomes;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        yield return AllOutcomes;
        yield return TrueOutcomes;
        yield return FalseOutcomes;
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
            .AppendLine(": ");

        foreach (var outcome in this)
        {
            composer
                .Append("  ")
                .Append(outcome.Description)
                .Append(": ")
                .Append(outcome.Count.ToString("###,###,###,###,###,###,##0"))
                .Append(", ")
                .AppendLine(outcome.Duration.ToString("G"));
        }

        return composer.ToString();
    }
}