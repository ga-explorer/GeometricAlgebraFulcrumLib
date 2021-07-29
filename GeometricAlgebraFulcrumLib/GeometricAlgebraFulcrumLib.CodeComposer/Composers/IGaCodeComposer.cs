using CodeComposerLib;
using GeometricAlgebraFulcrumLib.CodeComposer.Languages;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Context;

namespace GeometricAlgebraFulcrumLib.CodeComposer.Composers
{
    public interface IGaCodeComposer :
        ICclCodeComposer
    {
        GaLanguageServer GaLanguage { get; }

        SymbolicContextOptions DefaultContextOptions { get; }

        GaSymbolicContextCodeComposerOptions DefaultContextCodeComposerOptions { get; }

        void GenerateComment(string commentText);
    }
}