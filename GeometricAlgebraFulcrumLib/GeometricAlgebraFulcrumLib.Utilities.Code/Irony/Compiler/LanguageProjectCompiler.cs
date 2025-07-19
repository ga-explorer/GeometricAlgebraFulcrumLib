using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Utilities.Code.Irony.SourceCode;
using Irony.Parsing;

namespace GeometricAlgebraFulcrumLib.Utilities.Code.Irony.Compiler;

/// <summary>
/// This class performs compilation of a full DSL project possibly including multiple source
/// code files and possibly text code generated during compilation
/// </summary>
public abstract class LanguageProjectCompiler : LanguageCompiler
{
    /// <summary>
    /// The DSL Project holding all information about DSL source code and source code files
    /// </summary>
    public LanguageCodeProject Project { get; protected set; }

    public List<LanguageProjectCodeUnitCompiler> CodeUnitCompilers { get; private set; }

    /// <summary>
    /// The parse trees of the main code units (not including the generated code units)
    /// </summary>
    protected List<ParseTreeNode> CodeUnitParseTrees { get; private set; }


    /// <summary>
    /// Initialize the compilation log of this compiler
    /// </summary>
    protected virtual void InitializeCompilationLog()
    {
        if (CompilationLog == null)
            CompilationLog = new LanguageCompilationLog(Project, Progress);
        else
            CompilationLog.Initialize(Project);
    }

    /// <summary>
    /// Initialize the root AST of this compiler
    /// </summary>
    protected abstract void InitializeRootAst();

    /// <summary>
    /// Initialize the translator context
    /// </summary>
    protected abstract void InitializeTranslatorContext();

    /// <summary>
    /// Create a child code unit compiler
    /// </summary>
    /// <param name="codeUnit"></param>
    /// <param name="rootParseNode"></param>
    /// <returns></returns>
    protected abstract LanguageProjectCodeUnitCompiler InitializeCodeUnitCompiler(ISourceCodeUnit codeUnit, ParseTreeNode rootParseNode);

    /// <summary>
    /// Create a child code unit compiler
    /// </summary>
    /// <param name="codeUnit"></param>
    /// <returns></returns>
    protected LanguageProjectCodeUnitCompiler InitializeCodeUnitCompiler(ISourceCodeUnit codeUnit)
    {
        return InitializeCodeUnitCompiler(codeUnit, null);
    }

    /// <summary>
    /// Parse the source code to generate the parse tree for the given code unit
    /// </summary>
    public abstract ParseTreeNode ParseCodeUnit(ISourceCodeUnit codeUnit);

    /// <summary>
    /// Finalize the root AST of this compiler
    /// </summary>
    protected abstract void FinalizeRootAst();

    /// <summary>
    /// Tests if the code of the given project has changed since last compilation, if any
    /// </summary>
    /// <param name="dslProject"></param>
    /// <returns></returns>
    protected virtual bool CodeChanged(LanguageCodeProject dslProject)
    {
        return true;

        //if (CodeUnitParseTrees == null || CodeUnitParseTrees.Count != Project.SourceFiles.Count())
        //    return true;

        //var i = 0;
        //foreach (var codeFile in Project.SourceFiles)
        //{
        //    var oldRootParseNode = CodeUnitParseTrees[i++];
        //    var newRootParseNode = ParseCodeUnit(codeFile);

        //    if (
        //        ReferenceEquals(oldRootParseNode, null) ||
        //        ReferenceEquals(newRootParseNode, null) ||
        //        oldRootParseNode.IsSameParseTreeNode(newRootParseNode) == false
        //        )
        //        return true;
        //}

        //return false;
    }

    /// <summary>
    /// Initialize the compiler after clearing all previous compilation data; if any
    /// </summary>
    /// <param name="dslProject">The input DSL project</param>
    private void InitializeCompiler(LanguageCodeProject dslProject)
    {
        CodeUnitCompilers = new List<LanguageProjectCodeUnitCompiler>();

        CodeUnitParseTrees = new List<ParseTreeNode>();

        Project = dslProject;

        InitializeCompilationLog();

        InitializeRootAst();

        InitializeTranslatorContext();

        Project.UpdateSourceCodeUnitsText();
    }

    /// <summary>
    /// Initialize the compiler after compilation is done
    /// </summary>
    private void FinalizeCompiler()
    {
        FinalizeRootAst();

        Project.ActiveCodeUnit = null;

        TranslatorContext = null;
    }

    public void CompileGeneratedCode(string codeTitle, string codeText)
    {
        var codeUnit = Project.AddGeneratedCode(codeTitle, codeText);

        var oldActiveCodeUnit = Project.ActiveCodeUnit;

        Project.ActiveCodeUnit = codeUnit;

        var codeUnitCompiler = InitializeCodeUnitCompiler(codeUnit);

        CodeUnitCompilers.Add(codeUnitCompiler);

        codeUnitCompiler.Compile();

        Project.ActiveCodeUnit = oldActiveCodeUnit;
    }

    private void CompileCodeFile(LanguageCodeFile codeFile)
    {
        Project.ActiveCodeUnit = codeFile;

        var rootParseNode = ParseCodeUnit(codeFile);

        var codeUnitCompiler = InitializeCodeUnitCompiler(codeFile, rootParseNode);

        CodeUnitCompilers.Add(codeUnitCompiler);

        CodeUnitParseTrees.Add(codeUnitCompiler.RootParseNode);

        codeUnitCompiler.Compile();
    }

    /// <summary>
    /// Compile the given code units
    /// </summary>
    /// <param name="dslProject">The input DSL project</param>
    /// <param name="forceCompilation"></param>
    public void Compile(LanguageCodeProject dslProject, bool forceCompilation = false)
    {
        if (forceCompilation == false && CodeChanged(dslProject) == false)
            return;

        InitializeCompiler(dslProject);

        foreach (var codeFile in Project.SourceFiles)
            CompileCodeFile(codeFile);

        FinalizeCompiler();
    }
}