using System;
using CodeComposerLib.Irony.SourceCode;
using Irony.Parsing;

namespace CodeComposerLib.Irony.Compiler;

/// <summary>
/// This compiler can be used to parse a code unit (code file or in-memory text) and then translate
/// the parse tree into a given abstract syntax tree
/// </summary>
public abstract class LanguageProjectCodeUnitCompiler : LanguageCompiler
{
    /// <summary>
    /// The DSL source code unit to be compiled
    /// </summary>
    public ISourceCodeUnit CodeUnit { get; protected set; }

    /// <summary>
    /// The root parse tree node generated after parsing the source code (using Irony parser)
    /// </summary>
    public ParseTreeNode RootParseNode { get; protected set; }
        
    /// <summary>
    /// The parent compiler that created this compiler
    /// </summary>
    public LanguageProjectCompiler ParentCompiler { get; }


    protected LanguageProjectCodeUnitCompiler(LanguageProjectCompiler parentCompiler, ISourceCodeUnit codeUnit, ParseTreeNode rootParseNode)
    {
        ParentCompiler = parentCompiler;
        CodeUnit = codeUnit;
        RootParseNode = rootParseNode;
        RootAst = parentCompiler.RootAst;
        CompilationLog = parentCompiler.CompilationLog;
        TranslatorContext = parentCompiler.TranslatorContext;
    }


    ///// <summary>
    ///// Parse the source code to generate the parse tree for this code unit
    ///// </summary>
    //protected abstract void ParseSourceCode();

    /// <summary>
    /// Perform semantic analysis on the parse tree to add more parts to the full AST
    /// </summary>
    protected abstract void TranslateToAst();

    /// <summary>
    /// Parse and translate this code unit into the root AST
    /// </summary>
    public void Compile()
    {
        //Parse the new code if not already parsed by the parent compiler (this is a fast operation)
        if (ReferenceEquals(RootParseNode, null))
            RootParseNode = ParentCompiler.ParseCodeUnit(CodeUnit);

        //Translate the parse tree into the root AST
        if (ReferenceEquals(RootParseNode, null))
            return;

        //Make sure the translator context is initialized
        if (ReferenceEquals(TranslatorContext, null))
            throw new NullReferenceException("Translator context not initialized");

        TranslateToAst();
    }
}