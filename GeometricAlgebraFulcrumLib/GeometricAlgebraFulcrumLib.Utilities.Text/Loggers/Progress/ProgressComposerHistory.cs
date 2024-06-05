using System.Text;

namespace GeometricAlgebraFulcrumLib.Utilities.Text.Loggers.Progress;

/// <summary>
/// This class can be used to rigister a history of progress events that can be read at any time either
/// fully or partially
/// </summary>
public sealed class ProgressComposerHistory
{
    private readonly List<ProgressEventArgs> _events = new List<ProgressEventArgs>();

    /// <summary>
    /// If false, no new progress events are stored in the history. Old events are not cleared however.
    /// </summary>
    public bool Enabled { get; set; }

    /// <summary>
    /// If true, a read operation on the history clears it after reading
    /// </summary>
    public bool ClearOnRead { get; set; }

    /// <summary>
    /// True if the progress history contains any events of kind error or of result failure
    /// </summary>
    public bool HasErrorsOrFailures
    {
        get
        {
            return _events.Any(
                p => 
                    p.Kind == ProgressEventArgsKind.Error || 
                    p.Result == ProgressEventArgsResult.Failure
            );
        }
    }


    public ProgressComposerHistory()
    {
        Enabled = true;
        ClearOnRead = false;
    }

    /// <summary>
    /// Clears all progress history events
    /// </summary>
    /// <returns></returns>
    public ProgressComposerHistory Clear()
    {
        _events.Clear();

        return this;
    }

    /// <summary>
    /// Reads the full progress events history
    /// </summary>
    /// <returns></returns>
    public List<ProgressEventArgs> ReadHistory()
    {
        var eventsHistory = new List<ProgressEventArgs>(_events);

        if (ClearOnRead) _events.Clear();

        return eventsHistory;
    }

    /// <summary>
    /// Reads the progress events history starting at a given index into an array. 
    /// If the ClearOnRead flag is true the history is cleared and a zero is returned 
    /// else the next event index is returned.
    /// </summary>
    /// <param name="startIndex"></param>
    /// <param name="result"></param>
    /// <returns></returns>
    public int ReadHistory(int startIndex, out ProgressEventArgs[] result)
    {
        var lastIndex = _events.Count - 1;

        if (lastIndex < startIndex)
        {
            result = null;
            return lastIndex + 1;
        }

        result = new ProgressEventArgs[lastIndex - startIndex + 1];

        for (var i = startIndex; i <= lastIndex; i++)
            result[i - startIndex] = _events[i];

        if (ClearOnRead == false) 
            return lastIndex + 1;

        _events.Clear();

        return 0;
    }

    /// <summary>
    /// Reads the progress events history starting at a given index into the end of a list. 
    /// If the ClearOnRead flag is true the history is cleared and a zero is returned 
    /// else the next event index is returned.
    /// </summary>
    /// <param name="startIndex"></param>
    /// <param name="result"></param>
    /// <returns></returns>
    public int ReadHistory(int startIndex, List<ProgressEventArgs> result)
    {
        var lastIndex = _events.Count - 1;

        if (lastIndex < startIndex) return lastIndex + 1;

        for (var i = startIndex; i <= lastIndex; i++)
            result.Add(_events[i]);

        if (ClearOnRead == false) return lastIndex + 1;

        _events.Clear();

        return 0;
    }

    internal ProgressComposerHistory AddProgressEvent(ProgressEventArgs eventArgs)
    {
        if (Enabled == false) return this;

        _events.Add(eventArgs);

        //var eventArgsCopy = new ProgressEventArgs(
        //    eventArgs.StartTime,
        //    eventArgs.Title,
        //    eventArgs.Details,
        //    eventArgs.Kind,
        //    eventArgs.Result
        //    ) { FinishTime = eventArgs.FinishTime };

        //_events.Add(eventArgsCopy);

        return this;
    }

    public override string ToString()
    {
        var s = new StringBuilder();

        foreach (var progressEvent in _events)
            s.AppendLine(progressEvent.ToString());

        return s.ToString();
    }
}