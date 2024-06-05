using System.Text;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;

namespace GeometricAlgebraFulcrumLib.Utilities.Text.Files;

/// <summary>
/// This composer class uses an internal linear composer to create text in memory. After the text is complete
/// the method FinalizeText() can be called to set the final text member and remove the linear composer from 
/// memory. Also the SaveText() method can be called to finalize the text and save it into a file on disk
/// </summary>
public sealed class TextFileComposer
{
    private LinearTextComposer _textComposer;

    /// <summary>
    /// The path of this file
    /// </summary>
    public string FilePath { get; }

    /// <summary>
    /// The final text of this file composer
    /// </summary>
    public string FinalText { get; private set; }

    /// <summary>
    /// True if the contents of this file are saved to disk.
    /// The Clear() method resets this to false
    /// </summary>
    public bool IsSavedToDisk { get; private set; }

    /// <summary>
    /// True if the contents of this file are finalized.
    /// The Clear() method resets this to false
    /// </summary>
    public bool IsFinalized { get; private set; }

    /// <summary>
    /// The number of lines in the final text of this composer
    /// </summary>
    public int FinalTextLinesCount => FinalText.LinesCount();

    /// <summary>
    /// The number of characters in the final text of this composer
    /// </summary>
    public int FinalTextCharactersCount => FinalText.Length;

    /// <summary>
    /// The linear composer used in creating the final text for this file before it's
    /// finalized
    /// </summary>
    public LinearTextComposer TextComposer
    {
        get
        {
            if (IsFinalized)
                throw new InvalidOperationException("File contents already finalized");

            if (ReferenceEquals(_textComposer, null))
                _textComposer = new LinearTextComposer();

            return _textComposer;
        }
    }


    /// <summary>
    /// True if the final text is ready
    /// </summary>
    //public bool IsFinalTextReady => _textComposer == null;
    public TextFileComposer(string filePath)
    {
        FilePath = filePath;
        FinalText = string.Empty;
    }


    /// <summary>
    /// Clear the final text and the linear text composer
    /// </summary>
    /// <returns></returns>
    public TextFileComposer Clear()
    {
        _textComposer = null;

        FinalText = string.Empty;

        IsFinalized = false;

        IsSavedToDisk = false;

        return this;
    }

    /// <summary>
    /// Set final text to the text generated from the linear composer and clear the linear composer
    /// </summary>
    /// <returns></returns>
    public TextFileComposer FinalizeText()
    {
        if (IsFinalized) return this;

        FinalText = _textComposer.ToString();

        _textComposer = null;

        IsFinalized = true;

        return this;
    }
        
    /// <summary>
    /// Set final text to the text generated from the linear composer and clear the linear composer
    /// </summary>
    /// <returns></returns>
    public TextFileComposer FinalizeText(Func<string, string> textMapping)
    {
        if (IsFinalized) return this;

        FinalText = textMapping(_textComposer.ToString());

        _textComposer = null;

        IsFinalized = true;

        return this;
    }

    /// <summary>
    /// Set final text to the text generated from the linear composer and clear the linear composer and 
    /// save the final text to disk
    /// </summary>
    /// <param name="rootFolder"></param>
    /// <param name="clearFlag"></param>
    /// <param name="encoding"></param>
    /// <returns></returns>
    internal TextFileComposer SaveToDisk(string rootFolder, bool clearFlag = false, Encoding encoding = null)
    {
        if (IsSavedToDisk)
        {
            if (clearFlag) FinalText = string.Empty;
            return this;
        }

        FinalizeText();

        var fullFilePath = Path.Combine(rootFolder, FilePath);

        var folderPath = Path.GetDirectoryName(fullFilePath);

        if (folderPath != null && Directory.Exists(folderPath) == false)
            Directory.CreateDirectory(folderPath);

        File.WriteAllText(fullFilePath, FinalText, encoding ?? Encoding.Unicode);

        IsSavedToDisk = true;

        if (clearFlag) FinalText = string.Empty;

        return this;
    }

    /// <summary>
    /// If this file is finalized but not saved this method returns the FinalText
    /// property. If the file is already saved, this returns the contents from disk
    /// only if the FinalText property is empty
    /// </summary>
    /// <param name="rootFolder"></param>
    /// <param name="encoding"></param>
    /// <returns></returns>
    internal string GetFinalContents(string rootFolder, Encoding encoding = null)
    {
        if (!IsFinalized)
            return $"File <{FilePath}>: Contents not finalized yet";

        if (!IsSavedToDisk || !string.IsNullOrEmpty(FinalText))
            return FinalText;

        var fullFilePath = Path.Combine(rootFolder, FilePath);

        if (File.Exists(fullFilePath) == false)
            return $"File <{fullFilePath}>: Path not found on disk!";

        try
        {
            return File.ReadAllText(fullFilePath, encoding ?? Encoding.Unicode);
        }
        catch (Exception ex)
        {
            return new StringBuilder()
                .AppendLine($"File <{fullFilePath}>: Error while reading contents from disk!")
                .AppendLine(ex.Message)
                .ToString();
        }
    }

    /// <summary>
    /// Get a string describing some statistics about the final text of this composer
    /// </summary>
    /// <returns></returns>
    public string GenerateStatistics()
    {
        if (!IsFinalized)
            return $"File <{FilePath}>: Contents not finalized yet";

        if (IsSavedToDisk)
            return $"File <{FilePath}>: Contents saved to disk";

        var s = new StringBuilder();

        s
            .Append("File <")
            .Append(FilePath)
            .AppendLine(">:")
            .Append("    Lines: ")
            .AppendLine(FinalTextLinesCount.ToString("###,###,###,###"))
            .Append("    Characters: ")
            .AppendLine(FinalTextCharactersCount.ToString("###,###,###,###"))
            .Append("    Is Finalized: ")
            .AppendLine(IsFinalized ? "Yes" : "No")
            .Append("    Is Saved to Disk: ")
            .AppendLine(IsSavedToDisk ? "Yes" : "No")
            .Append("    Is Final Text Empty: ")
            .AppendLine(string.IsNullOrEmpty(FinalText) ? "Yes" : "No");

        return s.ToString();
    }

    internal string GenerateStatistics(string rootFolder, Encoding encoding, out int linesCount, out int charsCount)
    {
        if (!IsFinalized)
        {
            linesCount = 0;
            charsCount = 0;
            return $"File <{FilePath}>: Contents not finalized yet";
        }

        var text = GetFinalContents(rootFolder, encoding);

        linesCount = text.LinesCount();
        charsCount = text.Length;

        var s = new StringBuilder();

        s
            .Append("File <")
            .Append(FilePath)
            .AppendLine(">:")
            .Append("    Lines: ")
            .AppendLine(linesCount.ToString("###,###,###,###"))
            .Append("    Characters: ")
            .AppendLine(charsCount.ToString("###,###,###,###"))
            .Append("    Is Finalized: ")
            .AppendLine(IsFinalized ? "Yes" : "No")
            .Append("    Is Saved to Disk: ")
            .AppendLine(IsSavedToDisk ? "Yes" : "No")
            .Append("    Is Final Text Empty: ")
            .AppendLine(string.IsNullOrEmpty(FinalText) ? "Yes" : "No");

        return s.ToString();
    }

    public override string ToString()
    {
        if (!IsFinalized)
            return $"File <{FilePath}>: Contents not finalized yet";

        if (IsSavedToDisk)
            return $"File <{FilePath}>: Contents saved to disk";

        return FinalText;
    }
}