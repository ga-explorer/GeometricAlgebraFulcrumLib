using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Composers;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    public static class TextComposerUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetMultivectorText<T>(this ITextComposer<T> composer, IMultivectorStorageContainer<T> container)
        {
            return composer.GetMultivectorText(
                container.GetMultivectorStorage()
            );
        }
    }
}
