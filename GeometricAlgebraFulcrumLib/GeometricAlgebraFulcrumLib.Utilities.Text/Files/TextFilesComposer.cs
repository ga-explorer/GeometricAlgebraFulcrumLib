using System.Collections;
using System.Text;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;

namespace GeometricAlgebraFulcrumLib.Utilities.Text.Files;

public sealed class TextFilesComposer 
    : IDictionary<string, TextFileComposer>
{
    //private static string ExtractLastPathPart(string path, string separator)
    //{
    //    var lastSepPos = path.LastIndexOf(separator, StringComparison.Ordinal);

    //    if (lastSepPos < 0)
    //        return path;

    //    return
    //        (lastSepPos == path.Length - 1)
    //        ? string.Empty
    //        : path.Substring(lastSepPos + 1);
    //}

    private static string ExtractAllButLastPathPart(string path, string separator)
    {
        var lastSepPos = path.LastIndexOf(separator, StringComparison.Ordinal);

        return
            (lastSepPos < 0)
                ? string.Empty
                : path.Substring(0, lastSepPos + 1);
    }

    //private static void SaveToDisk(string rootFolder, string partialFilePath, TextFileComposer fileTexComposer, Encoding encoding)
    //{
    //    var filePath = Path.Combine(rootFolder, partialFilePath);

    //    var folderPath = Path.GetDirectoryName(filePath);

    //    if (folderPath != null && Directory.Exists(folderPath) == false)
    //        Directory.CreateDirectory(folderPath);

    //    File.WriteAllText(filePath, fileTexComposer.ToString(), encoding);
    //}



    private readonly SortedDictionary<string, TextFileComposer> _fileComposersDictionary =
        new SortedDictionary<string, TextFileComposer>();

    private readonly List<string> _activeFolderPathList = new List<string>();

    private Encoding _filesEncoding = Encoding.Unicode;

    private readonly LinearTextComposer _log = new LinearTextComposer();


    /// <summary>
    /// The encoding of all text files in this composer. The default is Encoding.Unicode
    /// </summary>
    public Encoding FilesEncoding
    {
        get => _filesEncoding;
        set => _filesEncoding = value ?? Encoding.Unicode;
    }

    /// <summary>
    /// The main root folder of this composer
    /// </summary>
    public string RootFolder { get; set; }

    /// <summary>
    /// If true, any file that is finalized will be saved to disk
    /// and cleared from memory provided the root folder exists or can be created
    /// and no errors during saving are made
    /// </summary>
    //public bool AutoSaveFinalizedFiles { get; set; }

    /// <summary>
    /// When a file is saved successfully, its in-memory text is cleared
    /// </summary>
    //public bool AutoClearSavedFiles { get; set; }

    /// <summary>
    /// The log of this composer containing any messages or errors during its operation
    /// </summary>
    public string ComposerLog => _log.ToString();

    /// <summary>
    /// The base folder where all text files will be written
    /// </summary>
    public bool AreAllFilesFinalized
    {
        get { return _fileComposersDictionary.Any(pair => pair.Value.IsFinalized == false) == false; }
    }

    /// <summary>
    /// 
    /// </summary>
    public bool AreSomeFilesNotFinalized
    {
        get { return _fileComposersDictionary.Any(pair => pair.Value.IsFinalized == false); }
    }

    /// <summary>
    /// Get the path separator for the folders
    /// </summary>
    public string PathSeparator { get; }

    /// <summary>
    /// The name of the active file
    /// </summary>
    public string ActiveFileName { get; private set; }

    /// <summary>
    /// The linear text composer associated with the active file
    /// </summary>
    public TextFileComposer ActiveFileComposer { get; private set; }

    /// <summary>
    /// The linear composer of the active file
    /// </summary>
    public LinearTextComposer ActiveFileTextComposer => HasActiveFile ? ActiveFileComposer.TextComposer : null;

    /// <summary>
    /// The final text of the active file
    /// </summary>
    public string ActiveFileFinalText => HasActiveFile ? ActiveFileComposer.FinalText : string.Empty;

    /// <summary>
    /// The path of the active folder
    /// </summary>
    public string ActiveFolder 
    {
        get
        {
            if (_activeFolderPathList.Count == 0)
                return string.Empty;

            var s = new StringBuilder();

            var flag = false;
            foreach (var folder in _activeFolderPathList)
            {
                if (flag) s.Append(PathSeparator);
                else flag = true;

                s.Append(folder);
            }

            return s.ToString();
        }
    }

    /// <summary>
    /// The full path of the active file
    /// </summary>
    public string ActiveFilePath 
    {
        get
        {
            if (HasActiveFile == false)
                return string.Empty;

            var s = new StringBuilder();

            s.Append(ActiveFolder).Append(PathSeparator).Append(ActiveFileName);

            return s.ToString();
        }
    }

    /// <summary>
    /// True if there is an active file
    /// </summary>
    public bool HasActiveFile => ActiveFileComposer != null;

    /// <summary>
    /// The level of the active folder
    /// </summary>
    public int ActiveFolderLevel => _activeFolderPathList.Count;

    /// <summary>
    /// All the file composers of this composer
    /// </summary>
    public IEnumerable<TextFileComposer> Files => _fileComposersDictionary.Values;

    ///// <summary>
    ///// All the file composers of this composer having ready final text
    ///// </summary>
    //public IEnumerable<TextFileComposer> FinalizedFiles
    //{
    //    get { return _fileComposersDictionary.Values.Where(c => c.IsFinalized); }
    //}

    ///// <summary>
    ///// All the file composers of this composer having not-ready final text
    ///// </summary>
    //public IEnumerable<TextFileComposer> NotFinalizedFiles
    //{
    //    get { return _fileComposersDictionary.Values.Where(c => c.IsFinalized == false); }
    //}

    /// <summary>
    /// All the relative file paths of this composer
    /// </summary>
    public IEnumerable<string> FilePaths => _fileComposersDictionary.Keys;

    /// <summary>
    /// All the full file paths of this composer
    /// </summary>
    public IEnumerable<string> FullFilePaths
    {
        get
        {
            return 
                string.IsNullOrEmpty(RootFolder)
                    ? _fileComposersDictionary.Keys
                    : _fileComposersDictionary.Select(pair => Path.Combine(RootFolder, pair.Key));
        }
    }

    /// <summary>
    /// All the folders of this composer
    /// </summary>
    public IEnumerable<string> FolderPaths
    {
        get
        {
            return 
                _fileComposersDictionary
                    .Select(pair => ExtractAllButLastPathPart(pair.Key, PathSeparator))
                    .Distinct();
        }
    }

    /// <summary>
    /// The final text of the first text file composer of this composer
    /// </summary>
    public string FirstFileFinalText
    {
        get
        {
            var firstFile = _fileComposersDictionary.FirstOrDefault();

            return firstFile.Value == null ? string.Empty : firstFile.Value.FinalText;
        }
    }

    /// <summary>
    /// The the first text file composer of this composer
    /// </summary>
    public TextFileComposer FirstFileComposer
    {
        get
        {
            var firstFile = _fileComposersDictionary.FirstOrDefault();

            return firstFile.Value;
        }
    }

    /// <summary>
    /// The linear text composer associated with the first text file of this composer
    /// </summary>
    public LinearTextComposer FirstFileTextComposer
    {
        get
        {
            var firstFile = _fileComposersDictionary.FirstOrDefault();

            return firstFile.Value?.TextComposer;
        }
    }


    public TextFilesComposer()
    {
        PathSeparator = @"\";
    }

    public TextFilesComposer(string pathSeparator)
    {
        PathSeparator = pathSeparator;
    }


    private string GetActiveFilePath(string fileName)
    {
        if (string.IsNullOrEmpty(fileName))
            return string.Empty;

        if (_activeFolderPathList.Count == 0)
            return fileName;

        var s = new StringBuilder();

        var flag = false;
        foreach (var folder in _activeFolderPathList)
        {
            if (flag) s.Append(PathSeparator);
            else flag = true;

            s.Append(folder);
        }

        s.Append(PathSeparator).Append(fileName);

        return s.ToString();
    }


    /// <summary>
    /// Set the active folder to be an empty string
    /// </summary>
    /// <returns></returns>
    public TextFilesComposer SelectFolder()
    {
        _activeFolderPathList.Clear();

        UnselectActiveFile();

        return this;
    }

    /// <summary>
    /// Set the active folder to the given path and unselect the active file
    /// </summary>
    /// <param name="folderPath"></param>
    /// <returns></returns>
    public TextFilesComposer SelectFolder(string folderPath)
    {
        if (string.IsNullOrEmpty(folderPath))
        {
            _activeFolderPathList.Clear();

            UnselectActiveFile();

            return this;
        }

        var subFolders = folderPath.Split(new[] {PathSeparator}, StringSplitOptions.None);

        _activeFolderPathList.Clear();

        _activeFolderPathList.AddRange(subFolders);

        UnselectActiveFile();

        return this;
    }

    /// <summary>
    /// Go up one level to the direct parent of the active folder if possible, and unselect the active file
    /// </summary>
    /// <returns></returns>
    public TextFilesComposer UpFolder()
    {
        return UpFolder(1);
    }

    /// <summary>
    /// Go up n levels (n > 0) to a parent of the active folder if possible, and unselect the active file
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public TextFilesComposer UpFolder(int n)
    {
        if (n < 1) return this;

        n = Math.Min(n, _activeFolderPathList.Count);

        if (n == _activeFolderPathList.Count)
            _activeFolderPathList.Clear();
        else
            _activeFolderPathList.RemoveRange(_activeFolderPathList.Count - n, n);

        UnselectActiveFile();

        return this;
    }

    /// <summary>
    /// Go down to a child of the active folder with the given relative path, and unselect the active file
    /// </summary>
    /// <param name="folderPath"></param>
    /// <returns></returns>
    public TextFilesComposer DownFolder(string folderPath)
    {
        if (string.IsNullOrEmpty(folderPath))
        {
            UnselectActiveFile();

            return this;
        }

        var subFolders = folderPath.Split(new[] { PathSeparator }, StringSplitOptions.None);

        _activeFolderPathList.AddRange(subFolders);

        UnselectActiveFile();

        return this;
    }

    /// <summary>
    /// Set the given file to be the active file. If the file doesn't exist it's created.
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public TextFilesComposer SelectFile(string fileName)
    {
        if (string.IsNullOrEmpty(fileName))
        {
            UnselectActiveFile();

            return this;
        }

        var filePath = GetActiveFilePath(fileName);

        ActiveFileName = fileName;

        if (_fileComposersDictionary.TryGetValue(filePath, out var fileComposer) == false)
        {
            fileComposer = new TextFileComposer(filePath);
            _fileComposersDictionary.Add(filePath, fileComposer);
        }

        ActiveFileComposer = fileComposer;

        return this;
    }

    /// <summary>
    /// Set the given file to be the active file. If the file doesn't exist it's created.
    /// After that it executes the given action.
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="action"></param>
    /// <returns></returns>
    public TextFilesComposer SelectFile(string fileName, Action<TextFileComposer> action)
    {
        SelectFile(fileName);

        if (HasActiveFile == false) return this;

        action?.Invoke(ActiveFileComposer);

        return this;
    }

    /// <summary>
    /// Clears the information of the active file so that HasActiveFile returna false.
    /// </summary>
    public TextFilesComposer UnselectActiveFile()
    {
        ActiveFileName = string.Empty;
        ActiveFileComposer = null;

        return this;
    }

    /// <summary>
    /// Execute the given action on the active file if any.
    /// Then it clears the information of the active file so that HasActiveFile returna false.
    /// </summary>
    /// <param name="action"></param>
    /// <returns></returns>
    public TextFilesComposer UnselectActiveFile(Action<TextFileComposer> action)
    {
        if (HasActiveFile == false) return this;

        action?.Invoke(ActiveFileComposer);

        ActiveFileName = string.Empty;
        ActiveFileComposer = null;

        return this;
    }

    /// <summary>
    /// Set the given file to be the active file and clear its composer. 
    /// If the file doesn't exist it's created.
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public TextFilesComposer InitializeFile(string fileName)
    {
        SelectFile(fileName);

        if (HasActiveFile == false) return this;

        ActiveFileComposer.Clear();

        return this;
    }

    /// <summary>
    /// Set the given file to be the active file and clear its composer. 
    /// If the file doesn't exist it's created. After that it executes the given action.
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="initAction"></param>
    /// <returns></returns>
    public TextFilesComposer InitializeFile(string fileName, Action<TextFileComposer> initAction)
    {
        SelectFile(fileName);

        if (HasActiveFile == false) return this;

        ActiveFileComposer.Clear();

        initAction?.Invoke(ActiveFileComposer);

        return this;
    }

    /// <summary>
    /// Set the given file to be the active file and write the given code into its composer. 
    /// If the file doesn't exist it's created.
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="code"></param>
    /// <returns></returns>
    public TextFilesComposer InitializeFile(string fileName, string code)
    {
        InitializeFile(fileName)
            .ActiveFileTextComposer
            .AppendLine(code);

        return this;
    }

    /// <summary>
    /// Finalize the text of the active file of this composer. If AutoSaveFinalizedFiles
    /// is true, this saves the file to disk under the main root folder
    /// </summary>
    /// <returns></returns>
    public TextFilesComposer FinalizeActiveFile()
    {
        if (HasActiveFile == false || ActiveFileComposer.IsFinalized) return this;

        ActiveFileComposer.FinalizeText();

        return this;
    }

    /// <summary>
    /// Finalize the text of the active file if not already finalized after executing
    /// the given action. If AutoSaveFinalizedFiles is true, this saves the file to 
    /// disk under the main root folder
    /// </summary>
    /// <param name="action"></param>
    /// <returns></returns>
    public TextFilesComposer FinalizeActiveFile(Action<TextFileComposer> action)
    {
        if (HasActiveFile == false || ActiveFileComposer.IsFinalized) return this;

        action?.Invoke(ActiveFileComposer);

        ActiveFileComposer.FinalizeText();

        return this;
    }

    /// <summary>
    /// For all files in this composer finalize the text in the files if
    /// not already finalized
    /// </summary>
    /// <returns></returns>
    public TextFilesComposer FinalizeAllFiles()
    {
        foreach (var fileComposer in Files)
        {
            if (fileComposer.IsFinalized) continue;

            fileComposer.FinalizeText();
        }

        return this;
    }

    /// <summary>
    /// For all file composers in this composer finalize the text in the files if
    /// not already finalized after executing the given action on each file
    /// </summary>
    /// <param name="action"></param>
    /// <returns></returns>
    public TextFilesComposer FinalizeAllFiles(Action<TextFileComposer> action)
    {
        foreach (var fileComposer in Files)
        {
            if (fileComposer.IsFinalized) continue;

            action?.Invoke(fileComposer);

            fileComposer.FinalizeText();
        }

        return this;
    }


    /// <summary>
    /// Save all files and their text contents to disk under the given root folder
    /// </summary>
    /// <param name="rootFolder"></param>
    /// <returns>True if saving files resulted in no errors</returns>
    public bool SaveToFolder(string rootFolder)
    {
        var success = true;

        //Finalize all files without calling the FinalizeAllFiles() method
        //to prevent un-intended recursion
        foreach (var fileComposer in Files)
            if (!fileComposer.IsFinalized) fileComposer.FinalizeText();

        foreach (var pair in _fileComposersDictionary)
        {
            try
            {
                pair.Value.SaveToDisk(rootFolder, true, FilesEncoding);
            }
            catch (Exception e)
            {
                success = false;

                _log
                    .AppendLineAtNewLine($"Error saving file <{pair.Key}>")
                    .AppendLineAtNewLine(e.Message);
            }
        }

        return success;
    }

    /// <summary>
    /// Save all files and their text contents to disk under the main root folder
    /// </summary>
    /// <returns>True if saving files resulted in no errors</returns>
    public bool SaveToFolder()
    {
        return SaveToFolder(RootFolder);
    }

    /// <summary>
    /// Save all files and their text contents to disk under the given root folder
    /// after executing the given action for each file and finalizing its text
    /// </summary>
    /// <param name="rootFolder"></param>
    /// <param name="action"></param>
    /// <returns></returns>
    public bool SaveToFolder(string rootFolder, Action<TextFileComposer> action)
    {
        var success = true;

        //Finalize all files without calling the FinalizeAllFiles() method
        //to prevent un-intended recursion
        foreach (var fileComposer in Files)
        {
            if (fileComposer.IsFinalized) continue;

            action?.Invoke(fileComposer);

            fileComposer.FinalizeText();
        }

        foreach (var pair in _fileComposersDictionary)
        {
            try
            {
                pair.Value.SaveToDisk(rootFolder, true, FilesEncoding);
            }
            catch (Exception e)
            {
                success = false;

                _log
                    .AppendLineAtNewLine($"Error saving file <{pair.Key}>")
                    .AppendLineAtNewLine(e.Message);
            }
        }

        return success;
    }

    /// <summary>
    /// Save all files and their text contents to disk under the main root folder
    /// after executing the given action for each file and finalizing its text
    /// </summary>
    /// <param name="action"></param>
    /// <returns></returns>
    public bool SaveToFolder(Action<TextFileComposer> action)
    {
        return SaveToFolder(RootFolder, action);
    }

    /// <summary>
    /// Finalize and save the active file under the given root folder. If the
    /// unselect input is true, the active file is unselected after saving
    /// </summary>
    /// <param name="rootFolder"></param>
    /// <param name="unselect"></param>
    /// <returns>True if saving file resulted in no errors</returns>
    public bool SaveActiveFile(string rootFolder, bool unselect = true)
    {
        if (HasActiveFile == false) return false;

        ActiveFileComposer.FinalizeText();

        try
        {
            ActiveFileComposer.SaveToDisk(rootFolder, true, FilesEncoding);
            if (unselect) UnselectActiveFile();
        }
        catch (Exception ex)
        {
            _log
                .AppendLineAtNewLine($"Error saving file <{ActiveFilePath}>")
                .AppendLineAtNewLine(ex.Message);

            return false;
        }

        return true;
    }

    /// <summary>
    /// Finalize and save the active file under the main folder. If the unselect
    /// input is true, the active file is unselected afte saving
    /// </summary>
    /// <returns>True if saving file resulted in no errors</returns>
    public bool SaveActiveFile(bool unselect = true)
    {
        return SaveActiveFile(RootFolder, unselect);
    }

    /// <summary>
    /// Finalize and save the active file under the main root folder after executing
    /// the given action. If the unselect input is true, the active file is unselected
    /// after saving
    /// </summary>
    /// <param name="rootFolder"></param>
    /// <param name="action"></param>
    /// <param name="unselect"></param>
    /// <returns>True if saving file resulted in no errors</returns>
    public bool SaveActiveFile(string rootFolder, Action<TextFileComposer> action, bool unselect = true)
    {
        if (HasActiveFile == false) return false;

        action?.Invoke(ActiveFileComposer);

        ActiveFileComposer.FinalizeText();

        try
        {
            ActiveFileComposer.SaveToDisk(rootFolder, true, FilesEncoding);
            if (unselect) UnselectActiveFile();
        }
        catch (Exception ex)
        {
            _log
                .AppendLineAtNewLine($"Error saving file <{ActiveFilePath}>")
                .AppendLineAtNewLine(ex.Message);

            return false;
        }

        return true;
    }

    /// <summary>
    /// Finalize and save the active file under the main folder after executing
    /// the given action. If the unselect input is true the active file is
    /// unselected after saving
    /// </summary>
    /// <param name="action"></param>
    /// <param name="unselect"></param>
    /// <returns>True if saving file resulted in no errors</returns>
    public bool SaveActiveFile(Action<TextFileComposer> action, bool unselect = true)
    {
        return SaveActiveFile(RootFolder, action, unselect);
    }

    /// <summary>
    /// Save a single file of this composer to a disk under the given root folder
    /// </summary>
    /// <param name="rootFolder"></param>
    /// <param name="filePath"></param>
    /// <returns>True if saving file resulted in no errors</returns>
    public bool SaveFile(string rootFolder, string filePath)
    {
        if (!_fileComposersDictionary.TryGetValue(filePath, out var fileComposer))
        {
            _log.AppendLineAtNewLine($"File {filePath} not found in composer files.");
            return false;
        }

        fileComposer.FinalizeText();

        try
        {
            fileComposer.SaveToDisk(rootFolder, true, FilesEncoding);
        }
        catch (Exception ex)
        {
            _log
                .AppendLineAtNewLine($"Error saving file <{filePath}>")
                .AppendLineAtNewLine(ex.Message);

            return false;
        }

        return true;
    }

    /// <summary>
    /// Save a single file of this composer to a disk under the main root folder
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns>True if saving files resulted in no errors</returns>
    public bool SaveFile(string filePath)
    {
        return SaveFile(RootFolder, filePath);
    }

    /// <summary>
    /// Read the contents of the given file from disk. The path is relative
    /// to the main root folder of the composer
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    public string GetFileFinalContents(string filePath)
    {
        return 
            _fileComposersDictionary.TryGetValue(filePath, out var fileComposer) 
                ? fileComposer.GetFinalContents(RootFolder, FilesEncoding) 
                : $"File {filePath} not found in composer files.";
    }

    /// <summary>
    /// Save all files contents to a single disk file. this function will not
    /// automatically clear files contents if AutoClearSavedFiles is true
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns>True if saving files resulted in no errors</returns>
    public bool SaveToSingleFile(string filePath)
    {
        var success = true;

        //Finalize all files without calling the FinalizeAllFiles() method
        //to prevent un-intended recursion
        foreach (var fileComposer in Files)
            if (!fileComposer.IsFinalized) fileComposer.FinalizeText();

        var text = ToString();

        try
        {
            File.WriteAllText(filePath, text, FilesEncoding);
        }
        catch (Exception e)
        {
            success = false;

            _log
                .AppendLineAtNewLine("Error saving files contents")
                .AppendLineAtNewLine(e.Message);
        }

        return success;
    }


    /// <summary>
    /// Generate a string containing some statistics about the final text inside
    /// the files of this composer
    /// </summary>
    /// <returns></returns>
    public string GenerateStatistics()
    {
        var composer = new LinearTextComposer();

        composer
            .AppendLineAtNewLine("Composer Log:")
            .AppendLineAtNewLine(_log.ToString())
            .AppendLine();

        var totalLines = 0;
        var totalCharacters = 0;

        foreach (var pair in _fileComposersDictionary)
        {
            composer.AppendAtNewLine(
                pair.Value.GenerateStatistics(RootFolder, FilesEncoding, out var linesCount, out var charsCount)
            );

            totalLines += linesCount;
            totalCharacters += charsCount;
        }

        composer
            .AppendLine()
            .AppendAtNewLine("Total Files: ")
            .AppendLine(_fileComposersDictionary.Count.ToString("###,###,###,###"))
            .AppendAtNewLine("Total Lines: ")
            .AppendLine(totalLines.ToString("###,###,###,###"))
            .AppendAtNewLine("Total Characters: ")
            .AppendLine(totalCharacters.ToString("###,###,###,###"));

        return composer.ToString();
    }


    public void Clear()
    {
        UnselectActiveFile();

        _activeFolderPathList.Clear();

        _fileComposersDictionary.Clear();

        _log.Clear();
    }

    public void Add(string key, TextFileComposer value)
    {
        _fileComposersDictionary.Add(key, value ?? new TextFileComposer(key));
    }

    public bool ContainsKey(string key)
    {
        return _fileComposersDictionary.ContainsKey(key);
    }

    public ICollection<string> Keys => _fileComposersDictionary.Keys;

    public bool Remove(string key)
    {
        return _fileComposersDictionary.Remove(key);
    }

    public bool TryGetValue(string key, out TextFileComposer value)
    {
        return _fileComposersDictionary.TryGetValue(key, out value);
    }

    public ICollection<TextFileComposer> Values => _fileComposersDictionary.Values;

    public TextFileComposer this[string key]
    {
        get => _fileComposersDictionary[key];
        set => _fileComposersDictionary[key] = value ?? new TextFileComposer(key);
    }

    public void Add(KeyValuePair<string, TextFileComposer> item)
    {
        _fileComposersDictionary.Add(item.Key, item.Value ?? new TextFileComposer(item.Key));
    }

    public bool Contains(KeyValuePair<string, TextFileComposer> item)
    {
        if (_fileComposersDictionary.TryGetValue(item.Key, out var value) == false)
            return false;

        if (item.Value == null)
            return value.ToString() == string.Empty;

        return (value.ToString() == item.Value.ToString());
    }

    public void CopyTo(KeyValuePair<string, TextFileComposer>[] array, int arrayIndex)
    {
        throw new NotImplementedException();
    }

    public int Count => _fileComposersDictionary.Count;

    public bool IsReadOnly => false;

    public bool Remove(KeyValuePair<string, TextFileComposer> item)
    {
        if (_fileComposersDictionary.TryGetValue(item.Key, out var value) == false)
            return false;

        if (item.Value == null)
            return value.ToString() == string.Empty;

        if (value.ToString() != item.Value.ToString())
            return false;

        return _fileComposersDictionary.Remove(item.Key);
    }

    public IEnumerator<KeyValuePair<string, TextFileComposer>> GetEnumerator()
    {
        return _fileComposersDictionary.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return _fileComposersDictionary.GetEnumerator();
    }


    public override string ToString()
    {
        var composer = new StringBuilder();

        foreach (var (fileName, textFileComposer) in _fileComposersDictionary)
            composer.AppendLine($"File <{fileName}>:")
                .AppendLine(textFileComposer.GetFinalContents(RootFolder, FilesEncoding))
                .AppendLine();

        return composer.ToString();
    }
}