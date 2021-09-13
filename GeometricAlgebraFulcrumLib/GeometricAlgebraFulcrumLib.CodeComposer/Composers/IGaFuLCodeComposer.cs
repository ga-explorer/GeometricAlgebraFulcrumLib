using CodeComposerLib;
using GeometricAlgebraFulcrumLib.CodeComposer.Languages;
using GeometricAlgebraFulcrumLib.Processors.SymbolicAlgebra.Context;

namespace GeometricAlgebraFulcrumLib.CodeComposer.Composers
{
    public interface IGaFuLCodeComposer :
        ICclCodeComposer
    {
        GaFuLLanguageServerBase GeoLanguage { get; }

        SymbolicContextOptions DefaultContextOptions { get; }

        GaFuLSymbolicContextCodeComposerOptions DefaultContextCodeComposerOptions { get; }

        void GenerateComment(string commentText);
    }
}