using CodeComposerLib;
using CodeComposerLib.Irony.Semantic;
using CodeComposerLib.Languages;
using GeometricAlgebraFulcrumLib.CodeComposer.Languages;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Context;
using TextComposerLib.Loggers.Progress;

namespace GeometricAlgebraFulcrumLib.CodeComposer.Composers
{
    public abstract class GaCodeLibraryComposerBase : 
        CclCodeLibraryComposerBase, IGaCodeComposer
    {
        public override string ProgressSourceId 
            => Name;

        public override ProgressComposer Progress 
            => null;

        public override CclLanguageServerBase Language 
            => GaLanguage;

        /// <summary>
        /// The target language of this generator
        /// </summary>
        public GaLanguageServer GaLanguage { get; }

        public SymbolicContextOptions DefaultContextOptions { get; }
            = new SymbolicContextOptions();

        public GaSymbolicContextCodeComposerOptions DefaultContextCodeComposerOptions { get; }
            = new GaSymbolicContextCodeComposerOptions();


        /// <summary>
        /// All derived classes must take a single AstRoot parameter for uniform operation purposes
        /// </summary>
        /// <param name="targetLanguage"></param>
        protected GaCodeLibraryComposerBase(GaLanguageServer targetLanguage)
        {
            GaLanguage = targetLanguage;
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
        public virtual GaSymbolicContextCodeComposer CreateSymbolicContextCodeComposer(SymbolicContext context)
        {
            return new GaSymbolicContextCodeComposer(GaLanguage, context);
        }

        /// <summary>
        /// Generates a single line comment on a separate line in the active file
        /// </summary>
        /// <param name="commentText"></param>
        public void GenerateComment(string commentText)
        {
            ActiveFileTextComposer.AppendLineAtNewLine(
                GaLanguage.CodeGenerator.GenerateCode(
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
