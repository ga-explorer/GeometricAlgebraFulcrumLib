namespace GeometricAlgebraFulcrumLib.Utilities.Text.Loggers.Progress;

public static class ProgressUtils
{
    /// <summary>
    /// Reset the progress object if not null
    /// </summary>
    /// <param name="source"></param>
    public static void ResetProgress(this IProgressReportSource source)
    {
        if (ReferenceEquals(source.Progress, null) == false)
            source.Progress.Reset();
    }

    /// <summary>
    /// Returns true if the given progress conposer is not null and enabled
    /// </summary>
    /// <param name="progress"></param>
    /// <returns></returns>
    public static bool IsReady(this ProgressComposer progress)
    {
        return ReferenceEquals(progress, null) == false && progress.Enabled;
    }

    /// <summary>
    /// True if the progress object is not null and has a status of Running
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static bool IsProgressStatusRunning(this IProgressReportSource source)
    {
        return 
            ReferenceEquals(source.Progress, null) == false && 
            source.Progress.Status == ProgressComposerStatus.Running;
    }

    /// <summary>
    /// True if the progress object is not null and has a status of NotRunning
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static bool IsProgressStatusNotRunning(this IProgressReportSource source)
    {
        return
            ReferenceEquals(source.Progress, null) == false &&
            source.Progress.Status == ProgressComposerStatus.NotRunning;
    }

    /// <summary>
    /// True if the progress object is not null and has a status of RunningRequestStop
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static bool IsProgressStatusRunningRequestStop(this IProgressReportSource source)
    {
        return
            ReferenceEquals(source.Progress, null) == false &&
            source.Progress.Status == ProgressComposerStatus.RunningRequestStop;
    }

    /// <summary>
    /// Set the progress object's status to Running if possible and return true if successful. 
    /// If the progress object is null or has any status other than NotRunning this method returns false
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static bool SetProgressRunning(this IProgressReportSource source)
    {
        if (ReferenceEquals(source.Progress, null))
            return false;

        source.Progress.Status = ProgressComposerStatus.Running;

        return true;
    }

    /// <summary>
    /// Set the progress object's status to NotRunning if possible and return true if successful. 
    /// If the progress object is null or has status NotRunning this method returns false
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static bool SetProgressNotRunning(this IProgressReportSource source)
    {
        if (ReferenceEquals(source.Progress, null))
            return false;

        source.Progress.Status = ProgressComposerStatus.NotRunning;

        return true;
    }



    /// <summary>
    /// Try change the status of the progress object into RunningRequestStop if possible and return true
    /// if successful
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static bool RequestProgressStop(this IProgressReportSource source)
    {
        if (ReferenceEquals(source.Progress, null)) return false;

        source.Progress.RequestStop();

        return source.Progress.Status == ProgressComposerStatus.RunningRequestStop;
    }

    /// <summary>
    /// Check if the progress object's status is RunningRequestStop and raise an exception to interrupt 
    /// running process if so.
    /// </summary>
    /// <param name="source"></param>
    public static void CheckProgressRequestStop(this IProgressReportSource source)
    {
        if (ReferenceEquals(source.Progress, null)) return;

        source.Progress.CheckRequestStop();
    }


    /// <summary>
    /// Reports the start of a process with normal result
    /// </summary>
    /// <param name="source"></param>
    /// <param name="title"></param>
    /// <param name="details"></param>
    /// <returns>An ID of the started progress event to be used when calling ReportProcessFinish() later</returns>
    public static int ReportStart(this IProgressReportSource source, string title, string details = "")
    {
        if (ReferenceEquals(source.Progress, null) || source.Progress.Enabled == false) return -1;

        var eventArgs =
            new ProgressEventArgs(
                source.Progress.NextProgressId++,
                DateTime.Now,
                source,
                title,
                details,
                ProgressEventArgsKind.Start,
                ProgressEventArgsResult.Normal
            );

        return source.Progress.OnProgressEvent(eventArgs);
    }

    public static int ReportStart<T>(this IProgressReportSource source, string title, T details)
    {
        if (ReferenceEquals(source.Progress, null) || source.Progress.Enabled == false) return -1;

        var eventArgs =
            new ProgressEventArgs(
                source.Progress.NextProgressId++,
                DateTime.Now,
                source,
                title,
                details.ToString(),
                ProgressEventArgsKind.Start,
                ProgressEventArgsResult.Normal
            );

        return source.Progress.OnProgressEvent(eventArgs);
    }

