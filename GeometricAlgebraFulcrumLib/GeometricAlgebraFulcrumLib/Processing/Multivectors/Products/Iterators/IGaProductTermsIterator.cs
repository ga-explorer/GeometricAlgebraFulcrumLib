using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Signatures;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;

namespace GeometricAlgebraFulcrumLib.Processing.Multivectors.Products.Iterators
{
    public interface IGaProductTermsIterator<T>
    {
        IScalarProcessor<T> ScalarProcessor { get; }

        IGaMultivectorStorage<T> Storage1 { get; }

        IGaMultivectorStorage<T> Storage2 { get; }


        IEnumerable<IndexScalarRecord<T>> GetOpIdScalarRecords();

        IEnumerable<IndexScalarRecord<T>> GetEGpIdScalarRecords();

        IEnumerable<IndexScalarRecord<T>> GetESpIdScalarRecords();

        IEnumerable<T> GetESpScalars();

        IEnumerable<IndexScalarRecord<T>> GetELcpIdScalarRecords();

        IEnumerable<IndexScalarRecord<T>> GetERcpIdScalarRecords();

        IEnumerable<IndexScalarRecord<T>> GetEHipIdScalarRecords();

        IEnumerable<IndexScalarRecord<T>> GetEFdpIdScalarRecords();

        IEnumerable<IndexScalarRecord<T>> GetECpIdScalarRecords();

        IEnumerable<IndexScalarRecord<T>> GetEAcpIdScalarRecords();


        IEnumerable<IndexScalarRecord<T>> GetGpIdScalarRecords(IGaSignature basesSignature);

        IEnumerable<IndexScalarRecord<T>> GetSpIdScalarRecords(IGaSignature basesSignature);

        IEnumerable<T> GetSpScalars(IGaSignature basesSignature);

        IEnumerable<IndexScalarRecord<T>> GetLcpIdScalarRecords(IGaSignature basesSignature);

        IEnumerable<IndexScalarRecord<T>> GetRcpIdScalarRecords(IGaSignature basesSignature);

        IEnumerable<IndexScalarRecord<T>> GetHipIdScalarRecords(IGaSignature basesSignature);

        IEnumerable<IndexScalarRecord<T>> GetFdpIdScalarRecords(IGaSignature basesSignature);

        IEnumerable<IndexScalarRecord<T>> GetCpIdScalarRecords(IGaSignature basesSignature);

        IEnumerable<IndexScalarRecord<T>> GetAcpIdScalarRecords(IGaSignature basesSignature);
    }
}