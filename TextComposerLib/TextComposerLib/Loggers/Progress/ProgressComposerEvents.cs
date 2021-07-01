using System;
using System.Text;
using TextComposerLib.Text.Linear;

namespace TextComposerLib.Loggers.Progress
{
    public enum ProgressEventArgsKind { Normal, Start, Finish, Error, Warning }

    public enum ProgressEventArgsResult { Normal, Success, Failure }


    public class ProgressEventArgs : EventArgs
    {
        public int ProgressId { get; }

        public DateTime StartTime { get; internal set; }

        public DateTime FinishTime { get; internal set; }

        public IProgressReportSource Source { get; internal set; }

        public string SourceId => Source.ProgressSourceId;

        public string Title { get; internal set; }

        public string Details { get; internal set; }

        public ProgressEventArgsKind Kind { get; }

        public ProgressEventArgsResult Result { get; }

        public TimeSpan Duration => FinishTime - StartTime;

        public string StartTimeText => StartTime.ToString("O");

        public string FinishTimeText => StartTime.ToString("O");

        public string DurationText => StartTime == FinishTime ? String.Empty : Duration.ToString("G");

        public string KindText
        {
            get
            {
                switch (Kind)
                {
                    case ProgressEventArgsKind.Start:
                        return "Start";

                    case ProgressEventArgsKind.Finish:
                        return "Finish";

                    case ProgressEventArgsKind.Error:
                        return "Error";

                    case ProgressEventArgsKind.Warning:
                        return "Warning";

                    default:
                        return String.Empty;
                }
            }
        }

        public string ResultText
        {
            get
            {
                switch (Result)
                {
                    case ProgressEventArgsResult.Success:
                        return "Seccess";

                    case ProgressEventArgsResult.Failure:
                        return "Failure";

                    default:
                        return String.Empty;
                }
            }
        }

        public string FullTitle
        {
            get
            {
                var s = new StringBuilder();

                s.Append(Source).Append(": ");

                if (Result != ProgressEventArgsResult.Normal && Kind != ProgressEventArgsKind.Normal)
                    s.Append("<").Append(KindText).Append(", ").Append(ResultText).Append("> ");

                else if (Result != ProgressEventArgsResult.Normal)
                    s.Append("<").Append(ResultText).Append("> ");

                if (Kind != ProgressEventArgsKind.Normal)
                    s.Append("<").Append(KindText).Append("> ");

                s.Append(Title);

                return s.ToString();
            }
        }

        public string FullTimingText
        {
            get
            {
                var s = new StringBuilder();

                if (StartTime == FinishTime)
                    s.Append(StartTimeText);

                else
                    s.Append(StartTimeText)
                        .Append(" => ")
                        .Append(FinishTimeText)
                        .Append(" < ")
                        .Append(DurationText)
                        .Append(" >");

                return s.ToString();
            }
        }


        internal ProgressEventArgs(int progressId, DateTime startTime, IProgressReportSource source, string title, string details, ProgressEventArgsKind kind, ProgressEventArgsResult result)
        {
            ProgressId = progressId;
            StartTime = startTime;
            FinishTime = startTime;
            Source = source;
            Title = title;
            Details = details;
            Kind = kind;
            Result = result;
        }


        public override string ToString()
        {
            var s = new LinearTextComposer();

            s.Append(ProgressId.ToString("D6")).Append(": ").Append(FullTimingText);

            s.AppendLineAtNewLine(FullTitle);

            if (String.IsNullOrEmpty(Details) == false)
                s.AppendLineAtNewLine()
                    .AppendLineAtNewLine("Begin Details")
                    .IncreaseIndentation()
                    .AppendAtNewLine(Details)
                    .DecreaseIndentation()
                    .AppendLineAtNewLine("End Details");

            return s.ToString();
        }
    }

    public delegate void ProgressEventHandler(object sender, ProgressEventArgs eventArgs);


    public sealed class ErrorEventArgs : ProgressEventArgs
    {
        public Exception Error { get; private set; }


        internal ErrorEventArgs(int progressId, DateTime startTime, Exception error, IProgressReportSource source)
            : base(progressId, startTime, source, "Error", error.ToString(), ProgressEventArgsKind.Error, ProgressEventArgsResult.Normal)
        {
            Error = error;
        }

        internal ErrorEventArgs(int progressId, DateTime startTime, Exception error, IProgressReportSource source, string title, string details)
            : base(progressId, startTime, source, title, details, ProgressEventArgsKind.Error, ProgressEventArgsResult.Normal)
        {
            Error = error;
        }
    }

    public delegate void ErrorEventHandler(object sender, ErrorEventArgs eventArgs);
}
