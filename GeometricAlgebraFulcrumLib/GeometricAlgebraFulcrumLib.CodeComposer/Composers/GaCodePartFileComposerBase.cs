using TextComposerLib.Files;
using TextComposerLib.Text.Linear;

namespace GeometricAlgebraFulcrumLib.CodeComposer.Composers
{
    /// <summary>
    /// This abstract class can be used to implement a sub-process of code file generation
    /// using the main code library generator components
    /// </summary>
    public abstract class GaCodePartFileComposerBase : 
        GaCodePartComposerBase
    {
        public TextFileComposer FileComposer { get; }

        public LinearTextComposer TextComposer 
            => FileComposer.TextComposer;


        protected GaCodePartFileComposerBase(IGaCodeComposer codeLibraryComposer)
            : base(codeLibraryComposer)
        {
            FileComposer = CodeComposer.ActiveFileComposer;
        }


        public abstract void Generate();
    }
}
