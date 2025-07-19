using System;
using System.Collections.Generic;
using System.Text;
using Irony.Parsing;
using GeometricAlgebraFulcrumLib.Utilities.Text.Loggers.Progress;

namespace GeometricAlgebraFulcrumLib.Utilities.Code.Irony.SourceCode;

public sealed class LanguageCompilationLog : IProgressReportSource
{
    public string ProgressSourceId => "Compilation Log";

    public ProgressComposer Progress { get; }


    /// <summary>
    /// A flag for enabling the logging of error messages
    /// </summary>
    public bool LogErrorMessages = true;

    /// <summary>
    /// A flag for enabling the logging of warning messages
    /// </summary>
    public bool LogWarningMessages = true;

    /// <summary>
    /// The parent project for this error object
    /// </summary>
    public ISourceCodeUnitsContainer Project { get; private set; }


    public List<LanguageCompilationMessage> ErrorMessages { get; } = new List<LanguageCompilationMessage>();

    public List<LanguageCompilationMessage> WarningMessages { get; } = new List<LanguageCompilationMessage>();

    public bool StopOnFirstError { get; set; }


    public bool LogAllMessages 
    { 
        get 
        {
            return LogErrorMessages && LogWarningMessages;
        }
        set
        {
            LogErrorMessages = value;
            LogWarningMessages = value;
        }
    }


    public LanguageCompilationLog(ISourceCodeUnitsContainer project, ProgressComposer progressComposer)
    {
        Project = project;
        Progress = progressComposer;
    }


    public int ErrorsCount => ErrorMessages.Count;

    public int WarningsCount => WarningMessages.Count;

    public bool HasErrors => ErrorMessages.Count > 0;

    public bool HasWarnings => WarningMessages.Count > 0;

    public void Initialize(ISourceCodeUnitsContainer project)
    {
        Project = project;
        ErrorMessages.Clear();
        WarningMessages.Clear();
    }


    public LanguageCompilationMessage AddErrorMessage(string description, int absolutePos)
    {
        if (LogErrorMessages == false) return null;

        var location = Project.ActiveCodeUnit.TranslateCharacterLocation(absolutePos);

        var msg = new LanguageCompilationMessage(Project, location, description);

        ErrorMessages.Add(msg);

        return msg;
    }

    public T RaiseErrorMessage<T>(Exception e, int absolutePos)
    {
        AddErrorMessage(e.Message, absolutePos);

        this.ReportError(e);

        throw e;
    }

    public LanguageCompilationMessage AddWarningMessage(string description, int absolutePos)
    {
        if (LogWarningMessages == false) return null;

        var location = Project.ActiveCodeUnit.TranslateCharacterLocation(absolutePos);

        var msg = new LanguageCompilationMessage(Project, location, description);

        WarningMessages.Add(msg);

        this.ReportWarning("Warning", msg.ToString());

        return msg;
    }

    public LanguageCompilationMessage AddErrorMessage(string description, ParseTreeNode node)
    {
        if (LogErrorMessages == false) return null;

        //var absolutePos = node.FindToken().Location.Position;
        var absolutePos = node.Span.Location.Position;

        var location = Project.ActiveCodeUnit.TranslateCharacterLocation(absolutePos);

        var msg = new LanguageCompilationMessage(Project, location, node, description);

        ErrorMessages.Add(msg);

        return msg;
    }

    public T RaiseErrorMessage<T>(Exception e, ParseTreeNode node)
    {
        AddErrorMessage(e.Message, node);

        this.ReportError(e);

        throw e;
    }

    public LanguageCompilationMessage AddWarningMessage(string description, ParseTreeNode node)
    {
        if (LogWarningMessages == false) return null;

        //var absolutePos = node.FindToken().Location.Position;
        var absolutePos = node.Span.Location.Position;

        var location = Project.ActiveCodeUnit.TranslateCharacterLocation(absolutePos);

        var msg = new LanguageCompilationMessage(Project, location, node, description);

        WarningMessages.Add(msg);

        this.ReportWarning("Warning", msg.ToString());

        return msg;
    }

    public LanguageCompilationMessage AddErrorMessage(string description, Token token)
    {
        if (LogErrorMessages == false) return null;

        var absolutePos = token.Location.Position;

        var location = Project.ActiveCodeUnit.TranslateCharacterLocation(absolutePos);

        var msg = new LanguageCompilationMessage(Project, location, token, description);

        ErrorMessages.Add(msg);

        return msg;
    }

    public T RaiseErrorMessage<T>(Exception e, Token token)
    {
        AddErrorMessage(e.Message, token);

        this.ReportError(e);

        throw e;
    }

    public LanguageCompilationMessage AddWarningMessage(string description, Token token)
    {
        if (LogWarningMessages == false) return null;

        var absolutePos = token.Location.Position;

        var location = Project.ActiveCodeUnit.TranslateCharacterLocation(absolutePos);

        var msg = new LanguageCompilationMessage(Project, location, token, description);

        WarningMessages.Add(msg);

        this.ReportWarning("Warning", msg.ToString());

        return msg;
    }


    public override string ToString()
    {
        var s = new StringBuilder();

        if (HasErrors)
        {
            s.Append(ErrorMessages.Count);
            s.AppendLine(" Errors.");

            foreach (var msg in ErrorMessages)
                s.AppendLine(msg.ToString());
        }

        if (!HasWarnings) 
            return s.ToString();

        s.Append(WarningMessages.Count);
        s.AppendLine(" Warining.");

        foreach (var msg in WarningMessages)
            s.AppendLine(msg.ToString());

        return s.ToString();
    }
}