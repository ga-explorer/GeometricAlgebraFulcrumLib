using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra.Signatures;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra.ProductIterators
{
    public interface IMultivectorStorageTermsIterator<T>
    {
        IScalarAlgebraProcessor<T> ScalarProcessor { get; }

        IMultivectorStorage<T> Storage1 { get; }

        IMultivectorStorage<T> Storage2 { get; }


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


        IEnumerable<IndexScalarRecord<T>> GetGpIdScalarRecords(IGeometricAlgebraSignature basesSignature);

        IEnumerable<IndexScalarRecord<T>> GetSpIdScalarRecords(IGeometricAlgebraSignature basesSignature);

        IEnumerable<T> GetSpScalars(IGeometricAlgebraSignature basesSignature);

        IEnumerable<IndexScalarRecord<T>> GetLcpIdScalarRecords(IGeometricAlgebraSignature basesSignature);

        IEnumerable<IndexScalarRecord<T>> GetRcpIdScalarRecords(IGeometricAlgebraSignature basesSignature);

        IEnumerable<IndexScalarRecord<T>> GetHipIdScalarRecords(IGeometricAlgebraSignature basesSignature);

        IEnumerable<IndexScalarRecord<T>> GetFdpIdScalarRecords(IGeometricAlgebraSignature basesSignature);

        IEnumerable<IndexScalarRecord<T>> GetCpIdScalarRecords(IGeometricAlgebraSignature basesSignature);

        IEnumerable<IndexScalarRecord<T>> GetAcpIdScalarRecords(IGeometricAlgebraSignature basesSignature);
    }
}