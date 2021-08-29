using CodeComposerLib;
using GeometricAlgebraFulcrumLib.CodeComposer.Languages;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Context;

namespace GeometricAlgebraFulcrumLib.CodeComposer.Composers
{
    public interface IGaFuLCodeComposer :
        ICclCodeComposer
    {
        GaFuLLanguageServerBase GaLanguage { get; }

        SymbolicContextOptions DefaultContextOptions { get; }

        GaFuLSymbolicContextCodeComposerOptions DefaultContextCodeComposerOptions { get; }

        void GenerateComment(string commentText);
    }
}