using CodeComposerLib;
using CodeComposerLib.Irony.Semantic;
using CodeComposerLib.Languages;
using GeometricAlgebraLib.CodeComposer.LanguageServers;
using GeometricAlgebraLib.Processing.SymbolicExpressions.Context;
using TextComposerLib.Loggers.Progress;

namespace GeometricAlgebraLib.CodeComposer.Composers
{
    /// <summary>
    /// This class represents a text generator that can access information from a GaClcAST to generate text into
    /// code files in a base output folder. This class should be the base for all GaClc-based code generation processes
    /// </summary>
    public abstract class GaCodeLibraryComposerBase : 
        CodeLibraryComposer
    {
        public override string ProgressSourceId 
            => Name;

        public override ProgressComposer Progress 
            => null;

        public override LanguageServer Language 
            => GaClcLanguage;

        /// <summary>
        /// The GaClc target language of this generator
        /// </summary>
        public GaClcLanguageServer GaClcLanguage { get; }

        public SymbolicContextOptions DefaultContextOptions { get; }
            = new SymbolicContextOptions();

        public GaClcSymbolicContextCodeComposerOptions DefaultContextCodeComposerOptions { get; }
            = new GaClcSymbolicContextCodeComposerOptions();

        /// <summary>
        /// All derived classes must take a single AstRoot parameter for uniform operation purposes
        /// </summary>
        /// <param name="targetLanguage"></param>
        protected GaCodeLibraryComposerBase(GaClcLanguageServer targetLanguage)
        {
            GaClcLanguage = targetLanguage;
        }


        /// <summary>
        /// Create an un-initialized copy of this library generator
        /// </summary>
        /// <returns></returns>
        public abstract GaCodeLibraryComposerBase CreateEmptyComposer();

        /// <summary>
        /// Create a macro code generator based on this library
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public virtual GaClcSymbolicContextCodeComposer CreateSymbolicContextCodeComposer(SymbolicContext context)
        {
            return new GaClcSymbolicContextCodeComposer(this, context);
        }

        /// <summary>
        /// Generates a single line comment on a separate line in the active file
        /// </summary>
        /// <param name="commentText"></param>
        public void GenerateComment(string commentText)
        {
            ActiveFileTextComposer.AppendLineAtNewLine(
                GaClcLanguage.CodeComposer.GenerateCode(
                    SyntaxFactory.Comment(commentText)
                )
            );
        }

        /// <summary>
        /// Return a unique name for the given AST object
        /// </summary>
        /// <param name="astObject"></param>
        /// <returns></returns>
        protected string GetUniqueName(IIronyAstObjectNamed astObject)
        {
            return astObject.ObjectName + astObject.ObjectId.ToString("X4");
        }
    }
}
