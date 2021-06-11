using System;
using System.Collections.Generic;
using CodeComposerLib.Irony.Semantic;
using CodeComposerLib.Irony.Semantic.Scope;
using CodeComposerLib.Irony.SourceCode;
using Irony.Parsing;

namespace CodeComposerLib.Irony.Compiler
{
    public abstract class LanguageTempCodeCompiler : LanguageCompiler
    {
        /// <summary>
        /// The DSL source code unit to be compiled
        /// </summary>
        public LanguageCodeText CodeUnit { get; protected set; }

        /// <summary>
        /// The root parse tree node generated after parsing the source code (using Irony parser)
        /// </summary>
        public ParseTreeNode RootParseNode { get; protected set; }


        protected LanguageTempCodeCompiler()
            : this(null, null)
        {
        }

        protected LanguageTempCodeCompiler(LanguageCompilationLog compilationLog)
            : this(null, compilationLog)
        {
        }

        protected LanguageTempCodeCompiler(IronyAst rootAst)
            : this(rootAst, null)
        {
        }

        protected LanguageTempCodeCompiler(IronyAst rootAst, LanguageCompilationLog compilationLog)
        {
            var textCodeUnit = new LanguageCodeText("", "");

            CodeUnit = textCodeUnit;
            RootAst = rootAst;
            CompilationLog = compilationLog ?? new LanguageCompilationLog(textCodeUnit, Progress);
            TranslatorContext = null;
        }


        /// <summary>
        /// Initialize the compilation log of this compiler
        /// </summary>
        protected virtual void InitializeCompilationLog()
        {
            if (CompilationLog == null)
                CompilationLog = new LanguageCompilationLog(CodeUnit, Progress);

            else
                CompilationLog.Initialize(CodeUnit);
        }

        /// <summary>
        /// Parse the source code to generate the parse tree for this code unit
        /// </summary>
        /// <param name="parsingFunction"></param>
        protected virtual void ParseSourceCode(Func<string, LanguageCompilationLog, ParseTreeNode> parsingFunction)
        {
            try
            {
                RootParseNode = parsingFunction(CodeUnit.CodeText, CompilationLog);
            }
            catch (Exception)
            {
                RootParseNode = null;
            }
        }

        /// <summary>
        /// Initialize the translator context
        /// </summary>
        /// <param name="parentScope"></param>
        protected abstract void InitializeTranslatorContext(LanguageScope parentScope);

        /// <summary>
        /// Initialize the translator context
        /// </summary>
        /// <param name="parentScope"></param>
        /// <param name="openedScopes"></param>
        protected abstract void InitializeTranslatorContext(LanguageScope parentScope, IEnumerable<LanguageScope> openedScopes);

        /// <summary>
        /// Initialize the compiler to be ready for parse tree to AST translation
        /// </summary>
        /// <param name="codeText"></param>
        /// <param name="parsingFunction"></param>
        /// <param name="parentScope"></param>
        /// <param name="openedScopes"></param>
        /// <returns></returns>
        protected bool InitializeCompiler(string codeText, Func<string, LanguageCompilationLog, ParseTreeNode> parsingFunction, LanguageScope parentScope, IEnumerable<LanguageScope> openedScopes)
        {
            CodeUnit = new LanguageCodeText("", codeText);

            InitializeCompilationLog();

            RootAst = parentScope.RootAst;

            //Parse the new code (this is a fast operation)
            ParseSourceCode(parsingFunction);

            //Translate the parse tree into the root AST
            if (RootParseNode == null)
                return false;

            InitializeTranslatorContext(parentScope, openedScopes);

            //Make sure the translator context is initialized
            return TranslatorContext != null;
        }

        /// <summary>
        /// Initialize the compiler to be ready for parse tree to AST translation
        /// </summary>
        /// <param name="codeText"></param>
        /// <param name="parsingFunction"></param>
        /// <param name="parentScope"></param>
        /// <returns></returns>
        protected bool InitializeCompiler(string codeText, Func<string, LanguageCompilationLog, ParseTreeNode> parsingFunction, LanguageScope parentScope)
        {
            CodeUnit = new LanguageCodeText("", codeText);

            InitializeCompilationLog();

            RootAst = parentScope.RootAst;

            //Parse the new code (this is a fast operation)
            ParseSourceCode(parsingFunction);

            //Translate the parse tree into the root AST
            if (RootParseNode == null)
                return false;

            InitializeTranslatorContext(parentScope);

            //Make sure the translator context is initialized
            return TranslatorContext != null;
        }
    }
}
