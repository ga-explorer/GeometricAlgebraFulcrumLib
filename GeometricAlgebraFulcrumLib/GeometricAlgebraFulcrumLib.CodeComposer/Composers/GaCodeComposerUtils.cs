using GeometricAlgebraFulcrumLib.CodeComposer.Languages;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Context;

namespace GeometricAlgebraFulcrumLib.CodeComposer.Composers
{
    public static class GaCodeComposerUtils
    {
        public static GaSymbolicContextCodeComposer CreateContextCodeComposer(this SymbolicContext context, GaLanguageServer languageServer)
        {
            return new GaSymbolicContextCodeComposer(
                languageServer,
                context
            );
        }

        public static GaSymbolicContextCodeComposer CreateContextCodeComposer(this SymbolicContext context, GaLanguageServer languageServer, GaSymbolicContextCodeComposerOptions options)
        {
            return new GaSymbolicContextCodeComposer(
                languageServer,
                context,
                options
            );
        }


        public static GaSymbolicContextCodeComposer CreateContextCodeComposer(this GaLanguageServer languageServer, SymbolicContext context)
        {
            return new GaSymbolicContextCodeComposer(
                languageServer,
                context
            );
        }

        public static GaSymbolicContextCodeComposer CreateContextCodeComposer(this GaLanguageServer languageServer, SymbolicContext context, GaSymbolicContextCodeComposerOptions options)
        {
            return new GaSymbolicContextCodeComposer(
                languageServer,
                context,
                options
            );
        }
    }
}
