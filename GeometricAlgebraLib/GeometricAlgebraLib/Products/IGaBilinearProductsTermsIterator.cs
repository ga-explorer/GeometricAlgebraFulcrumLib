using System.Collections.Generic;
using GeometricAlgebraLib.Frames;
using GeometricAlgebraLib.Processors.Scalars;
using GeometricAlgebraLib.Storage;

namespace GeometricAlgebraLib.Products
{
    public interface IGaBilinearProductsTermsIterator<T>
    {
        IGaScalarProcessor<T> ScalarProcessor { get; }

        IGaMultivectorStorage<T> Storage1 { get; }

        IGaMultivectorStorage<T> Storage2 { get; }


        IEnumerable<KeyValuePair<ulong, T>> GetOpIdScalarPairs();

        IEnumerable<KeyValuePair<ulong, T>> GetEGpIdScalarPairs();

        IEnumerable<KeyValuePair<ulong, T>> GetESpIdScalarPairs();

        IEnumerable<T> GetESpScalars();

        IEnumerable<KeyValuePair<ulong, T>> GetELcpIdScalarPairs();

        IEnumerable<KeyValuePair<ulong, T>> GetERcpIdScalarPairs();

        IEnumerable<KeyValuePair<ulong, T>> GetEHipIdScalarPairs();

        IEnumerable<KeyValuePair<ulong, T>> GetEFdpIdScalarPairs();

        IEnumerable<KeyValuePair<ulong, T>> GetECpIdScalarPairs();

        IEnumerable<KeyValuePair<ulong, T>> GetEAcpIdScalarPairs();


        IEnumerable<KeyValuePair<ulong, T>> GetGpIdScalarPairs(GaOrthonormalBasesSignature basesSignature);

        IEnumerable<KeyValuePair<ulong, T>> GetSpIdScalarPairs(GaOrthonormalBasesSignature basesSignature);

        IEnumerable<T> GetSpScalars(GaOrthonormalBasesSignature basesSignature);

        IEnumerable<KeyValuePair<ulong, T>> GetLcpIdScalarPairs(GaOrthonormalBasesSignature basesSignature);

        IEnumerable<KeyValuePair<ulong, T>> GetRcpIdScalarPairs(GaOrthonormalBasesSignature basesSignature);

        IEnumerable<KeyValuePair<ulong, T>> GetHipIdScalarPairs(GaOrthonormalBasesSignature basesSignature);

        IEnumerable<KeyValuePair<ulong, T>> GetFdpIdScalarPairs(GaOrthonormalBasesSignature basesSignature);

        IEnumerable<KeyValuePair<ulong, T>> GetCpIdScalarPairs(GaOrthonormalBasesSignature basesSignature);

        IEnumerable<KeyValuePair<ulong, T>> GetAcpIdScalarPairs(GaOrthonormalBasesSignature basesSignature);
    }
}