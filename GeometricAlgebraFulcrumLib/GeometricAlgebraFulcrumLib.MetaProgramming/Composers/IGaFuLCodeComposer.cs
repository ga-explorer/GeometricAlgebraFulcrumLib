using GeometricAlgebraFulcrumLib.Utilities.Code;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context;
using GeometricAlgebraFulcrumLib.MetaProgramming.Utilities.Code;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Composers;

public interface IGaFuLCodeComposer :
    ICclCodeComposer
{
    GaFuLLanguageServerBase GeoLanguage { get; }

    MetaContextOptions DefaultContextOptions { get; }

    GaFuLMetaContextCodeComposerOptions DefaultContextCodeComposerOptions { get; }

    void GenerateComment(string commentText);
}