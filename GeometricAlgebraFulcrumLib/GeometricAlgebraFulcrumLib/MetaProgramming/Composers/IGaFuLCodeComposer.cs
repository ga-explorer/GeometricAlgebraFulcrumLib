using CodeComposerLib;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context;
using GeometricAlgebraFulcrumLib.MetaProgramming.Languages;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Composers;

public interface IGaFuLCodeComposer :
    ICclCodeComposer
{
    GaFuLLanguageServerBase GeoLanguage { get; }

    MetaContextOptions DefaultContextOptions { get; }

    GaFuLMetaContextCodeComposerOptions DefaultContextCodeComposerOptions { get; }

    void GenerateComment(string commentText);
}