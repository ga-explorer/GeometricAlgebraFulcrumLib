using GeometricAlgebraFulcrumLib.Utilities.Code.Irony.Semantic;
using GeometricAlgebraFulcrumLib.Utilities.Code.Irony.Semantic.Translator;
using GeometricAlgebraFulcrumLib.Utilities.Code.Irony.SourceCode;
using GeometricAlgebraFulcrumLib.Utilities.Text.Loggers.Progress;

namespace GeometricAlgebraFulcrumLib.Utilities.Code.Irony.Compiler;

/// <summary>
/// The base class for all DSL compilers
/// </summary>
public abstract class LanguageCompiler : IProgressReportSource
{
    public abstract string ProgressSourceId { get; }

    public abstract ProgressComposer Progress { get;  }

    /// <summary>
    /// The compilation log object
    /// </summary>
    public LanguageCompilationLog CompilationLog { get; protected set; }

    /// <summary>
    /// The root Irony AST generated after semantic analysis of the code units parse trees
    /// </summary>
    public IronyAst RootAst { get; protected set; }

    /// <summary>
    /// The symbols translator context used during translation of parse tree into AST
    /// </summary>
    public SymbolTranslatorContext TranslatorContext { get; protected set; }
}