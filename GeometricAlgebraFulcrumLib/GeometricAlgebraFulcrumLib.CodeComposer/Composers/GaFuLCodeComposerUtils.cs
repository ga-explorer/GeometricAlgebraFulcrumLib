using GeometricAlgebraFulcrumLib.CodeComposer.Languages;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Context;

namespace GeometricAlgebraFulcrumLib.CodeComposer.Composers
{
    public static class GaFuLCodeComposerUtils
    {
        public static GaFuLSymbolicContextCodeComposer CreateContextCodeComposer(this SymbolicContext context, GaFuLLanguageServerBase languageServer)
        {
            return new GaFuLSymbolicContextCodeComposer(
                languageServer,
                context
            );
        }

        public static GaFuLSymbolicContextCodeComposer CreateContextCodeComposer(this SymbolicContext context, GaFuLLanguageServerBase languageServer, GaFuLSymbolicContextCodeComposerOptions options)
        {
            return new GaFuLSymbolicContextCodeComposer(
                languageServer,
                context,
                options
            );
        }


        public static GaFuLSymbolicContextCodeComposer CreateContextCodeComposer(this GaFuLLanguageServerBase languageServer, SymbolicContext context)
        {
            return new GaFuLSymbolicContextCodeComposer(
                languageServer,
                context
            );
        }

        public static GaFuLSymbolicContextCodeComposer CreateContextCodeComposer(this GaFuLLanguageServerBase languageServer, SymbolicContext context, GaFuLSymbolicContextCodeComposerOptions options)
        {
            return new GaFuLSymbolicContextCodeComposer(
                languageServer,
                context,
                options
            );
        }
    }
}
