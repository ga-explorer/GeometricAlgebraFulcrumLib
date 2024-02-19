using System;
using System.Collections.Generic;

namespace TextComposerLib.Loggers.Progress;

public enum ProgressComposerStatus
{
    /// <summary>
    /// The operation is not running
    /// </summary>
    NotRunning, 

    /// <summary>
    /// The operation is running
    /// </summary>
    Running, 

    /// <summary>
    /// The operation is running and a stop request is in place
    /// </summary>
    RunningRequestStop
}

/// <summary>
/// This class can be used report progress of long processes.
/// </summary>
public sealed class ProgressComposer
{
    public event ProgressEventHandler ProgressEvent;

    public event ErrorEventHandler ErrorEvent;


    internal int NextProgressId;

    internal readonly Dictionary<int, ProgressEventArgs> StartedProcesses = 
        new Dictionary<int, ProgressEventArgs>();


    /// <summary>
    /// The status of the operation being monitored for progress
    /// </summary>
    public ProgressComposerStatus Status { get; internal set; }

    /// <summary>
    /// If false, calles to reporting methods will not be rigistered or raise any events
    /// </summary>
    public bool Enabled { get; set; }

    /// <summary>
    /// This progress composer is automatically disabled after the next report
    /// </summary>
    public bool DisableAfterNextReport { get; set; }

    /// <summary>
    /// If this member is enabled, it rigisters the progress history as ProgressEventArgs objects
    /// </summary>
    public ProgressComposerHistory History { get; private set; }


    public ProgressComposer()
    {
        Enabled = true;
        Status = ProgressComposerStatus.NotRunning;
        History = new ProgressComposerHistory();
    }


    private void RegisterProgress(ProgressEventArgs eventArgs, int startProgressId)
    {
        if (eventArgs.Kind == ProgressEventArgsKind.Start)
        {
            StartedProcesses.Add(eventArgs.ProgressId, eventArgs);
        }
        else if (eventArgs.Kind == ProgressEventArgsKind.Finish)
        {
            if (StartedProcesses.TryGetValue(startProgressId, out var startedProcess))
            {
                //startedProcess.FinishTime = eventArgs.FinishTime;
                eventArgs.StartTime = startedProcess.StartTime;
                eventArgs.Title = startedProcess.Title;

                StartedProcesses.Remove(startProgressId);
            }
        }

        History.AddProgressEvent(eventArgs);

        if (DisableAfterNextReport) Enabled = false;
    }

    internal int OnProgressEvent(ProgressEventArgs eventArgs, int startProgressId = -1)
    {
        RegisterProgress(eventArgs, startProgressId);

        var handler = ProgressEvent;

        if (ReferenceEquals(handler, null))
            return eventArgs.ProgressId;

        handler(this, eventArgs);

        return eventArgs.ProgressId;
    }

    internal int OnErrorEvent(ErrorEventArgs eventArgs)
    {
        RegisterProgress(eventArgs, -1);

        var handler = ErrorEvent;

        if (ReferenceEquals(handler, null))
            return eventArgs.ProgressId;

        handler(this, eventArgs);

        return eventArgs.ProgressId;
    }


    public ProgressComposer Reset(bool enabled = true)
    {
        NextProgressId = 0;

        Status = ProgressComposerStatus.NotRunning;

        StartedProcesses.Clear();

        History = new ProgressComposerHistory { Enabled = History.Enabled };

        return this;
    }


    /// <summary>
    /// This method should be called at any desired point in the monitored process to raise an 
    /// exception and interrupt progress if the status of the progress composer is changed to 
    /// RunningRequestStop earlier to fulfill the stop request.
    /// </summary>
    public void CheckRequestStop()
    {
        if (Status == ProgressComposerStatus.RunningRequestStop)
        {
            Status = ProgressComposerStatus.NotRunning;
            throw new OperationCanceledException();
        }
    }

    /// <summary>
    /// Request for the process to stop running as soon as possible. Later calles to CheckRequestStop()
    /// will raise an exception to fulfil the stop request
    /// </summary>
    public void RequestStop()
    {
        if (Status != ProgressComposerStatus.Running) return;

        Status = ProgressComposerStatus.RunningRequestStop;
    }
}