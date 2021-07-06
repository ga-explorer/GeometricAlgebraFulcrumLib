using GeometricAlgebraLib.Storage;

namespace GeometricAlgebraLib.Algebra.Multivectors
{
    public static class GaMultivectorsUtils
    {
        public static GaMultivector<T> CreateMultivector<T>(this IGaMultivectorStorage<T> storage)
        {
            return new(storage);
        }
    }
}
