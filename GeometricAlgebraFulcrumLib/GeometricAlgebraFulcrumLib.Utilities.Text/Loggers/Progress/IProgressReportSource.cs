namespace GeometricAlgebraFulcrumLib.Utilities.Text.Loggers.Progress;

public interface IProgressReportSource
{
    /// <summary>
    /// The ID of this class as a progress reporting source
    /// </summary>
    string ProgressSourceId { get; }

    /// <summary>
    /// The progress composer object used for reporting progress events on this class
    /// </summary>
    ProgressComposer Progress { get; }
}