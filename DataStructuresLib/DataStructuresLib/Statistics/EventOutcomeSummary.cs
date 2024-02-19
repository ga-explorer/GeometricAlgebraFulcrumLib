using System;
using System.Text;

namespace DataStructuresLib.Statistics;

public sealed class EventOutcomeSummary
{
    public string Description { get; set; }
        = string.Empty;

    public ulong Count { get; internal set; }

    public TimeSpan Duration { get; internal set; }
        = TimeSpan.Zero;


    internal void Reset()
    {
        Count = 0;
        Duration = TimeSpan.Zero;
    }


    public override string ToString()
    {
        return new StringBuilder()
            .Append(Description)
            .Append(": ")
            .Append(Count.ToString("###,###,###,###,###,###,##0"))
            .Append(", ")
            .Append(Duration.ToString("G"))
            .ToString();
    }
}