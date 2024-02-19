using CodeComposerLib.Irony.SourceCode;
using Irony.Parsing;

namespace CodeComposerLib.Irony.Semantic.Translator;

/// <summary>
/// This is the base class for creating language symbol translators
/// </summary>
public abstract class SymbolTranslator //: IPoolObject
{
    /// <summary>
    /// The translator context
    /// </summary>
    public SymbolTranslatorContext TranslatorContext { get; private set; }

    /// <summary>
    /// The root parse tree node for the symbol
    /// </summary>
    public ParseTreeNode RootParseNode { get; private set; }

    /// <summary>
    /// The compilation log object
    /// </summary>
    public LanguageCompilationLog CompilationLog => TranslatorContext.CompilationLog;

    /// <summary>
    /// The parent Irony DSL for translation process
    /// </summary>
    public virtual IronyAst ParentDsl => TranslatorContext.RootAst;


    //protected SymbolTranslator(SymbolTranslatorContext context)
    //{
    //    TranslatorContext = context;
    //    RootParseNode = context.ActiveParseNode;
    //}

    protected virtual void SetContext(SymbolTranslatorContext context)
    {
        TranslatorContext = context;
        RootParseNode = context.ActiveParseNode;
    }


    /// <summary>
    /// The main function used for translation process
    /// </summary>
    protected abstract void Translate();


    //#region IPoolObject Implementation

    //public bool EnableInitializeOnCreate
    //{
    //    get { return false; }
    //}

    //public bool EnableResetOnRelease
    //{
    //    get { return false; }
    //}

    //public bool EnableResetOnAcquire
    //{
    //    get { return true; }
    //}

    //public void InitializeOnCreate()
    //{
    //    throw new NotImplementedException();
    //}

    //public void ResetOnRelease()
    //{
    //    throw new NotImplementedException();
    //}

    //public virtual void ResetOnAcquire()
    //{
    //    RootParseNode = null;
    //    TranslatorContext = null;
    //}

    //#endregion
}