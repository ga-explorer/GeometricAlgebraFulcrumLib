using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Signatures;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;
using GeometricAlgebraFulcrumLib.Structures;

namespace GeometricAlgebraFulcrumLib.Processing.Multivectors.Products.Iterators
{
    public interface IGaProductTermsIterator<T>
    {
        IGaScalarProcessor<T> ScalarProcessor { get; }

        IGaStorageMultivector<T> Storage1 { get; }

        IGaStorageMultivector<T> Storage2 { get; }


        IEnumerable<GaRecordKeyValue<T>> GetOpIdScalarRecords();

        IEnumerable<GaRecordKeyValue<T>> GetEGpIdScalarRecords();

        IEnumerable<GaRecordKeyValue<T>> GetESpIdScalarRecords();

        IEnumerable<T> GetESpScalars();

        IEnumerable<GaRecordKeyValue<T>> GetELcpIdScalarRecords();

        IEnumerable<GaRecordKeyValue<T>> GetERcpIdScalarRecords();

        IEnumerable<GaRecordKeyValue<T>> GetEHipIdScalarRecords();

        IEnumerable<GaRecordKeyValue<T>> GetEFdpIdScalarRecords();

        IEnumerable<GaRecordKeyValue<T>> GetECpIdScalarRecords();

        IEnumerable<GaRecordKeyValue<T>> GetEAcpIdScalarRecords();


        IEnumerable<GaRecordKeyValue<T>> GetGpIdScalarRecords(IGaSignature basesSignature);

        IEnumerable<GaRecordKeyValue<T>> GetSpIdScalarRecords(IGaSignature basesSignature);

        IEnumerable<T> GetSpScalars(IGaSignature basesSignature);

        IEnumerable<GaRecordKeyValue<T>> GetLcpIdScalarRecords(IGaSignature basesSignature);

        IEnumerable<GaRecordKeyValue<T>> GetRcpIdScalarRecords(IGaSignature basesSignature);

        IEnumerable<GaRecordKeyValue<T>> GetHipIdScalarRecords(IGaSignature basesSignature);

        IEnumerable<GaRecordKeyValue<T>> GetFdpIdScalarRecords(IGaSignature basesSignature);

        IEnumerable<GaRecordKeyValue<T>> GetCpIdScalarRecords(IGaSignature basesSignature);

        IEnumerable<GaRecordKeyValue<T>> GetAcpIdScalarRecords(IGaSignature basesSignature);
    }
}