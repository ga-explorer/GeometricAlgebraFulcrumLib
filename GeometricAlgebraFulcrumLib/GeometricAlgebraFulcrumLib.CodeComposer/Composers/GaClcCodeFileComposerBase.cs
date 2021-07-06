using TextComposerLib.Files;
using TextComposerLib.Text.Linear;

namespace GeometricAlgebraFulcrumLib.CodeComposer.Composers
{
    /// <summary>
    /// This abstract class can be used to implement a sub-process of code file generation
    /// using the main code library generator components
    /// </summary>
    public abstract class GaClcCodeFileComposerBase : 
        GaClcCodePartComposerBase
    {
        public TextFileComposer FileComposer { get; }

        public LinearTextComposer TextComposer 
            => FileComposer.TextComposer;


        protected GaClcCodeFileComposerBase(GaCodeLibraryComposerBase codeLibraryComposer)
            : base(codeLibraryComposer)
        {
            FileComposer = LibraryComposer.ActiveFileComposer;
        }


        public abstract void Generate();
    }
}
