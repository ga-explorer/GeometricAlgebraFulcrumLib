using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Signatures;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage;

namespace GeometricAlgebraFulcrumLib.Processing.Multivectors.Products.Iterators
{
    public interface IGaProductTermsIterator<T>
    {
        IGaScalarProcessor<T> ScalarProcessor { get; }

        IGaStorageMultivector<T> Storage1 { get; }

        IGaStorageMultivector<T> Storage2 { get; }


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


        IEnumerable<KeyValuePair<ulong, T>> GetGpIdScalarPairs(GaSignature basesSignature);

        IEnumerable<KeyValuePair<ulong, T>> GetSpIdScalarPairs(GaSignature basesSignature);

        IEnumerable<T> GetSpScalars(GaSignature basesSignature);

        IEnumerable<KeyValuePair<ulong, T>> GetLcpIdScalarPairs(GaSignature basesSignature);

        IEnumerable<KeyValuePair<ulong, T>> GetRcpIdScalarPairs(GaSignature basesSignature);

        IEnumerable<KeyValuePair<ulong, T>> GetHipIdScalarPairs(GaSignature basesSignature);

        IEnumerable<KeyValuePair<ulong, T>> GetFdpIdScalarPairs(GaSignature basesSignature);

        IEnumerable<KeyValuePair<ulong, T>> GetCpIdScalarPairs(GaSignature basesSignature);

        IEnumerable<KeyValuePair<ulong, T>> GetAcpIdScalarPairs(GaSignature basesSignature);
    }
}