    public static int ReportStart(this IProgressReportSource source, string title, Func<string> details)
    {
        if (ReferenceEquals(source.Progress, null) || source.Progress.Enabled == false) return -1;

        var eventArgs =
            new ProgressEventArgs(
                source.Progress.NextProgressId++,
                DateTime.Now,
                source,
                title,
                details(),
                ProgressEventArgsKind.Start,
                ProgressEventArgsResult.Normal
            );

        return source.Progress.OnProgressEvent(eventArgs);
    }

    /// <summary>
    /// Reports the end of a process
    /// </summary>
    /// <param name="progressStartId"></param>
    /// <param name="source"></param>
    /// <param name="details"></param>
    /// <param name="result"></param>
    /// <returns>An ID of the progress event</returns>
    public static int ReportFinish(this IProgressReportSource source, int progressStartId, string details, ProgressEventArgsResult result = ProgressEventArgsResult.Normal)
    {
        if (ReferenceEquals(source.Progress, null) || source.Progress.Enabled == false) return -1;

        var eventArgs =
            new ProgressEventArgs(
                source.Progress.NextProgressId++,
                DateTime.Now,
                source,
                "",
                details,
                ProgressEventArgsKind.Finish, result
            );

        return source.Progress.OnProgressEvent(eventArgs, progressStartId);
    }

    public static int ReportFinish<T>(this IProgressReportSource source, int progressStartId, T details, ProgressEventArgsResult result = ProgressEventArgsResult.Normal)
    {
        if (ReferenceEquals(source.Progress, null) || source.Progress.Enabled == false) return -1;

        var eventArgs =
            new ProgressEventArgs(
                source.Progress.NextProgressId++,
                DateTime.Now,
                source,
                "",
                details.ToString(),
                ProgressEventArgsKind.Finish, result
            );

        return source.Progress.OnProgressEvent(eventArgs, progressStartId);
    }

    public static int ReportFinish(this IProgressReportSource source, int progressStartId, Func<string> details, ProgressEventArgsResult result = ProgressEventArgsResult.Normal)
    {
        if (ReferenceEquals(source.Progress, null) || source.Progress.Enabled == false) return -1;

        var eventArgs =
            new ProgressEventArgs(
                source.Progress.NextProgressId++,
                DateTime.Now,
                source,
                "",
                details(),
                ProgressEventArgsKind.Finish, result
            );

        return source.Progress.OnProgressEvent(eventArgs, progressStartId);
    }

    /// <summary>
    /// Reports the end of a process
    /// </summary>
    /// <param name="progressStartId"></param>
    /// <param name="source"></param>
    /// <param name="result"></param>
    /// <returns></returns>
    public static int ReportFinish(this IProgressReportSource source, int progressStartId, ProgressEventArgsResult result = ProgressEventArgsResult.Normal)
    {
        if (ReferenceEquals(source.Progress, null) || source.Progress.Enabled == false) return -1;

        var eventArgs =
            new ProgressEventArgs(
                source.Progress.NextProgressId++,
                DateTime.Now,
                source,
                "",
                "",
                ProgressEventArgsKind.Finish, result
            );

        return source.Progress.OnProgressEvent(eventArgs, progressStartId);
    }


    /// <summary>
    /// Reports normal progress
    /// </summary>
    /// <param name="source"></param>
    /// <param name="title"></param>
    /// <param name="details"></param>
    /// <param name="result"></param>
    /// <returns>An ID of the progress event</returns>
    public static int ReportNormal(this IProgressReportSource source, string title, string details, ProgressEventArgsResult result = ProgressEventArgsResult.Normal)
    {
        if (ReferenceEquals(source.Progress, null) || source.Progress.Enabled == false) return -1;

        var eventArgs =
            new ProgressEventArgs(
                source.Progress.NextProgressId++,
                DateTime.Now,
                source,
                title,
                details,
                ProgressEventArgsKind.Normal,
                result
            );

        return source.Progress.OnProgressEvent(eventArgs);
    }

