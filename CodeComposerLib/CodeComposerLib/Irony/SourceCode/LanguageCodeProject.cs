using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CodeComposerLib.Irony.SourceCode
{
    /// <summary>
    /// This class represents a DSL project containing all information about the input DSL source code and files to the
    /// DSL compiler. All source files must be inside the same folder as the project file
    /// </summary>
    [Serializable]
    public abstract class LanguageCodeProject : ISourceCodeUnitsContainer
    {
        /// <summary>
        /// The language name of the DSL project
        /// </summary>
        public string LanguageName { get; protected set; }

        /// <summary>
        /// The allowed DSL source files extensions
        /// </summary>
        public string[] AllowedSourceFilesExtensions { get; protected set; }

        /// <summary>
        /// The ordered list of project DSL source code files.
        /// </summary>
        protected readonly List<LanguageCodeFile> SourceCodeFiles = new List<LanguageCodeFile>();

        /// <summary>
        /// A list of LanguageCodeText objects that is populated during compilation of this project
        /// using code generation
        /// </summary>
        protected readonly Dictionary<string, LanguageCodeText> GeneratedSourceCode = 
            new Dictionary<string, LanguageCodeText>();

        /// <summary>
        /// The full project file path including its folder, name, and extension
        /// </summary>
        public string ProjectFilePath { get; protected set; }

        /// <summary>
        /// True if the project file path exists
        /// </summary>
        public bool ProjectFileExists => File.Exists(ProjectFilePath);

        /// <summary>
        /// The full project folder path as determined by ProjectFilePath
        /// </summary>
        public string ProjectFolderPath
        {
            get
            {
                if (File.Exists(ProjectFilePath) == false)
                    return string.Empty;

                var folder = Path.GetDirectoryName(ProjectFilePath);

                if (string.IsNullOrEmpty(folder))
                    return string.Empty;

                if (folder.Last() != '\\')
                    return folder + "\\";

                return folder;
            }
        }

        /// <summary>
        /// The project file name and extension as determined by ProjectFilePath
        /// </summary>
        public string ProjectFileName => File.Exists(ProjectFilePath) ? Path.GetFileName(ProjectFilePath) : "";

        /// <summary>
        /// The project file name without the extension as determined by ProjectFilePath
        /// </summary>
        public string ProjectFileNameWithoutExtension => File.Exists(ProjectFilePath) ? Path.GetFileNameWithoutExtension(ProjectFilePath) : "";

        /// <summary>
        /// The project file extension as determined by ProjectFilePath
        /// </summary>
        public string ProjectFileExtension => File.Exists(ProjectFilePath) ? Path.GetExtension(ProjectFilePath) : "";


        public FileInfo GetProjectFileInfo()
        {
            return new FileInfo(ProjectFilePath);
        }

        public DateTime ProjectFileLastWriteTime => GetProjectFileInfo().LastWriteTime;

        public IEnumerable<LanguageCodeFile> SourceFiles => SourceCodeFiles;

        public DateTime SourceFilesLastWriteTime
        {
            get { return SourceCodeFiles.Select(file => file.LastWriteTime).Max(); }
        }

        public IEnumerable<LanguageCodeText> GeneratedCode => GeneratedSourceCode.Values;

        public IEnumerable<string> SourceFilesPaths
        {
            get { return SourceCodeFiles.Select(file => file.FilePath); }
        }

        public IEnumerable<string> SourceFilesRelativePaths { get { return SourceCodeFiles.Select(file => file.FileRelativePath); } }

        public bool ContainsSourceFiles => SourceCodeFiles.Count > 0;

        public bool IsProject => true;

        public bool IsText => false;

        public IEnumerable<ISourceCodeUnit> SourceCodeUnits => SourceCodeFiles;

        public List<string> SourceCodeUnitsText
        {
            get
            {
                return SourceCodeFiles.Select(file => file.CodeText).ToList();
            }
        }

        public bool ContainsSourceCodeUnits => ContainsSourceFiles;

        public bool RequiresSourceCodeTextUpdate => true;

        public ISourceCodeUnit ActiveCodeUnit { get; set; }

        public bool HasActiveCodeUnit => ActiveCodeUnit != null;


        protected LanguageCodeProject()
        {
        }

        protected LanguageCodeProject(string projectFilePath)
        {
            ProjectFilePath = projectFilePath;
        }


        /// <summary>
        /// Set the project file path in case it's not already set
        /// </summary>
        /// <param name="projectFilePath"></param>
        public void SetProjectFilePath(string projectFilePath)
        {
            if (ProjectFileExists)
                throw new InvalidOperationException("Project file path already set");

            ProjectFilePath = projectFilePath;
        }

        /// <summary>
        /// This method removes the project folder path from the given absolute path
        /// The given path must be under the project folder path for this to work properly
        /// </summary>
        /// <param name="absolutePath"></param>
        /// <returns></returns>
        public string GetRelativePath(string absolutePath)
        {
            var relativePath = absolutePath.Substring(ProjectFolderPath.Length);

            return 
                relativePath.Substring(0, 1) == "\\" ? 
                relativePath.Substring(1) : 
                relativePath;
        }

        /// <summary>
        /// Return the absolute path resulting from combining the project folder path with the given path
        /// </summary>
        /// <param name="relativePath"></param>
        /// <returns></returns>
        public string GetAbsolutePath(string relativePath)
        {
            return Path.GetFullPath(Path.Combine(ProjectFolderPath, relativePath));
        }

        /// <summary>
        /// Clear the list of project source code files and generated text
        /// </summary>
        public void ClearSourceFilesList()
        {
            SourceCodeFiles.Clear();
            GeneratedSourceCode.Clear();
        }

        /// <summary>
        /// Clear the list of project source code files then add the given files one by one
        /// </summary>
        /// <param name="filesList"></param>
        public void SetSourceFilesList(IEnumerable<string> filesList)
        {
            ClearSourceFilesList();

            foreach (var filePath in filesList)
                AddSourceFile(filePath);
        }

        /// <summary>
        /// Clear the list of project source code files then add the given files one by one
        /// </summary>
        /// <param name="filesList"></param>
        /// <param name="fileEncoding"></param>
        public void SetSourceFilesList(IEnumerable<string> filesList, Encoding fileEncoding)
        {
            ClearSourceFilesList();

            foreach (var filePath in filesList)
                AddSourceFile(filePath, fileEncoding);
        }


        /// <summary>
        /// Given the absolute full path of a file this returns true if it is already a part of the project
        /// </summary>
        /// <param name="filePath">The absolute full path of the file</param>
        /// <returns>True if the file already exists in the project</returns>
        public bool ContainsSourceFile(string filePath)
        {
            var relativeChildPath = 
                filePath
                .ToLower()
                .Substring(ProjectFolderPath.Length);

            return (SourceCodeFiles.Exists(file => file.FileRelativePath.ToLower() == relativeChildPath));
        }

        /// <summary>
        /// Given the absolute full path of a file this returns true if the file can be added to the project
        /// The follwing cases prevent the file from being added to the project
        /// 1- The project file does not exist yet
        /// 2- File is not inside the project folder or one of its sub-folders
        /// 3- The relative file path is empty
        /// 4- The file already exists in the project
        /// 5- The file extension is not accepted
        /// </summary>
        /// <param name="filePath">The absolute full path of the file</param>
        /// <returns></returns>
        public virtual bool CanAddSourceFile(string filePath)
        {
            //The project file does not exist yet! The file can't be added
            if (ProjectFileExists == false)
                return false;

            var parentFolder = ProjectFolderPath.ToLower();
            var childPath = filePath.ToLower();

            //File is not inside the project folder or one of its sub-folders. The file can't be added
            if (childPath.Substring(0, parentFolder.Length) != parentFolder)
                return false;

            var relativeChildPath = childPath.Substring(parentFolder.Length);

            //The relative file path is empty! The file can't be added
            if (string.IsNullOrEmpty(relativeChildPath))
                return false;

            //The file already exists in the project. The file can't be added
            if (SourceCodeFiles.Exists(file => file.FileRelativePath.ToLower() == relativeChildPath))
                return false;

            var allowedExtensions = AllowedSourceFilesExtensions.Select(e => "." + e.ToLower()).ToList();
            var childExtinsion = Path.GetExtension(childPath);

            //If the file extension is not accepted. The file can't be added
            return allowedExtensions.Contains(childExtinsion);
        }

        /// <summary>
        /// Add a new source code file to the list of source code files in the project. The file must exist inside 
        /// the folder of the project or inside one of its sub-folders. The file must have an allowed extension as
        /// determined by the GetSourceFilesExtensions() mathod. The file must not be already added to the project
        /// </summary>
        /// <param name="filePath">The absolute full path of the file to be added to the project</param>
        /// <returns></returns>
        public LanguageCodeFile AddSourceFile(string filePath)
        {
            if (CanAddSourceFile(filePath) == false)
                return null;

            var fileRelativePath = GetRelativePath(filePath); //file_path.Substring(ProjectFolderPath.Length);

            var sourceFile = new LanguageCodeFile(this, fileRelativePath);

            SourceCodeFiles.Add(sourceFile);

            return sourceFile;
        }

        /// <summary>
        /// Add a new source code file to the list of source code files in the project. The file must exist inside 
        /// the folder of the project or inside one of its sub-folders. The file must have an allowed extension as
        /// determined by the GetSourceFilesExtensions() mathod. The file must not be already added to the project
        /// </summary>
        /// <param name="filePath">The absolute full path of the file to be added to the project</param>
        /// <param name="fileEncoding">The encoding of the text in the file</param>
        /// <returns></returns>
        public LanguageCodeFile AddSourceFile(string filePath, Encoding fileEncoding)
        {
            if (CanAddSourceFile(filePath) == false)
                return null;

            var fileRelativePath = filePath.Substring(ProjectFolderPath.Length);

            var sourceFile = new LanguageCodeFile(this, fileRelativePath, fileEncoding);

            SourceCodeFiles.Add(sourceFile);

            return sourceFile;
        }

        /// <summary>
        /// Given the absolute full file path this method removes the file from the project
        /// </summary>
        /// <param name="filePath">The absolute full file path to be removed</param>
        /// <returns>True if the file is found and removed</returns>
        public bool RemoveSourceFile(string filePath)
        {
            for (var i = 0; i < SourceCodeFiles.Count; i++)
            {
                if (!string.Equals(SourceCodeFiles[i].FilePath, filePath, StringComparison.CurrentCultureIgnoreCase)) 
                    continue;

                SourceCodeFiles.RemoveAt(i);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Find the DSLSourceCodeFile object corresponding to the given relative path from the list of source files in this project
        /// If no object exists in the list of source files null is returned
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public LanguageCodeFile GetSourceFileByRelativePath(string filePath)
        {
            return SourceCodeFiles.FirstOrDefault(file => string.Equals(file.FileRelativePath, filePath, StringComparison.CurrentCultureIgnoreCase));
        }

        /// <summary>
        /// Find the DSLSourceCodeFile object corresponding to the given path from the list of source files in this project
        /// If no object exists in the list of source files null is returned
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public LanguageCodeFile GetSourceFileByPath(string filePath)
        {
            return SourceCodeFiles.FirstOrDefault(file => string.Equals(file.FilePath, filePath, StringComparison.CurrentCultureIgnoreCase));
        }

        /// <summary>
        /// Add generated code text to this project
        /// </summary>
        /// <param name="codeTitle"></param>
        /// <param name="codeText"></param>
        /// <returns></returns>
        public LanguageCodeText AddGeneratedCode(string codeTitle, string codeText)
        {
            var genCode = new LanguageCodeText(codeTitle, codeText);

            GeneratedSourceCode.Add(codeTitle, genCode);

            return genCode;
        }

        protected void ClearSourceCodeLines()
        {
            if (SourceCodeFiles == null) 
                return;

            foreach (var file in SourceCodeFiles)
                file.ClearSourceCodeLines();
        }

        /// <summary>
        /// Save all relevant project information to a file on disk using serialization
        /// </summary>
        public abstract void SaveProjectToFile();

        public void UpdateSourceCodeUnitsText()
        {
            GeneratedSourceCode.Clear();

            foreach (var sourceFile in SourceCodeFiles)
                sourceFile.ReadSourceCodeTextFromFile();
        }

        /// <summary>
        /// True if there are files in this project that were modified on disk after the given time
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public virtual bool ModifiedAfter(DateTime time)
        {
            return ProjectFileLastWriteTime > time || SourceFilesLastWriteTime > time;
        }
    }
}
