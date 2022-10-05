using GeometricAlgebraFulcrumLib.MetaProgramming.Context;
using GeometricAlgebraFulcrumLib.MetaProgramming.Languages;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Composers
{
    public static class GaFuLCodeComposerUtils
    {
        public static GaFuLMetaContextCodeComposer CreateContextCodeComposer(this MetaContext context, GaFuLLanguageServerBase languageServer)
        {
            return new GaFuLMetaContextCodeComposer(
                languageServer,
                context
            );
        }

        public static GaFuLMetaContextCodeComposer CreateContextCodeComposer(this MetaContext context, GaFuLLanguageServerBase languageServer, GaFuLMetaContextCodeComposerOptions options)
        {
            return new GaFuLMetaContextCodeComposer(
                languageServer,
                context,
                options
            );
        }


        public static GaFuLMetaContextCodeComposer CreateContextCodeComposer(this GaFuLLanguageServerBase languageServer, MetaContext context)
        {
            return new GaFuLMetaContextCodeComposer(
                languageServer,
                context
            );
        }

        public static GaFuLMetaContextCodeComposer CreateContextCodeComposer(this GaFuLLanguageServerBase languageServer, MetaContext context, GaFuLMetaContextCodeComposerOptions options)
        {
            return new GaFuLMetaContextCodeComposer(
                languageServer,
                context,
                options
            );
        }
    }
}