    public static int ReportNormal<T>(this IProgressReportSource source, string title, T details, ProgressEventArgsResult result = ProgressEventArgsResult.Normal)
    {
        if (ReferenceEquals(source.Progress, null) || source.Progress.Enabled == false) return -1;

        var eventArgs =
            new ProgressEventArgs(
                source.Progress.NextProgressId++,
                DateTime.Now,
                source,
                title,
                details.ToString(),
                ProgressEventArgsKind.Normal,
                result
            );

        return source.Progress.OnProgressEvent(eventArgs);
    }

    public static int ReportNormal(this IProgressReportSource source, string title, Func<string> details, ProgressEventArgsResult result = ProgressEventArgsResult.Normal)
    {
        if (ReferenceEquals(source.Progress, null) || source.Progress.Enabled == false) return -1;

        var eventArgs =
            new ProgressEventArgs(
                source.Progress.NextProgressId++,
                DateTime.Now,
                source,
                title,
                details(),
                ProgressEventArgsKind.Normal,
                result
            );

        return source.Progress.OnProgressEvent(eventArgs);
    }

    /// <summary>
    /// Reports normal progress
    /// </summary>
    /// <param name="source"></param>
    /// <param name="title"></param>
    /// <param name="result"></param>
    /// <returns></returns>
    public static int ReportNormal(this IProgressReportSource source, string title, ProgressEventArgsResult result = ProgressEventArgsResult.Normal)
    {
        if (ReferenceEquals(source.Progress, null) || source.Progress.Enabled == false) return -1;

        var eventArgs =
            new ProgressEventArgs(
                source.Progress.NextProgressId++,
                DateTime.Now,
                source,
                title,
                "",
                ProgressEventArgsKind.Normal,
                result
            );

        return source.Progress.OnProgressEvent(eventArgs);
    }

    /// <summary>
    /// Reports normal progress
    /// </summary>
    /// <param name="source"></param>
    /// <param name="title"></param>
    /// <param name="isSuccess"></param>
    /// <returns></returns>
    public static int ReportNormal(this IProgressReportSource source, string title, bool isSuccess)
    {
        return ReportNormal(source, title, isSuccess ? ProgressEventArgsResult.Success : ProgressEventArgsResult.Failure);
    }

    /// <summary>
    /// Reports normal progress
    /// </summary>
    /// <param name="source"></param>
    /// <param name="title"></param>
    /// <param name="details"></param>
    /// <param name="isSuccess"></param>
    /// <returns></returns>
    public static int ReportNormal(this IProgressReportSource source, string title, string details, bool isSuccess)
    {
        return ReportNormal(source, title, details, isSuccess ? ProgressEventArgsResult.Success : ProgressEventArgsResult.Failure);
    }

    public static int ReportNormal<T>(this IProgressReportSource source, string title, T details, bool isSuccess)
    {
        return ReportNormal(source, title, details, isSuccess ? ProgressEventArgsResult.Success : ProgressEventArgsResult.Failure);
    }

    public static int ReportNormal(this IProgressReportSource source, string title, Func<string> details, bool isSuccess)
    {
        return ReportNormal(source, title, details, isSuccess ? ProgressEventArgsResult.Success : ProgressEventArgsResult.Failure);
    }

    /// <summary>
    /// Reports a warning
    /// </summary>
    /// <param name="source"></param>
    /// <param name="title"></param>
    /// <param name="details"></param>
    /// <param name="result"></param>
    /// <returns>An ID of the progress event</returns>
    public static int ReportWarning(this IProgressReportSource source, string title, string details, ProgressEventArgsResult result = ProgressEventArgsResult.Normal)
    {
        if (ReferenceEquals(source.Progress, null) || source.Progress.Enabled == false) return -1;

        var eventArgs =
            new ProgressEventArgs(
                source.Progress.NextProgressId++,
                DateTime.Now,
                source,
                title,
                details,
                ProgressEventArgsKind.Warning,
                result
            );

        return source.Progress.OnProgressEvent(eventArgs);
    }

    public static int ReportWarning<T>(this IProgressReportSource source, string title, T details, ProgressEventArgsResult result = ProgressEventArgsResult.Normal)
    {
        if (ReferenceEquals(source.Progress, null) || source.Progress.Enabled == false) return -1;

        var eventArgs =
            new ProgressEventArgs(
                source.Progress.NextProgressId++,
                DateTime.Now,
                source,
                title,
                details.ToString(),
                ProgressEventArgsKind.Warning,
                result
            );

        return source.Progress.OnProgressEvent(eventArgs);
    }

