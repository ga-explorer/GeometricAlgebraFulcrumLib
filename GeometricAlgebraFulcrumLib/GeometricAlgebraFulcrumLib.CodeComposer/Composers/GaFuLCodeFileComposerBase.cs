using CodeComposerLib;
using CodeComposerLib.Irony.Semantic;
using CodeComposerLib.Languages;
using GeometricAlgebraFulcrumLib.CodeComposer.Languages;
using GeometricAlgebraFulcrumLib.Processors.SymbolicAlgebra.Context;
using TextComposerLib.Loggers.Progress;

namespace GeometricAlgebraFulcrumLib.CodeComposer.Composers
{
    public abstract class GaFuLCodeFileComposerBase :
        CclCodeFileComposerBase, IGaFuLCodeComposer
    {
        public override string ProgressSourceId 
            => Name;

        public override ProgressComposer Progress 
            => null;

        public override CclLanguageServerBase Language 
            => GeoLanguage;

        /// <summary>
        /// The target language of this generator
        /// </summary>
        public GaFuLLanguageServerBase GeoLanguage { get; }

        public SymbolicContextOptions DefaultContextOptions { get; }
            = new SymbolicContextOptions();

        public GaFuLSymbolicContextCodeComposerOptions DefaultContextCodeComposerOptions { get; }
            = new GaFuLSymbolicContextCodeComposerOptions();


        /// <summary>
        /// All derived classes must take a single AstRoot parameter for uniform operation purposes
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="targetLanguage"></param>
        protected GaFuLCodeFileComposerBase(string filePath, GaFuLLanguageServerBase targetLanguage)
            : base(filePath)
        {
            GeoLanguage = targetLanguage;
        }

        
        /// <summary>
        /// Create a macro code generator based on this library
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public virtual GaFuLSymbolicContextCodeComposer CreateSymbolicContextCodeComposer(SymbolicContext context)
        {
            return new GaFuLSymbolicContextCodeComposer(GeoLanguage, context);
        }

        /// <summary>
        /// Generates a single line comment on a separate line in the active file
        /// </summary>
        /// <param name="commentText"></param>
        public void GenerateComment(string commentText)
        {
            ActiveFileTextComposer.AppendLineAtNewLine(
                GeoLanguage.CodeGenerator.GenerateCode(
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