    public static int ReportWarning(this IProgressReportSource source, string title, Func<string> details, ProgressEventArgsResult result = ProgressEventArgsResult.Normal)
    {
        if (ReferenceEquals(source.Progress, null) || source.Progress.Enabled == false) return -1;

        var eventArgs =
            new ProgressEventArgs(
                source.Progress.NextProgressId++,
                DateTime.Now,
                source,
                title,
                details(),
                ProgressEventArgsKind.Warning,
                result
            );

        return source.Progress.OnProgressEvent(eventArgs);
    }

    /// <summary>
    /// Reports a warning
    /// </summary>
    /// <param name="source"></param>
    /// <param name="title"></param>
    /// <param name="result"></param>
    /// <returns></returns>
    public static int ReportWarning(this IProgressReportSource source, string title, ProgressEventArgsResult result = ProgressEventArgsResult.Normal)
    {
        if (ReferenceEquals(source.Progress, null) || source.Progress.Enabled == false) return -1;

        var eventArgs =
            new ProgressEventArgs(
                source.Progress.NextProgressId++,
                DateTime.Now,
                source,
                title,
                "",
                ProgressEventArgsKind.Warning,
                result
            );

        return source.Progress.OnProgressEvent(eventArgs);
    }

    /// <summary>
    /// Reports an error
    /// </summary>
    /// <param name="source"></param>
    /// <param name="error"></param>
    /// <returns>An ID of the progress event</returns>
    public static int ReportError(this IProgressReportSource source, Exception error)
    {
        if (ReferenceEquals(source.Progress, null) || source.Progress.Enabled == false) return -1;

        var eventArgs =
            new ErrorEventArgs(
                source.Progress.NextProgressId++,
                DateTime.Now,
                error,
                source
            );

        return source.Progress.OnErrorEvent(eventArgs);
    }

    /// <summary>
    /// Reports an error
    /// </summary>
    /// <param name="source"></param>
    /// <param name="title"></param>
    /// <param name="error"></param>
    /// <param name="details"></param>
    /// <returns>An ID of the progress event</returns>
    public static int ReportError(this IProgressReportSource source, string title, string details, Exception error)
    {
        if (ReferenceEquals(source.Progress, null) || source.Progress.Enabled == false) return -1;

        var eventArgs =
            new ErrorEventArgs(
                source.Progress.NextProgressId++,
                DateTime.Now,
                error,
                source,
                title,
                details
            );

        return source.Progress.OnErrorEvent(eventArgs);
    }

    public static int ReportError<T>(this IProgressReportSource source, string title, T details, Exception error)
    {
        if (ReferenceEquals(source.Progress, null) || source.Progress.Enabled == false) return -1;

        var eventArgs =
            new ErrorEventArgs(
                source.Progress.NextProgressId++,
                DateTime.Now,
                error,
                source,
                title,
                details.ToString()
            );

        return source.Progress.OnErrorEvent(eventArgs);
    }

    public static int ReportError(this IProgressReportSource source, string title, Func<string> details, Exception error)
    {
        if (ReferenceEquals(source.Progress, null) || source.Progress.Enabled == false) return -1;

        var eventArgs =
            new ErrorEventArgs(
                source.Progress.NextProgressId++,
                DateTime.Now,
                error,
                source,
                title,
                details()
            );

        return source.Progress.OnErrorEvent(eventArgs);
    }

    /// <summary>
    /// Reports an error
    /// </summary>
    /// <param name="source"></param>
    /// <param name="title"></param>
    /// <param name="error"></param>
    /// <returns></returns>
    public static int ReportError(this IProgressReportSource source, string title, Exception error)
    {
        if (ReferenceEquals(source.Progress, null) || source.Progress.Enabled == false) return -1;

        var eventArgs =
            new ErrorEventArgs(
                source.Progress.NextProgressId++,
                DateTime.Now,
                error,
                source,
                title,
                error.Message
            );

        return source.Progress.OnErrorEvent(eventArgs);
    }

}